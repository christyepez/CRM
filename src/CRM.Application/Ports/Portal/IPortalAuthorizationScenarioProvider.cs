using CRM.Application.Portal;

namespace CRM.Application.Ports.Portal;

/// <summary>
/// FuturePortalAdapter FoundationSimulation port for fictitious Portal authorization scenarios without Portal runtime or persisted roles.
/// </summary>
public interface IPortalAuthorizationScenarioProvider
{
    IReadOnlyCollection<CrmPortalAuthorizationScenarioContract> GetScenarios();

    IReadOnlyCollection<string> GetPermissions();
}
