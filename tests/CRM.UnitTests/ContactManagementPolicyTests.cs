using CRM.Domain.ContactManagement;
using CRM.Domain.Enums;
using Xunit;

namespace CRM.UnitTests;

public sealed class ContactManagementPolicyTests
{
    [Fact]
    public void Create_AllowsValidContactAndNormalizesSafeFields()
    {
        var result = ContactManagementPolicy.Evaluate(new ContactManagementCommand(
            ContactManagementOperation.Create,
            ContactId: null,
            Name: "  Ada Lovelace  ",
            Email: "  ADA@EXAMPLE.TEST ",
            Phone: " 0999999999 ",
            Role: "  Buyer ",
            AccountId: null,
            PreferredContactMethod.Email));

        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal("Ada Lovelace", result.NormalizedName);
        Assert.Equal("ada@example.test", result.NormalizedEmail);
        Assert.Equal("0999999999", result.NormalizedPhone);
        Assert.Equal("Buyer", result.NormalizedRole);
    }

    [Fact]
    public void Create_RejectsMissingName()
    {
        var result = EvaluateCreate(name: " ");

        Assert.False(result.Success);
        Assert.Equal(ContactManagementErrorCode.NameRequired, result.ErrorCode);
    }

    [Fact]
    public void Create_RejectsInvalidEmail()
    {
        var result = EvaluateCreate(email: "not-an-email");

        Assert.False(result.Success);
        Assert.Equal(ContactManagementErrorCode.InvalidEmail, result.ErrorCode);
    }

    [Fact]
    public void Create_AllowsValidPhone()
    {
        var result = EvaluateCreate(phone: "022222222", preferredContactMethod: PreferredContactMethod.Phone);

        Assert.True(result.Success);
        Assert.Equal("022222222", result.NormalizedPhone);
    }

    [Fact]
    public void Create_RejectsPreferredEmailWithoutEmail()
    {
        var result = EvaluateCreate(email: null, preferredContactMethod: PreferredContactMethod.Email);

        Assert.False(result.Success);
        Assert.Equal(ContactManagementErrorCode.PreferredContactMethodRequiresEmail, result.ErrorCode);
    }

    [Fact]
    public void Create_RejectsPreferredPhoneWithoutPhone()
    {
        var result = EvaluateCreate(phone: null, preferredContactMethod: PreferredContactMethod.Phone);

        Assert.False(result.Success);
        Assert.Equal(ContactManagementErrorCode.PreferredContactMethodRequiresPhone, result.ErrorCode);
    }

    [Fact]
    public void Create_AcceptsOptionalAccountId()
    {
        var accountId = Guid.NewGuid().ToString("D");

        var result = EvaluateCreate(accountId: accountId);

        Assert.True(result.Success);
        Assert.Equal(accountId, result.NormalizedAccountId);
    }

    [Fact]
    public void Create_RejectsInvalidAccountIdFormat()
    {
        var result = EvaluateCreate(accountId: "account-123");

        Assert.False(result.Success);
        Assert.Equal(ContactManagementErrorCode.InvalidAccountReferenceFormat, result.ErrorCode);
    }

    [Fact]
    public void Update_AllowsValidModification()
    {
        var contactId = Guid.NewGuid().ToString("D");
        var existing = Snapshot(contactId);

        var result = ContactManagementPolicy.Evaluate(new ContactManagementCommand(
            ContactManagementOperation.Update,
            contactId,
            "Ada Byron",
            "ada.byron@example.test",
            "0999999999",
            "Decision Maker",
            null,
            PreferredContactMethod.Email,
            existing));

        Assert.True(result.Success);
        Assert.True(result.Changed);
        Assert.Equal("Ada Byron", result.NormalizedName);
    }

    [Fact]
    public void Update_ReturnsNoChangeForSameState()
    {
        var contactId = Guid.NewGuid().ToString("D");
        var existing = Snapshot(contactId);

        var result = ContactManagementPolicy.Evaluate(new ContactManagementCommand(
            ContactManagementOperation.Update,
            contactId,
            " Ada Lovelace ",
            "ADA@EXAMPLE.TEST",
            "0999999999",
            "Buyer",
            null,
            PreferredContactMethod.Email,
            existing));

        Assert.True(result.Success);
        Assert.False(result.Changed);
        Assert.Equal(ContactManagementErrorCode.None, result.ErrorCode);
    }

    [Fact]
    public void Update_RejectsContactIdChange()
    {
        var existing = Snapshot(Guid.NewGuid().ToString("D"));

        var result = ContactManagementPolicy.Evaluate(new ContactManagementCommand(
            ContactManagementOperation.Update,
            Guid.NewGuid().ToString("D"),
            "Ada Lovelace",
            "ada@example.test",
            "0999999999",
            "Buyer",
            null,
            PreferredContactMethod.Email,
            existing));

        Assert.False(result.Success);
        Assert.Equal(ContactManagementErrorCode.InvalidContactId, result.ErrorCode);
    }

    [Fact]
    public void Create_AllowsContactWithoutEmailOrPhoneWhenNoPreferenceIsSpecified()
    {
        var result = EvaluateCreate(email: null, phone: null, preferredContactMethod: PreferredContactMethod.NotSpecified);

        Assert.True(result.Success);
    }

    private static ContactManagementRuleResult EvaluateCreate(
        string? name = "Ada Lovelace",
        string? email = "ada@example.test",
        string? phone = null,
        string? role = "Buyer",
        string? accountId = null,
        PreferredContactMethod preferredContactMethod = PreferredContactMethod.NotSpecified) =>
        ContactManagementPolicy.Evaluate(new ContactManagementCommand(
            ContactManagementOperation.Create,
            ContactId: null,
            name,
            email,
            phone,
            role,
            accountId,
            preferredContactMethod));

    private static ContactManagementSnapshot Snapshot(string contactId) =>
        new(
            contactId,
            "Ada Lovelace",
            "ada@example.test",
            "0999999999",
            "Buyer",
            AccountId: null,
            PreferredContactMethod.Email,
            ContactStatus.Draft);
}
