namespace CRM.Infrastructure.Portal.RuntimeProbe;

public sealed record PortalAuthRuntimeProbeOptions(
    bool Enabled = false,
    string Mode = "ContractOnlyRuntimeProbeDisabled");
