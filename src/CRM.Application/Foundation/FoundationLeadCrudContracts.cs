namespace CRM.Application.Foundation;

public sealed record FoundationLeadCreateRequest(
    string? FirstName,
    string? LastName,
    string? Email,
    string? CompanyName,
    string? Title,
    string? Phone);

public sealed record FoundationLeadUpdateRequest(
    string? FirstName,
    string? LastName,
    string? Email,
    string? CompanyName,
    string? Title,
    string? Phone,
    string? Status);

public sealed record FoundationLeadResponse(
    string Id,
    string? FirstName,
    string? LastName,
    string? Email,
    string? CompanyName,
    string? Title,
    string? Phone,
    string Status,
    bool FoundationMode,
    string PersistenceMode,
    bool DurablePersistence,
    bool ProductiveCrudEnabled,
    bool DatabaseConfigured,
    string AuthorizationMode,
    string Warning);
