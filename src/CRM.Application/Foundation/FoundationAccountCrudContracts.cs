namespace CRM.Application.Foundation;

public sealed record FoundationAccountCreateRequest(
    string? Name,
    string? Industry,
    string? Segment,
    string? Phone,
    string? Email);

public sealed record FoundationAccountUpdateRequest(
    string? Name,
    string? Industry,
    string? Segment,
    string? Phone,
    string? Email,
    string? Status);

public sealed record FoundationAccountResponse(
    string Id,
    string? Name,
    string? Industry,
    string? Segment,
    string? Phone,
    string? Email,
    string Status,
    bool FoundationMode,
    string PersistenceMode,
    bool DurablePersistence,
    bool ProductiveCrudEnabled,
    bool DatabaseConfigured,
    string AuthorizationMode,
    string Warning);
