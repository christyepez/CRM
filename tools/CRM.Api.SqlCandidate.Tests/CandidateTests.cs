using System.Net;
using CRM.Application.Ports.Persistence;
using CRM.CentralizationCandidate;
using CRM.Runtime.Sql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CRM.SqlCandidate.Tests;

public sealed class CandidateTests
{
    // Synthetic configuration only: this identity does not exist and no test opens SQL.
    private const string Synthetic = "Server=tcp:127.0.0.1,1433;Database=CrmMigration_Synthetic;User ID=CrmSyntheticTest;Password=not-a-real-credential;Encrypt=True;TrustServerCertificate=True";
    private static Dictionary<string,string?> Settings() => new()
    {
        ["Crm:CentralizationCandidate:Enabled"]="true",
        ["Crm:CentralizationCandidate:TenantId"]="default",
        ["ConnectionStrings:CrmCandidate"]=Synthetic
    };

    [Fact] public void AllThirteenPortsUseSqlAndOldDescriptorsAreRemoved()
    {
        var builder=WebApplication.CreateBuilder(new WebApplicationOptions{EnvironmentName="Development"});
        builder.Configuration.AddInMemoryCollection(Settings());
        builder.Services.AddSingleton<ILeadFoundationStore>(_=>throw new InvalidOperationException("Old descriptor must not survive."));
        CandidateComposition.Configure(builder);
        using var services=builder.Services.BuildServiceProvider();
        var types=new[] {typeof(ILeadFoundationStore),typeof(IAccountFoundationStore),typeof(IContactFoundationStore),
            typeof(IActivityFoundationStore),typeof(ICampaignFoundationStore),typeof(ICaseFoundationStore),
            typeof(IInteractionFoundationStore),typeof(INoteFoundationStore),typeof(ITagFoundationStore),
            typeof(IAssignmentFoundationStore),typeof(IDocumentMetadataFoundationStore),typeof(ISegmentFoundationStore),typeof(IOpportunityFoundationStore)};
        foreach(var type in types)
        {
            Assert.Single(builder.Services.Where(x=>x.ServiceType==type));
            Assert.Equal(typeof(FoundationDbContext).Assembly,services.GetRequiredService(type).GetType().Assembly);
        }
        Assert.Equal("http://127.0.0.1:0",builder.Configuration["urls"]);
    }

    [Theory][InlineData("Production")][InlineData("Staging")]
    public void NonDevelopmentRejected(string environment)
    {
        var builder=WebApplication.CreateBuilder(new WebApplicationOptions{EnvironmentName=environment});
        builder.Configuration.AddInMemoryCollection(Settings());
        Assert.Throws<InvalidOperationException>(()=>CandidateComposition.Configure(builder));
    }
    [Theory][InlineData("Crm:CentralizationCandidate:Enabled","false")][InlineData("Crm:CentralizationCandidate:TenantId","")]
    [InlineData("ConnectionStrings:CrmCandidate","")][InlineData("ConnectionStrings:CrmCandidate","malformed")]
    [InlineData("ConnectionStrings:CrmCandidate","Server=tcp:127.0.0.1,1433;Database=PortalSecurity;User ID=sa;Password=synthetic")]
    [InlineData("ConnectionStrings:CrmCandidate","Server=tcp:127.0.0.1,1433;Database=CrmMigration_Test;User ID=sa;Password=synthetic")]
    [InlineData("ConnectionStrings:CrmCandidate","Server=tcp:127.0.0.1,1433;Database=CrmMigration_Test;User ID=test;Password=synthetic;Encrypt=False")]
    public void UnsafeConfigurationFailsClosedWithoutEchoingValues(string key,string value)
    {
        var builder=WebApplication.CreateBuilder(new WebApplicationOptions{EnvironmentName="Development"});
        var settings=Settings(); settings[key]=value; builder.Configuration.AddInMemoryCollection(settings);
        var exception=Assert.ThrowsAny<Exception>(()=>CandidateComposition.Configure(builder));
        Assert.DoesNotContain("Password=",exception.Message,StringComparison.OrdinalIgnoreCase);
        Assert.Empty(builder.Services.Where(x=>x.ServiceType==typeof(ILeadFoundationStore)));
    }

    [Theory][InlineData("GET","/api/crm/foundation/leads")][InlineData("POST","/api/crm/foundation/leads")]
    [InlineData("DELETE","/api/crm/foundation/persistence/previews")][InlineData("GET","/api/crm/readiness")]
    [InlineData("GET","/health/ready")][InlineData("POST","/health/live")]
    public async Task CandidateBlocksEveryUnacceptedSurface(string method,string path)
    {
        await using var factory=new Factory(); using var client=factory.CreateClient();
        using var response=await client.SendAsync(new(new HttpMethod(method),path));
        Assert.Equal(HttpStatusCode.ServiceUnavailable,response.StatusCode);
        Assert.Contains("CRM SQL candidate is not activated",await response.Content.ReadAsStringAsync());
        Assert.True(response.Headers.CacheControl!.NoStore);
        Assert.IsType<SqlLeadFoundationStore>(factory.Services.GetRequiredService<ILeadFoundationStore>());
    }
    [Theory][InlineData("/health")][InlineData("/health/live")]
    public async Task LivenessDoesNotClaimSqlReadiness(string path)
    {
        await using var factory=new Factory(); using var client=factory.CreateClient();
        Assert.Equal(HttpStatusCode.OK,(await client.GetAsync(path)).StatusCode);
    }

    private sealed class Factory:WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Development");
            builder.UseContentRoot(Path.GetFullPath("../../../../../tools/CRM.Api.SqlCandidate",AppContext.BaseDirectory));
            builder.ConfigureAppConfiguration((_,configuration)=>configuration.AddInMemoryCollection(Settings()));
        }
    }
}
