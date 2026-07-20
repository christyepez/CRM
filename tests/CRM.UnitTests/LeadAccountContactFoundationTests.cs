using CRM.Domain.Common;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using CRM.Domain.ValueObjects;
using Xunit;

namespace CRM.UnitTests;

public sealed class LeadAccountContactFoundationTests
{
    [Fact]
    public void Lead_Transitions_ContactQualifyConvert()
    {
        var lead = CreateLead();

        lead.MarkContacted();
        lead.Qualify();
        lead.Convert();

        Assert.Equal(LeadStatus.Converted, lead.Status);
    }

    [Fact]
    public void Lead_BlocksInvalidQualificationFromNew()
    {
        var lead = CreateLead();

        Assert.Throws<InvalidOperationException>(lead.Qualify);
    }

    [Fact]
    public void Lead_Disqualify_StoresReason()
    {
        var lead = CreateLead();

        lead.Disqualify(DisqualificationReason.From("No current interest"));

        Assert.Equal(LeadStatus.Disqualified, lead.Status);
        Assert.NotNull(lead.DisqualificationReason);
    }

    [Fact]
    public void Account_AddContactReference_PreventsDuplicates()
    {
        var account = Account.Create(CrmId.New(), CompanyName.From("Example Co"));
        var contactId = CrmId.New();

        account.AddContactReference(contactId);

        Assert.Throws<InvalidOperationException>(() => account.AddContactReference(contactId));
    }

    [Fact]
    public void Account_CanActivateAndInactivate()
    {
        var account = Account.Create(CrmId.New(), CompanyName.From("Example Co"));

        account.MarkActive();
        Assert.Equal(AccountStatus.Active, account.Status);

        account.MarkInactive();
        Assert.Equal(AccountStatus.Inactive, account.Status);
    }

    [Fact]
    public void Contact_AssignToAccount_ActivatesContact()
    {
        var contact = Contact.Create(
            CrmId.New(),
            PersonName.From("Grace", "Hopper"),
            EmailAddress.From("grace@example.test"));

        var accountId = CrmId.New();
        contact.AssignToAccount(accountId);

        Assert.Equal(accountId, contact.AccountId);
        Assert.Equal(ContactStatus.Active, contact.Status);
    }

    [Fact]
    public void Contact_RequiresEmailWhenPreferredMethodIsEmail()
    {
        Assert.Throws<ArgumentException>(() => Contact.Create(
            CrmId.New(),
            PersonName.From("Grace", "Hopper"),
            preferredContactMethod: PreferredContactMethod.Email));
    }

    [Fact]
    public void Contact_UpdatePreferences_ValidatesPreferredMethod()
    {
        var contact = Contact.Create(CrmId.New(), PersonName.From("Grace", "Hopper"));

        contact.UpdateContactPreferences(
            EmailAddress.From("grace@example.test"),
            null,
            PreferredContactMethod.Email);

        Assert.Equal(PreferredContactMethod.Email, contact.PreferredContactMethod);
    }

    private static Lead CreateLead() =>
        Lead.Create(
            CrmId.New(),
            PersonName.From("Ada", "Lovelace"),
            EmailAddress.From("ada@example.test"),
            CompanyName.From("Example Co"),
            new DateTimeOffset(2026, 7, 20, 0, 0, 0, TimeSpan.Zero),
            LeadSource.From("Website"));
}
