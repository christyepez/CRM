namespace CRM.Infrastructure.Security.Secrets;

public sealed record SecretProviderRealNonProductionRuntimeProbeOptions(
    bool Exists = true,
    bool ApprovalGranted = false,
    bool ProbeEnabled = false,
    bool ProbeAttempted = false,
    bool RuntimeConnected = false,
    bool RealSecretReadAttempted = false,
    bool LogicalSecretNamesValidated = true,
    bool ProbeSkippedBecauseApprovalNotGranted = true);
