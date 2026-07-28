namespace CRM.Infrastructure.Persistence.RuntimeProbe;

public sealed record CommonDbConnectivityDryRunOptions(
    bool Exists = true,
    bool DryRunEnabled = false,
    bool RuntimeUsable = false,
    string SyntheticConnectionReference = "mock://crm/common-db");
