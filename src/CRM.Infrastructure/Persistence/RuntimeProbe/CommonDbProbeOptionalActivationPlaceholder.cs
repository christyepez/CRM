namespace CRM.Infrastructure.Persistence.RuntimeProbe;

public sealed record CommonDbProbeOptionalActivationPlaceholderStatus(
    bool Exists,
    bool Enabled,
    bool ActivationApproved,
    bool ConnectionAttempted,
    string Strategy,
    string Warning);

public sealed class CommonDbProbeOptionalActivationPlaceholder
{
    public const string WarningText = "Common DB probe optional activation only; no database connection is attempted";

    public CommonDbProbeOptionalActivationPlaceholderStatus GetStatus() =>
        new(
            true,
            false,
            false,
            false,
            "ContractOnly",
            WarningText);
}
