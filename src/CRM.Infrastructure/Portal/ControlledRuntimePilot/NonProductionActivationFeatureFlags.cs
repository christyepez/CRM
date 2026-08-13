namespace CRM.Infrastructure.Portal.ControlledRuntimePilot;

public sealed record NonProductionActivationFeatureFlags(
    bool FirstSliceEnabled,
    bool PortalClientEnabled,
    bool HealthSmokeEnabled,
    bool GatewayRoutesEnabled,
    bool PortalNavigationEnabled)
{
    public static NonProductionActivationFeatureFlags Disabled() => new(false, false, false, false, false);

    public bool AnyEnabled => FirstSliceEnabled || PortalClientEnabled || HealthSmokeEnabled || GatewayRoutesEnabled || PortalNavigationEnabled;
}
