namespace CRM.Infrastructure.Persistence.RuntimeProbe;

public sealed record CommonDbRuntimeProbePlaceholderStatus(
    bool Exists,
    bool Enabled,
    bool ConnectionAttempted,
    string Mode,
    string Warning);

public sealed class CommonDbRuntimeProbePlaceholder
{
    public const string WarningText = "Common DB runtime probe exists but is disabled; no database connection is attempted";

    public CommonDbRuntimeProbePlaceholderStatus GetStatus() =>
        new(true, false, false, "ContractOnlyRuntimeProbeDisabled", WarningText);
}
