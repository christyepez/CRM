namespace CRM.Infrastructure.Portal.RuntimeProbe;

public sealed record PortalAuthRuntimeValidationProbeRequest(string BaseUrlSecretName, string ClientIdSecretName, string ClientSecretName);
