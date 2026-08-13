using CRM.Application.Foundation;

namespace CRM.Infrastructure.Portal.ControlledRuntimePilot;

public sealed class DisabledControlledNonProductionActivationService
{
    private readonly ControlledNonProductionActivationOptions _options;
    private readonly ControlledNonProductionActivationFeatureFlags _featureFlags;

    public DisabledControlledNonProductionActivationService(
        ControlledNonProductionActivationOptions options,
        ControlledNonProductionActivationFeatureFlags featureFlags)
    {
        _options = options;
        _featureFlags = featureFlags;
    }

    public CrmControlledRuntimePilotFirstSliceNonProductionActivationControlledDryRunResult GetDryRunResult(
        CrmControlledRuntimePilotFirstSliceNonProductionActivationControlledDryRunRequest? request = null) =>
        new(
            DryRunOnly: true,
            ControlledImplementationPrepared: true,
            ControlledImplementationExecuted: false,
            ActivationAttempted: false,
            ActivationExecuted: false,
            ExternalCallAttempted: false,
            PortalCouplingEnabled: false,
            FeatureFlagsEnabled: _options.Enabled || _options.DryRunEnabled || _featureFlags.AnyEnabled,
            ApprovalReferenceAccepted: !string.IsNullOrWhiteSpace(request?.ApprovalReference),
            Status: "Locked",
            Warning: "Controlled NonProduction activation implementation is disabled-by-default and performs no external calls");
}
