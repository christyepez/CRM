namespace CRM.Infrastructure.Portal.RuntimeProbe;

public sealed record PortalAuthTokenPropagationDryRunStatus(
    bool Exists,
    bool DryRunEnabled,
    bool TokenReadAttempted,
    bool HeaderReadAttempted,
    bool PortalHttpAttempted,
    string SyntheticTokenReference,
    string SyntheticUserReference,
    bool RuntimeUsable,
    string Warning);

public sealed class PortalAuthTokenPropagationDryRun
{
    public const string WarningText = "Portal Auth token propagation dry-run contract only; no real tokens or headers are read";

    private readonly PortalAuthTokenPropagationDryRunOptions options;

    public PortalAuthTokenPropagationDryRun()
        : this(new PortalAuthTokenPropagationDryRunOptions())
    {
    }

    public PortalAuthTokenPropagationDryRun(PortalAuthTokenPropagationDryRunOptions options)
    {
        this.options = options;
    }

    public PortalAuthTokenPropagationDryRunStatus GetStatus() =>
        new(
            true,
            false,
            false,
            false,
            false,
            options.SyntheticTokenReference,
            options.SyntheticUserReference,
            false,
            WarningText);
}
