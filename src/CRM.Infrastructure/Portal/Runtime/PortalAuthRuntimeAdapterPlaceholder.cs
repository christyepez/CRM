using CRM.Application.Portal;

namespace CRM.Infrastructure.Portal.Runtime;

public sealed class PortalAuthRuntimeAdapterPlaceholder
{
    public const string Warning = "Portal Auth runtime contract validation only; no real Auth runtime configured";

    public CrmPortalAuthRuntimeContractStatusResponse GetStatus() =>
        new CrmPortalAuthRuntimeContractStatusService().GetStatus();

    public bool Configured => false;

    public bool RuntimeConnected => false;
}
