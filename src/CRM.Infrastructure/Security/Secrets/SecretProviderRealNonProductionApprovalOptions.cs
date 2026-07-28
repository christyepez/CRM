namespace CRM.Infrastructure.Security.Secrets;

public sealed record SecretProviderRealNonProductionApprovalOptions(
    bool Exists = true,
    bool ApprovalGranted = false,
    bool RuntimeEnabled = false,
    bool RuntimeConnected = false,
    bool RealSecretReadAttempted = false);
