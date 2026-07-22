namespace CRM.Application.Portal;

public sealed record PortalUserContextContract(
    string Module,
    string Status,
    string IntegrationMode,
    string RuntimeMode,
    string CapabilityOwner,
    bool Connected,
    string Warning,
    IReadOnlyCollection<string> RequiredCapabilities);

public sealed record PortalTenantContextContract(
    string Module,
    string Status,
    string IntegrationMode,
    string RuntimeMode,
    string CapabilityOwner,
    bool Connected,
    string Warning,
    IReadOnlyCollection<string> RequiredCapabilities);

public sealed record PortalPermissionContract(
    string Module,
    string Status,
    string IntegrationMode,
    string RuntimeMode,
    string CapabilityOwner,
    bool Connected,
    string Warning,
    IReadOnlyCollection<string> RequiredCapabilities);

public sealed record PortalMenuItemContract(
    string Module,
    string Status,
    string IntegrationMode,
    string RuntimeMode,
    string CapabilityOwner,
    bool Connected,
    string Warning,
    IReadOnlyCollection<string> RequiredCapabilities);

public sealed record PortalAuditEventContract(
    string Module,
    string Status,
    string IntegrationMode,
    string RuntimeMode,
    string CapabilityOwner,
    bool Connected,
    string Warning,
    IReadOnlyCollection<string> RequiredCapabilities);

public sealed record PortalNotificationContract(
    string Module,
    string Status,
    string IntegrationMode,
    string RuntimeMode,
    string CapabilityOwner,
    bool Connected,
    string Warning,
    IReadOnlyCollection<string> RequiredCapabilities);

public sealed record PortalConfigurationContract(
    string Module,
    string Status,
    string IntegrationMode,
    string RuntimeMode,
    string CapabilityOwner,
    bool Connected,
    string Warning,
    IReadOnlyCollection<string> RequiredCapabilities);

public sealed record PortalCorrelationContract(
    string Module,
    string Status,
    string IntegrationMode,
    string RuntimeMode,
    string CapabilityOwner,
    bool Connected,
    string Warning,
    IReadOnlyCollection<string> RequiredCapabilities);

public sealed record CrmPortalIntegrationStatusResponse(
    string Module,
    string Status,
    string IntegrationMode,
    string RuntimeMode,
    string CapabilityOwner,
    bool Connected,
    string Warning,
    IReadOnlyCollection<string> RequiredCapabilities,
    bool FoundationMode);
