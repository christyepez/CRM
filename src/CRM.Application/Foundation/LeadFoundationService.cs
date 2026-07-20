using CRM.Domain.Common;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using CRM.Domain.Foundation;
using CRM.Domain.ValueObjects;

namespace CRM.Application.Foundation;

public sealed class LeadFoundationService
{
    public FoundationPreviewResponse Preview(LeadFoundationRequest request)
    {
        var lead = Lead.Create(
            CrmId.New(),
            PersonName.From(request.FirstName, request.LastName),
            EmailAddress.From(request.Email),
            string.IsNullOrWhiteSpace(request.CompanyName) ? null : CompanyName.From(request.CompanyName),
            new DateTimeOffset(2026, 7, 20, 0, 0, 0, TimeSpan.Zero),
            string.IsNullOrWhiteSpace(request.Source) ? null : LeadSource.From(request.Source));

        lead.MarkContacted();

        return new FoundationPreviewResponse(
            CrmFoundationStatus.Module,
            true,
            "None",
            CrmFoundationStatus.RuntimeMode,
            "Preview only, not persisted",
            "Lead",
            LeadStatus.Contacted.ToString(),
            ["Email validated", "Name validated", "Transition New to Contacted previewed"]);
    }
}
