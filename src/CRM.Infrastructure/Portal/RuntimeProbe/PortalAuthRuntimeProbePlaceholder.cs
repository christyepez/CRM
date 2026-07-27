namespace CRM.Infrastructure.Portal.RuntimeProbe;

public sealed record PortalAuthRuntimeProbePlaceholderStatus(
    bool Exists,
    bool Enabled,
    bool TokenReadAttempted,
    bool PortalHttpAttempted,
    string Mode,
    string Warning);

public sealed class PortalAuthRuntimeProbePlaceholder
{
    public const string WarningText = "Portal Auth runtime probe exists but is disabled; no tokens are read and no Portal HTTP calls are attempted";

    public PortalAuthRuntimeProbePlaceholderStatus GetStatus() =>
        new(true, false, false, false, "ContractOnlyRuntimeProbeDisabled", WarningText);
}
