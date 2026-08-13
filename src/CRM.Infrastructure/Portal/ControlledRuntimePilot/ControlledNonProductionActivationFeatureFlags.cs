namespace CRM.Infrastructure.Portal.ControlledRuntimePilot;

public sealed record ControlledNonProductionActivationFeatureFlags(
    bool FirstSliceEnabled,
    bool PortalClientEnabled,
    bool DryRunEnabled,
    bool GatewayRoutesEnabled,
    bool PortalNavigationEnabled)
{
    public static ControlledNonProductionActivationFeatureFlags Disabled() => new(false, false, false, false, false);

    public bool AnyEnabled => FirstSliceEnabled || PortalClientEnabled || DryRunEnabled || GatewayRoutesEnabled || PortalNavigationEnabled;
}
