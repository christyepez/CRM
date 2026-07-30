namespace CRM.Infrastructure.Portal.Auth;

public sealed record PortalAuthRuntimeValidationTrialOptions(
    bool Enabled,
    string RuntimeEnvironment,
    string BaseUrlSecretName,
    string ClientIdSecretName,
    string ClientSecretName);
