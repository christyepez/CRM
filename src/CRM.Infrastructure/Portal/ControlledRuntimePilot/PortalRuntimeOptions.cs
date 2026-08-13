namespace CRM.Infrastructure.Portal.ControlledRuntimePilot;

public sealed record PortalRuntimeOptions(
    bool Enabled,
    bool PortalClientEnabled,
    bool HealthSmokeEnabled,
    bool GatewayRoutesEnabled,
    bool PortalNavigationEnabled,
    string RuntimeEnvironment,
    string PortalBaseLogicalName,
    string ClientLogicalName,
    string SecretLogicalName)
{
    public static PortalRuntimeOptions Disabled() =>
        new(
            Enabled: false,
            PortalClientEnabled: false,
            HealthSmokeEnabled: false,
            GatewayRoutesEnabled: false,
            PortalNavigationEnabled: false,
            RuntimeEnvironment: "NonProduction",
            PortalBaseLogicalName: "logical-portal-placeholder",
            ClientLogicalName: "logical-crm-client-placeholder",
            SecretLogicalName: "logical-secret-placeholder");
}
