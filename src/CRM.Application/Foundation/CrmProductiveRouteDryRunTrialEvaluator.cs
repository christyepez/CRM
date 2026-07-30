namespace CRM.Application.Foundation;

public sealed class CrmProductiveRouteDryRunTrialEvaluator
{
    public CrmProductiveRouteDryRunTrialEvaluationResult Evaluate(CrmProductiveRouteDryRunTrialEvaluationRequest request)
    {
        if (IsProduction(request.RuntimeEnvironment))
        {
            return Locked("ProductionBlocked", "Productive route dry-run trial is blocked in Production");
        }

        if (!request.TrialEnabled)
        {
            return Locked("FlagDisabled", "Productive route dry-run trial is disabled by default");
        }

        if (IsDelete(request.Method))
        {
            return Locked("DeleteBlocked", "DELETE endpoints are not allowed in the productive route dry-run trial");
        }

        return new CrmProductiveRouteDryRunTrialEvaluationResult(
            ProductiveRouteDryRunAttempted: true,
            ProductiveRouteDryRunAllowed: false,
            ProductiveRouteDryRunDecisionReturned: true,
            ProductiveRouteDryRunStatusCode: 423,
            ProductiveCrudEnabled: false,
            ProductiveDomainExecutionEnabled: false,
            ProductivePersistenceEnabled: false,
            DatabaseWriteAttempted: false,
            SideEffectsAllowed: false,
            DeleteEndpointsEnabled: false,
            DbRuntimeEnabled: false,
            EfRuntimeEnabled: false,
            MigrationsEnabled: false,
            SchemaChangeAllowed: false,
            AuthHeaderRead: false,
            TokenRead: false,
            TokenStored: false,
            AuthAttributeEnabled: false,
            LoginEndpointCreated: false,
            LogoutEndpointCreated: false,
            IdentityRuntimeEnabled: false,
            NonProductionOnly: true,
            ProductionBlocked: true,
            FailClosedByDefault: true,
            Status: "Locked",
            Warning: "Productive route dry-run returns a sanitized 423 decision without side effects",
            ErrorCategory: "DryRunLocked");
    }

    private static CrmProductiveRouteDryRunTrialEvaluationResult Locked(string category, string warning) =>
        new(
            ProductiveRouteDryRunAttempted: false,
            ProductiveRouteDryRunAllowed: false,
            ProductiveRouteDryRunDecisionReturned: false,
            ProductiveRouteDryRunStatusCode: 423,
            ProductiveCrudEnabled: false,
            ProductiveDomainExecutionEnabled: false,
            ProductivePersistenceEnabled: false,
            DatabaseWriteAttempted: false,
            SideEffectsAllowed: false,
            DeleteEndpointsEnabled: false,
            DbRuntimeEnabled: false,
            EfRuntimeEnabled: false,
            MigrationsEnabled: false,
            SchemaChangeAllowed: false,
            AuthHeaderRead: false,
            TokenRead: false,
            TokenStored: false,
            AuthAttributeEnabled: false,
            LoginEndpointCreated: false,
            LogoutEndpointCreated: false,
            IdentityRuntimeEnabled: false,
            NonProductionOnly: true,
            ProductionBlocked: true,
            FailClosedByDefault: true,
            Status: "Locked",
            Warning: warning,
            ErrorCategory: category);

    private static bool IsProduction(string value) =>
        value.Equals("Production", StringComparison.OrdinalIgnoreCase);

    private static bool IsDelete(string? method) =>
        string.Equals(method, "DELETE", StringComparison.OrdinalIgnoreCase);
}
