namespace CRM.Infrastructure.Portal.RuntimeProbe;

public sealed record PortalAuthTokenPropagationDryRunOptions(
    bool Enabled = false,
    string SyntheticTokenReference = "mock://crm/portal-auth-token",
    string SyntheticUserReference = "mock://crm/portal-user");
