namespace CRM.Infrastructure.Persistence.RuntimeProbe;

public sealed record CommonDbRuntimeProbeOptions(
    bool Exists = true,
    bool Enabled = false,
    bool ConnectionAttempted = false,
    string Mode = "ContractOnlyRuntimeProbeDisabled");
