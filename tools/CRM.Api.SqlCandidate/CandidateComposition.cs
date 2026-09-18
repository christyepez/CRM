using CRM.Application.Ports.Persistence;
using CRM.Migration.Application;
using CRM.Runtime.Sql;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CRM.CentralizationCandidate;

// Composition only. Existing Application services own all use cases.
public static class CandidateComposition
{
    public static void Configure(WebApplicationBuilder builder)
    {
        if (!builder.Environment.IsDevelopment() || !builder.Configuration.GetValue<bool>("Crm:CentralizationCandidate:Enabled"))
            throw new InvalidOperationException("SQL candidate requires explicit Development configuration; no memory fallback.");
        var tenant = builder.Configuration["Crm:CentralizationCandidate:TenantId"] ?? "";
        SnapshotValidation.ValidateTenant(tenant);
        var connection = builder.Configuration.GetConnectionString("CrmCandidate");
        if (string.IsNullOrWhiteSpace(connection)) throw new InvalidOperationException("Dedicated candidate SQL configuration is required.");
        SqlConnectionStringBuilder sql;
        try { sql = new(connection); }
        catch (ArgumentException) { throw new InvalidOperationException("Invalid candidate SQL configuration."); }
        if (!sql.InitialCatalog.StartsWith("CrmMigration_", StringComparison.Ordinal) || sql.InitialCatalog.Length <= 13 ||
            string.IsNullOrWhiteSpace(sql.DataSource) || sql.IntegratedSecurity ||
            string.IsNullOrWhiteSpace(sql.UserID) || sql.UserID.Equals("sa", StringComparison.OrdinalIgnoreCase) ||
            string.IsNullOrWhiteSpace(sql.Password) || sql.Encrypt == SqlConnectionEncryptOption.Optional ||
            (sql.TrustServerCertificate && sql.DataSource != "tcp:127.0.0.1,1433"))
            throw new InvalidOperationException("SQL candidate requires an isolated catalog, restricted SQL identity and encrypted connection.");
        var options = new DbContextOptionsBuilder<FoundationDbContext>().UseSqlServer(sql.ConnectionString).Options;
        Func<FoundationDbContext> db = () => new(options);
        Replace<ILeadFoundationStore>(builder.Services, new SqlLeadFoundationStore(db, tenant));
        Replace<IAccountFoundationStore>(builder.Services, new SqlAccountFoundationStore(db, tenant));
        Replace<IContactFoundationStore>(builder.Services, new SqlContactFoundationStore(db, tenant));
        Replace<IActivityFoundationStore>(builder.Services, new SqlActivityFoundationStore(db, tenant));
        Replace<ICampaignFoundationStore>(builder.Services, new SqlCampaignFoundationStore(db, tenant));
        Replace<ICaseFoundationStore>(builder.Services, new SqlCaseFoundationStore(db, tenant));
        Replace<IInteractionFoundationStore>(builder.Services, new SqlInteractionFoundationStore(db, tenant));
        Replace<INoteFoundationStore>(builder.Services, new SqlNoteFoundationStore(db, tenant));
        Replace<ITagFoundationStore>(builder.Services, new SqlTagFoundationStore(db, tenant));
        Replace<IAssignmentFoundationStore>(builder.Services, new SqlAssignmentFoundationStore(db, tenant));
        Replace<IDocumentMetadataFoundationStore>(builder.Services, new SqlDocumentMetadataFoundationStore(db, tenant));
        Replace<ISegmentFoundationStore>(builder.Services, new SqlSegmentFoundationStore(db, tenant));
        Replace<IOpportunityFoundationStore>(builder.Services, new SqlOpportunityFoundationStore(db, tenant));
        builder.WebHost.UseUrls("http://127.0.0.1:0");
    }

    public static void LockHttpSurface(WebApplication app)
    {
        app.Use(async (HttpContext context, RequestDelegate next) =>
        {
            // Liveness only. Readiness and every other surface remain locked until Portal integration.
            if (HttpMethods.IsGet(context.Request.Method) &&
                (context.Request.Path == "/health" || context.Request.Path == "/health/live"))
            {
                await next(context);
                return;
            }
            context.Response.Headers.CacheControl = "no-store";
            await Results.Problem(statusCode: StatusCodes.Status503ServiceUnavailable,
                title: "CRM SQL candidate is not activated",
                detail: "Portal authorization, audit and live-state migration acceptance are pending.")
                .ExecuteAsync(context);
        });
    }

    private static void Replace<T>(IServiceCollection services, T implementation) where T : class
    {
        services.RemoveAll<T>(); services.AddSingleton(implementation);
    }
}
