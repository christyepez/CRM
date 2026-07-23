using CRM.Application.Portal;

namespace CRM.Infrastructure.Portal.Runtime;

public sealed class PortalAuthContextMapperPlaceholder
{
    public const string Warning = "Portal Auth runtime contract validation only; no real Auth runtime configured";

    public IReadOnlyCollection<CrmPortalAuthContextMappingContract> GetMappings() =>
        new CrmPortalAuthRuntimeContractStatusService().GetContextMappings();

    public bool Configured => false;

    public bool RuntimeConnected => false;
}
