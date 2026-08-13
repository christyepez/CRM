namespace CRM.Infrastructure.Portal.ControlledRuntimePilot;

public sealed record NonProductionActivationOptions(
    bool Enabled,
    bool ActivationDryRunEnabled,
    string RuntimeEnvironment,
    string PortalBaseLogicalName,
    string ClientLogicalName,
    string SecretLogicalName)
{
    public static NonProductionActivationOptions Disabled() =>
        new(
            Enabled: false,
            ActivationDryRunEnabled: false,
            RuntimeEnvironment: "NonProduction",
            PortalBaseLogicalName: "logical-portal-placeholder",
            ClientLogicalName: "logical-crm-client-placeholder",
            SecretLogicalName: "logical-secret-placeholder");
}
