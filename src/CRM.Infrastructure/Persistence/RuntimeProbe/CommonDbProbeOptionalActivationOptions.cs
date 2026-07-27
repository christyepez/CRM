namespace CRM.Infrastructure.Persistence.RuntimeProbe;

public sealed record CommonDbProbeOptionalActivationOptions(
    bool Exists = true,
    bool Enabled = false,
    bool ActivationApproved = false,
    bool ConnectionAttempted = false,
    string Strategy = "ContractOnly");
