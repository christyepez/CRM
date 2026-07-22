using CRM.Application.Portal;

namespace CRM.Application.Foundation;

public sealed record FoundationCrudOperationResult<T>(
    bool Allowed,
    string Operation,
    string Entity,
    T? Data,
    string RequiredPermission,
    CrmPortalPermissionCheckContract PermissionSimulation,
    bool FoundationMode,
    string PersistenceMode,
    bool DurablePersistence,
    bool ProductiveCrudEnabled,
    bool DatabaseConfigured,
    string AuthorizationMode,
    string Warning);

public sealed record FoundationCrudStatusResponse(
    string Module,
    string Status,
    bool FoundationCrudEnabled,
    bool LeadFoundationCrudEnabled,
    bool AccountFoundationCrudEnabled,
    bool ContactFoundationCrudEnabled,
    bool FoundationMode,
    string PersistenceMode,
    bool DurablePersistence,
    bool ProductiveCrudEnabled,
    bool DatabaseConfigured,
    bool PortalRuntimeConnected,
    string AuthorizationMode,
    string NextGate,
    string Warning);
