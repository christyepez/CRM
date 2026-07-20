namespace CRM.Domain.ValueObjects;

public sealed record LeadSource
{
    public string Value { get; }

    private LeadSource(string value) => Value = value;

    public static LeadSource From(string value)
    {
        var normalized = (value ?? string.Empty).Trim();
        return normalized.Length == 0
            ? throw new ArgumentException("Lead source is required.", nameof(value))
            : new LeadSource(normalized);
    }

    public override string ToString() => Value;
}

public sealed record DisqualificationReason
{
    public string Value { get; }

    private DisqualificationReason(string value) => Value = value;

    public static DisqualificationReason From(string value)
    {
        var normalized = (value ?? string.Empty).Trim();
        return normalized.Length < 3
            ? throw new ArgumentException("Disqualification reason must be at least 3 characters.", nameof(value))
            : new DisqualificationReason(normalized);
    }

    public override string ToString() => Value;
}

public sealed record TaxIdentifier
{
    public string Value { get; }

    private TaxIdentifier(string value) => Value = value;

    public static TaxIdentifier? Optional(string? value)
    {
        var normalized = (value ?? string.Empty).Trim();
        return normalized.Length == 0 ? null : new TaxIdentifier(normalized);
    }
}

public sealed record IndustryName
{
    public string Value { get; }

    private IndustryName(string value) => Value = value;

    public static IndustryName? Optional(string? value)
    {
        var normalized = (value ?? string.Empty).Trim();
        return normalized.Length == 0 ? null : new IndustryName(normalized);
    }
}

public sealed record AccountSegment
{
    public string Value { get; }

    private AccountSegment(string value) => Value = value;

    public static AccountSegment? Optional(string? value)
    {
        var normalized = (value ?? string.Empty).Trim();
        return normalized.Length == 0 ? null : new AccountSegment(normalized);
    }
}

public sealed record ContactRole
{
    public string Value { get; }

    private ContactRole(string value) => Value = value;

    public static ContactRole? Optional(string? value)
    {
        var normalized = (value ?? string.Empty).Trim();
        return normalized.Length == 0 ? null : new ContactRole(normalized);
    }
}
