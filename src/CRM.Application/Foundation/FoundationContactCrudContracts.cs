using CRM.Domain.Enums;

namespace CRM.Application.Foundation;

public sealed record FoundationContactCreateRequest(
    string? FirstName,
    string? LastName,
    string? Email,
    string? Phone,
    string? Title,
    string? AccountId = null,
    PreferredContactMethod PreferredContactMethod = PreferredContactMethod.NotSpecified);

public sealed record FoundationContactUpdateRequest(
    string? FirstName,
    string? LastName,
    string? Email,
    string? Phone,
    string? Title,
    string? Status,
    string? AccountId = null,
    PreferredContactMethod PreferredContactMethod = PreferredContactMethod.NotSpecified);

public sealed record FoundationContactResponse(
    string Id,
    string? FirstName,
    string? LastName,
    string? Email,
    string? Phone,
    string? Title,
    string? AccountId,
    PreferredContactMethod PreferredContactMethod,
    string Status,
    bool FoundationMode,
    string PersistenceMode,
    bool DurablePersistence,
    bool ProductiveCrudEnabled,
    bool DatabaseConfigured,
    string AuthorizationMode,
    string Warning);
