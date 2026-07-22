namespace CRM.Application.Foundation;

public sealed record FoundationContactCreateRequest(
    string? FirstName,
    string? LastName,
    string? Email,
    string? Phone,
    string? Title);

public sealed record FoundationContactUpdateRequest(
    string? FirstName,
    string? LastName,
    string? Email,
    string? Phone,
    string? Title,
    string? Status);

public sealed record FoundationContactResponse(
    string Id,
    string? FirstName,
    string? LastName,
    string? Email,
    string? Phone,
    string? Title,
    string Status,
    bool FoundationMode,
    string PersistenceMode,
    bool DurablePersistence,
    bool ProductiveCrudEnabled,
    bool DatabaseConfigured,
    string AuthorizationMode,
    string Warning);
