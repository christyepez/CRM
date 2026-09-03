using CRM.Application.Persistence;
using CRM.Application.Ports.Persistence;
using CRM.Domain.ContactManagement;
using CRM.Domain.Enums;

namespace CRM.Application.ContactManagement;

public sealed class ContactManagementService(IContactFoundationStore store) : IContactManagementService
{
    private const string EntityName = "Contact";
    private const string FoundationStatus = "PreviewOnly";
    private const string PersistenceMode = "NonProductionSeam";

    public async Task<ContactManagementApplicationResult> CreateAsync(ContactManagementCreateApplicationRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var command = new ContactManagementCommand(
            ContactManagementOperation.Create,
            ContactId: null,
            request.Name,
            request.Email,
            request.Phone,
            request.Role,
            request.AccountId,
            request.PreferredContactMethod);

        var evaluation = ContactManagementPolicy.Evaluate(command);
        if (!evaluation.Success)
        {
            return ToApplicationResult(evaluation, contact: null);
        }

        var contactId = Guid.NewGuid().ToString("D");
        var preview = ToPreview(
            contactId,
            evaluation.NormalizedName!,
            evaluation.NormalizedEmail,
            evaluation.NormalizedPhone,
            evaluation.NormalizedRole,
            evaluation.NormalizedAccountId,
            evaluation.PreferredContactMethod,
            FoundationStatus);

        var saved = await store.SavePreviewAsync(preview, cancellationToken);

        return ToApplicationResult(evaluation with { ContactId = saved.Id }, ToContact(saved));
    }

    public async Task<ContactManagementApplicationResult> UpdateAsync(string contactId, ContactManagementUpdateApplicationRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var existing = await store.GetPreviewByIdAsync(contactId, cancellationToken);
        if (existing is null)
        {
            var notFound = ContactManagementRuleResult.Rejected(
                new ContactManagementCommand(
                    ContactManagementOperation.Update,
                    contactId,
                    request.Name,
                    request.Email,
                    request.Phone,
                    request.Role,
                    request.AccountId,
                    request.PreferredContactMethod),
                ContactManagementErrorCode.ContactNotFound,
                "Contact was not found.");

            return ToApplicationResult(notFound, contact: null);
        }

        var command = new ContactManagementCommand(
            ContactManagementOperation.Update,
            contactId,
            request.Name,
            request.Email,
            request.Phone,
            request.Role,
            request.AccountId,
            request.PreferredContactMethod,
            ToSnapshot(existing));

        var evaluation = ContactManagementPolicy.Evaluate(command);
        if (!evaluation.Success)
        {
            return ToApplicationResult(evaluation, contact: null);
        }

        if (!evaluation.Changed)
        {
            return ToApplicationResult(evaluation, ToContact(existing));
        }

        var preview = ToPreview(
            existing.Id,
            evaluation.NormalizedName!,
            evaluation.NormalizedEmail,
            evaluation.NormalizedPhone,
            evaluation.NormalizedRole,
            evaluation.NormalizedAccountId,
            evaluation.PreferredContactMethod,
            existing.Status);

        var saved = await store.SavePreviewAsync(preview, cancellationToken);

        return ToApplicationResult(evaluation, ToContact(saved));
    }

    private static ContactManagementApplicationResult ToApplicationResult(ContactManagementRuleResult evaluation, ContactManagementApplicationContact? contact) =>
        new(
            contact?.Id ?? evaluation.ContactId,
            evaluation.Operation,
            evaluation.Allowed,
            evaluation.Changed,
            evaluation.ErrorCode.ToString(),
            evaluation.Message,
            contact);

    private static CrmFoundationPreviewItemContract ToPreview(
        string id,
        string name,
        string? email,
        string? phone,
        string? role,
        string? accountId,
        PreferredContactMethod preferredContactMethod,
        string status) =>
        new(
            id,
            EntityName,
            name,
            status,
            DateTimeOffset.UtcNow,
            new Dictionary<string, string>
            {
                ["email"] = email ?? string.Empty,
                ["phone"] = phone ?? string.Empty,
                ["role"] = role ?? string.Empty,
                ["accountId"] = accountId ?? string.Empty,
                ["preferredContactMethod"] = preferredContactMethod.ToString()
            });

    private static ContactManagementApplicationContact ToContact(CrmFoundationPreviewItemContract preview) =>
        new(
            preview.Id,
            preview.DisplayName,
            Optional(preview.Metadata.GetValueOrDefault("email")),
            Optional(preview.Metadata.GetValueOrDefault("phone")),
            Optional(preview.Metadata.GetValueOrDefault("role")),
            Optional(preview.Metadata.GetValueOrDefault("accountId")),
            ParsePreferredContactMethod(preview.Metadata.GetValueOrDefault("preferredContactMethod")),
            preview.Status,
            PersistenceMode,
            ProductiveCrudEnabled: false);

    private static ContactManagementSnapshot ToSnapshot(CrmFoundationPreviewItemContract preview) =>
        new(
            preview.Id,
            preview.DisplayName,
            Optional(preview.Metadata.GetValueOrDefault("email")),
            Optional(preview.Metadata.GetValueOrDefault("phone")),
            Optional(preview.Metadata.GetValueOrDefault("role")),
            Optional(preview.Metadata.GetValueOrDefault("accountId")),
            ParsePreferredContactMethod(preview.Metadata.GetValueOrDefault("preferredContactMethod")),
            ContactStatus.Draft);

    private static PreferredContactMethod ParsePreferredContactMethod(string? value) =>
        Enum.TryParse<PreferredContactMethod>(value, ignoreCase: true, out var parsed)
            ? parsed
            : PreferredContactMethod.NotSpecified;

    private static string? Optional(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value;
}
