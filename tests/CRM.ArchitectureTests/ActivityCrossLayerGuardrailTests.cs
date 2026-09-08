using Xunit;

namespace CRM.ArchitectureTests;

public sealed class ActivityCrossLayerGuardrailTests
{
    private static readonly string Root = FindRepositoryRoot();

    [Fact]
    public void ActivityType_ValuesMatchAcrossDomainApiAndFrontend()
    {
        var domain = Read("src/CRM.Domain/Enums/CrmStatuses.cs");
        var contract = Read("src/CRM.Api/Foundation/ActivityManagementApiContracts.cs");
        var frontend = Read("frontend/crm-web/src/main.ts");

        foreach (var value in new[] { "Call", "Email", "Meeting", "Task" })
        {
            Assert.Contains(value, domain);
            Assert.Contains(value, frontend);
        }

        Assert.Contains("ActivityType", contract);
        Assert.Contains("FoundationActivityCreateRequest(", contract);
        Assert.Contains("ActivityType Type", contract);
        Assert.DoesNotContain("Visit", domain);
        Assert.DoesNotContain("Visit", contract);
        Assert.DoesNotContain("'Visit'", frontend);
    }

    [Fact]
    public void ActivityStatus_ValuesMatchAcrossDomainApiAndFrontend()
    {
        var domain = Read("src/CRM.Domain/Enums/CrmStatuses.cs");
        var frontend = Read("frontend/crm-web/src/main.ts");

        foreach (var value in new[] { "Scheduled", "Completed", "Cancelled" })
        {
            Assert.Contains(value, domain);
            Assert.Contains(value, frontend);
        }

        Assert.DoesNotContain("Overdue", domain);
        Assert.DoesNotContain("'Overdue'", ActivityFrontendSource());
    }

    [Fact]
    public void ActivityApiAndFrontend_UseFoundationRoutesOnly()
    {
        var program = Read("src/CRM.Api/Program.cs");
        var frontend = ActivityFrontendSource();

        Assert.Contains("/api/crm/foundation/activities", program);
        Assert.Contains("foundationActivitiesRoute = '/api/crm/foundation/activities'", frontend);
        Assert.Contains("path: 'foundation/activities'", Read("frontend/crm-web/src/main.ts"));

        foreach (var forbidden in new[] { "/api/crm/activities", "path: 'activities'", ".delete<", "deleteActivity", "Delete activity" })
        {
            Assert.DoesNotContain(forbidden, frontend);
        }

        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/activities", program);
        Assert.DoesNotContain("MapDelete(\"/api/crm/activities", program);
    }

    [Fact]
    public void ActivityFrontend_SecurityAndSafetyGuardrailsRemainClosed()
    {
        var frontend = ActivityFrontendSource();

        foreach (var forbidden in new[]
        {
            "innerHTML",
            "bypassSecurityTrustHtml",
            "console.log",
            "local" + "Storage",
            "session" + "Storage",
            "Author" + "ization",
            "Bear" + "er",
            "Sql" + "Connection",
            "Use" + "SqlServer",
            "AccountId",
            "OpportunityId",
            "AssignedUserId",
            "OwnerId",
            "AgentId"
        })
        {
            Assert.DoesNotContain(forbidden, frontend);
        }

        Assert.Contains("isSubmitting", frontend);
        Assert.Contains("toISOString()", frontend);
        Assert.Contains("statusLabel(activity)", frontend);
    }

    [Fact]
    public void ActivityApplication_TargetValidation_IsolatedToLeadAndContactFoundationSeams()
    {
        var service = Read("src/CRM.Application/ActivityManagement/ActivityManagementService.cs");

        foreach (var marker in new[]
        {
            "IActivityFoundationStore",
            "ILeadFoundationStore",
            "IContactFoundationStore",
            "ActivityManagementPolicy.Evaluate",
            "leads.GetPreviewByIdAsync",
            "contacts.GetPreviewByIdAsync"
        })
        {
            Assert.Contains(marker, service);
        }

        foreach (var forbidden in new[] { "IAccountFoundationStore", "Opportunity", "DbContext", "Sql" + "Connection", "Use" + "SqlServer" })
        {
            Assert.DoesNotContain(forbidden, service);
        }
    }

    private static string ActivityFrontendSource()
    {
        var frontend = Read("frontend/crm-web/src/main.ts");
        var activityStart = frontend.IndexOf("type ActivityType", StringComparison.Ordinal);
        var activityEnd = frontend.IndexOf("selector: 'crm-home'", StringComparison.Ordinal);
        Assert.True(activityStart >= 0, "Activity frontend start marker was not found.");
        Assert.True(activityEnd > activityStart, "Activity frontend end marker was not found.");
        return frontend[activityStart..activityEnd];
    }

    private static string Read(string relativePath) => File.ReadAllText(Path.Combine(Root, relativePath));

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            if (File.Exists(Path.Combine(directory.FullName, "CRM.sln")))
            {
                return directory.FullName;
            }

            directory = directory.Parent;
        }

        throw new InvalidOperationException("Repository root could not be located.");
    }
}
