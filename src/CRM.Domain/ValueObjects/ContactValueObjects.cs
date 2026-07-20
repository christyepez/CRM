using System.Text.RegularExpressions;

namespace CRM.Domain.ValueObjects;

public sealed record EmailAddress
{
    private static readonly Regex EmailPattern = new("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$", RegexOptions.Compiled);

    public string Value { get; }

    private EmailAddress(string value) => Value = value;

    public static EmailAddress From(string value)
    {
        var normalized = (value ?? string.Empty).Trim().ToLowerInvariant();
        return EmailPattern.IsMatch(normalized)
            ? new EmailAddress(normalized)
            : throw new ArgumentException("Email address is invalid.", nameof(value));
    }

    public override string ToString() => Value;
}

public sealed record PhoneNumber
{
    public string Value { get; }

    private PhoneNumber(string value) => Value = value;

    public static PhoneNumber From(string value)
    {
        var normalized = (value ?? string.Empty).Trim();
        return normalized.Length is >= 7 and <= 24
            ? new PhoneNumber(normalized)
            : throw new ArgumentException("Phone number length is invalid.", nameof(value));
    }

    public override string ToString() => Value;
}

public sealed record PersonName
{
    public string FirstName { get; }
    public string LastName { get; }

    private PersonName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static PersonName From(string firstName, string lastName)
    {
        var first = (firstName ?? string.Empty).Trim();
        var last = (lastName ?? string.Empty).Trim();
        if (first.Length == 0 || last.Length == 0)
        {
            throw new ArgumentException("Person name requires first and last name.");
        }

        return new PersonName(first, last);
    }

    public override string ToString() => $"{FirstName} {LastName}";
}

public sealed record CompanyName
{
    public string Value { get; }

    private CompanyName(string value) => Value = value;

    public static CompanyName From(string value)
    {
        var normalized = (value ?? string.Empty).Trim();
        return normalized.Length >= 2
            ? new CompanyName(normalized)
            : throw new ArgumentException("Company name is required.", nameof(value));
    }

    public override string ToString() => Value;
}
