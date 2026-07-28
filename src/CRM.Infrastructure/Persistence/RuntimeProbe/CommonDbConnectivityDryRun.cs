namespace CRM.Infrastructure.Persistence.RuntimeProbe;

public sealed record CommonDbConnectivityDryRunStatus(
    bool Exists,
    bool DryRunEnabled,
    bool ConnectionAttempted,
    string SyntheticConnectionReference,
    bool RuntimeUsable,
    string Warning);

public sealed class CommonDbConnectivityDryRun
{
    public const string SyntheticReference = "mock://crm/common-db";
    public const string WarningText = "Common DB connectivity dry-run contract only; no database connection is attempted";

    public CommonDbConnectivityDryRunStatus GetStatus() =>
        new(
            true,
            false,
            false,
            SyntheticReference,
            false,
            WarningText);
}
