namespace CRM.Infrastructure.Portal.ControlledRuntimePilot;

public sealed record PortalRuntimeFeatureFlags(
    bool FirstSliceEnabled,
    bool PortalClientEnabled,
    bool HealthSmokeEnabled,
    bool GatewayRoutesEnabled,
    bool PortalNavigationEnabled)
{
    public static PortalRuntimeFeatureFlags Disabled() => new(false, false, false, false, false);
}
