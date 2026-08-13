namespace CRM.Infrastructure.Portal.ControlledRuntimePilot;

public sealed record ControlledNonProductionActivationOptions(
    bool Enabled,
    bool DryRunEnabled,
    string RuntimeEnvironment,
    string PortalBaseLogicalName,
    string ApprovalReferenceLogicalName)
{
    public static ControlledNonProductionActivationOptions Disabled() =>
        new(
            Enabled: false,
            DryRunEnabled: false,
            RuntimeEnvironment: "NonProduction",
            PortalBaseLogicalName: "logical-portal-placeholder",
            ApprovalReferenceLogicalName: "logical-approval-reference-placeholder");
}
