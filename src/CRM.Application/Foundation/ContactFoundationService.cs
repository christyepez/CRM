using CRM.Domain.Common;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using CRM.Domain.Foundation;
using CRM.Domain.ValueObjects;

namespace CRM.Application.Foundation;

public sealed class ContactFoundationService
{
    public FoundationPreviewResponse Preview(ContactFoundationRequest request)
    {
        var preferred = ParsePreferredContactMethod(request.PreferredContactMethod);
        var contact = Contact.Create(
            CrmId.New(),
            PersonName.From(request.FirstName, request.LastName),
            string.IsNullOrWhiteSpace(request.Email) ? null : EmailAddress.From(request.Email),
            string.IsNullOrWhiteSpace(request.Phone) ? null : PhoneNumber.From(request.Phone),
            ContactRole.Optional(request.Role),
            preferred);

        return new FoundationPreviewResponse(
            CrmFoundationStatus.Module,
            true,
            "None",
            CrmFoundationStatus.RuntimeMode,
            "Preview only, not persisted",
            "Contact",
            contact.Status.ToString(),
            ["Name validated", "Optional email/phone validated", "Preference rules previewed"]);
    }

    private static PreferredContactMethod ParsePreferredContactMethod(string value) =>
        Enum.TryParse<PreferredContactMethod>(value, ignoreCase: true, out var parsed)
            ? parsed
            : PreferredContactMethod.NotSpecified;
}
