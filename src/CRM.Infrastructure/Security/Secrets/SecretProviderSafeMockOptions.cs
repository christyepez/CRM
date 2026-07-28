namespace CRM.Infrastructure.Security.Secrets;

public sealed record SecretProviderSafeMockOptions(
    bool Enabled = true,
    bool NonProductionOnly = true,
    bool RuntimeUsable = false);
