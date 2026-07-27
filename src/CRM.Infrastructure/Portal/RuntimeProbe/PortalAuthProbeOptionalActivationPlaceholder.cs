namespace CRM.Infrastructure.Portal.RuntimeProbe;

public sealed record PortalAuthProbeOptionalActivationPlaceholderStatus(
    bool Exists,
    bool Enabled,
    bool ActivationApproved,
    bool PortalHttpAttempted,
    bool TokenReadAttempted,
    bool HeaderReadAttempted,
    string Strategy,
    string Warning);

public sealed class PortalAuthProbeOptionalActivationPlaceholder
{
    public const string WarningText = "Portal Auth probe optional activation only; no tokens are read and no Portal HTTP calls are attempted";

    public PortalAuthProbeOptionalActivationPlaceholderStatus GetStatus() =>
        new(
            true,
            false,
            false,
            false,
            false,
            false,
            "ContractOnly",
            WarningText);
}
