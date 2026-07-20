using CRM.Domain.Common;
using CRM.Domain.Enums;
using CRM.Domain.ValueObjects;

namespace CRM.Domain.Entities;

public sealed class Account
{
    private readonly HashSet<CrmId> _contactReferences = [];

    private Account(CrmId id, CompanyName name, TaxIdentifier? taxId, IndustryName? industry, AccountSegment? segment)
    {
        Id = id;
        Name = name;
        TaxId = taxId;
        Industry = industry;
        Segment = segment;
        Status = AccountStatus.Draft;
    }

    public CrmId Id { get; }
    public CompanyName Name { get; }
    public TaxIdentifier? TaxId { get; }
    public IndustryName? Industry { get; }
    public AccountSegment? Segment { get; }
    public AccountStatus Status { get; private set; }
    public IReadOnlyCollection<CrmId> ContactReferences => _contactReferences.ToArray();

    public static Account Create(CrmId id, CompanyName name, TaxIdentifier? taxId = null, IndustryName? industry = null, AccountSegment? segment = null) =>
        new(id, name, taxId, industry, segment);

    public void AddContactReference(CrmId contactId)
    {
        if (!_contactReferences.Add(contactId))
        {
            throw new InvalidOperationException("Contact is already related to the account.");
        }
    }

    public void MarkActive() => Status = AccountStatus.Active;

    public void MarkInactive() => Status = AccountStatus.Inactive;
}

public sealed class Contact
{
    private Contact(CrmId id, PersonName name, EmailAddress? email, PhoneNumber? phone, ContactRole? role, PreferredContactMethod preferredContactMethod)
    {
        if (preferredContactMethod == PreferredContactMethod.Email && email is null)
        {
            throw new ArgumentException("Preferred email contact requires an email address.", nameof(preferredContactMethod));
        }

        if (preferredContactMethod == PreferredContactMethod.Phone && phone is null)
        {
            throw new ArgumentException("Preferred phone contact requires a phone number.", nameof(preferredContactMethod));
        }

        Id = id;
        Name = name;
        Email = email;
        Phone = phone;
        Role = role;
        PreferredContactMethod = preferredContactMethod;
        Status = ContactStatus.Draft;
    }

    public CrmId Id { get; }
    public PersonName Name { get; }
    public EmailAddress? Email { get; private set; }
    public PhoneNumber? Phone { get; private set; }
    public ContactRole? Role { get; }
    public CrmId? AccountId { get; private set; }
    public PreferredContactMethod PreferredContactMethod { get; private set; }
    public ContactStatus Status { get; private set; }

    public static Contact Create(CrmId id, PersonName name, EmailAddress? email = null, PhoneNumber? phone = null, ContactRole? role = null, PreferredContactMethod preferredContactMethod = PreferredContactMethod.NotSpecified) =>
        new(id, name, email, phone, role, preferredContactMethod);

    public void AssignToAccount(CrmId accountId)
    {
        AccountId = accountId;
        Status = ContactStatus.Active;
    }

    public void UpdateContactPreferences(EmailAddress? email, PhoneNumber? phone, PreferredContactMethod preferredContactMethod)
    {
        if (preferredContactMethod == PreferredContactMethod.Email && email is null)
        {
            throw new ArgumentException("Preferred email contact requires an email address.", nameof(preferredContactMethod));
        }

        if (preferredContactMethod == PreferredContactMethod.Phone && phone is null)
        {
            throw new ArgumentException("Preferred phone contact requires a phone number.", nameof(preferredContactMethod));
        }

        Email = email;
        Phone = phone;
        PreferredContactMethod = preferredContactMethod;
    }
}

public sealed record Pipeline(CrmId Id, string Name, IReadOnlyCollection<PipelineStage> Stages);

public sealed record PipelineStage(CrmId Id, string Name, int Order);

public sealed record Note(CrmId Id, CrmId RelatedEntityId, string Text);

public sealed record Campaign(CrmId Id, string Name, DateRange ActiveRange);

public sealed record Segment(CrmId Id, string Name, string CriteriaSummary);
