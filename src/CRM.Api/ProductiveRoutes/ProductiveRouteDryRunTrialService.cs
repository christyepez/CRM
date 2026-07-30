using CRM.Application.Foundation;

namespace CRM.Api.ProductiveRoutes;

public sealed class ProductiveRouteDryRunTrialService(
    ProductiveRouteDryRunTrialOptions options,
    CrmProductiveRouteDryRunTrialEvaluator evaluator)
{
    public ProductiveRouteDryRunTrialResult Probe(CrmProductiveRouteDryRunTrialProbeContract request)
    {
        var evaluation = evaluator.Evaluate(new CrmProductiveRouteDryRunTrialEvaluationRequest(
            Route: request.Route,
            Method: request.Method,
            TrialEnabled: options.Enabled,
            RuntimeEnvironment: options.RuntimeEnvironment));

        return new ProductiveRouteDryRunTrialResult(
            ProductiveRouteDryRunAttempted: evaluation.ProductiveRouteDryRunAttempted,
            ProductiveRouteDryRunAllowed: evaluation.ProductiveRouteDryRunAllowed,
            ProductiveRouteDryRunDecisionReturned: evaluation.ProductiveRouteDryRunDecisionReturned,
            ProductiveRouteDryRunStatusCode: evaluation.ProductiveRouteDryRunStatusCode,
            ProductiveCrudEnabled: evaluation.ProductiveCrudEnabled,
            ProductiveDomainExecutionEnabled: evaluation.ProductiveDomainExecutionEnabled,
            ProductivePersistenceEnabled: evaluation.ProductivePersistenceEnabled,
            DatabaseWriteAttempted: evaluation.DatabaseWriteAttempted,
            SideEffectsAllowed: evaluation.SideEffectsAllowed,
            DeleteEndpointsEnabled: evaluation.DeleteEndpointsEnabled,
            DbRuntimeEnabled: evaluation.DbRuntimeEnabled,
            EfRuntimeEnabled: evaluation.EfRuntimeEnabled,
            MigrationsEnabled: evaluation.MigrationsEnabled,
            SchemaChangeAllowed: evaluation.SchemaChangeAllowed,
            PortalAuthMetadataDependencyValidated: true,
            CommonDbMetadataDependencyValidated: true,
            SecretProviderMetadataDependencyValidated: true,
            AuthHeaderRead: evaluation.AuthHeaderRead,
            TokenRead: evaluation.TokenRead,
            TokenStored: evaluation.TokenStored,
            AuthAttributeEnabled: evaluation.AuthAttributeEnabled,
            LoginEndpointCreated: evaluation.LoginEndpointCreated,
            LogoutEndpointCreated: evaluation.LogoutEndpointCreated,
            IdentityRuntimeEnabled: evaluation.IdentityRuntimeEnabled,
            NonProductionOnly: evaluation.NonProductionOnly,
            ProductionBlocked: evaluation.ProductionBlocked,
            FailClosedByDefault: evaluation.FailClosedByDefault,
            ObservabilityMetadataOnly: true,
            Status: evaluation.Status,
            Warning: evaluation.Warning,
            ErrorCategory: evaluation.ErrorCategory);
    }
}
