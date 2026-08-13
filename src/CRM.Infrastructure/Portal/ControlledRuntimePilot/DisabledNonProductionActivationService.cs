using CRM.Application.Foundation;

namespace CRM.Infrastructure.Portal.ControlledRuntimePilot;

public sealed class DisabledNonProductionActivationService
{
    private readonly NonProductionActivationOptions _options;
    private readonly NonProductionActivationFeatureFlags _featureFlags;

    public DisabledNonProductionActivationService(
        NonProductionActivationOptions options,
        NonProductionActivationFeatureFlags featureFlags)
    {
        _options = options;
        _featureFlags = featureFlags;
    }

    public CrmControlledRuntimePilotFirstSliceNonProductionActivationDryRunResult GetDryRunResult() =>
        new(
            DryRunOnly: true,
            ActivationAttempted: false,
            ActivationExecuted: false,
            ExternalCallAttempted: false,
            PortalCouplingEnabled: false,
            FeatureFlagsEnabled: _options.Enabled || _options.ActivationDryRunEnabled || _featureFlags.AnyEnabled,
            Status: "Locked",
            Warning: "NonProduction activation scaffold is disabled-by-default and performs no external calls");
}
