namespace CRM.Application.Foundation;

public sealed record LeadFoundationRequest(
    string FirstName,
    string LastName,
    string Email,
    string? CompanyName,
    string? Source);

public sealed record AccountFoundationRequest(
    string CompanyName,
    string? TaxId,
    string? Industry,
    string? Segment);

public sealed record ContactFoundationRequest(
    string FirstName,
    string LastName,
    string? Email,
    string? Phone,
    string? Role,
    string PreferredContactMethod);

public sealed record FoundationPreviewResponse(
    string Module,
    bool FoundationMode,
    string Persistence,
    string RuntimeMode,
    string Warning,
    string Entity,
    string Status,
    IReadOnlyCollection<string> AppliedRules);
