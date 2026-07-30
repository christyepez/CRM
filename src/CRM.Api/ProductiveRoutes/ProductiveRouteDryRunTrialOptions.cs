namespace CRM.Api.ProductiveRoutes;

public sealed record ProductiveRouteDryRunTrialOptions(
    bool Enabled,
    string RuntimeEnvironment);
