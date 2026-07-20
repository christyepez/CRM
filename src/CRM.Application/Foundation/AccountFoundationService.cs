using CRM.Domain.Common;
using CRM.Domain.Entities;
using CRM.Domain.Foundation;
using CRM.Domain.ValueObjects;

namespace CRM.Application.Foundation;

public sealed class AccountFoundationService
{
    public FoundationPreviewResponse Preview(AccountFoundationRequest request)
    {
        var account = Account.Create(
            CrmId.New(),
            CompanyName.From(request.CompanyName),
            TaxIdentifier.Optional(request.TaxId),
            IndustryName.Optional(request.Industry),
            AccountSegment.Optional(request.Segment));

        account.MarkActive();

        return new FoundationPreviewResponse(
            CrmFoundationStatus.Module,
            true,
            "None",
            CrmFoundationStatus.RuntimeMode,
            "Preview only, not persisted",
            "Account",
            account.Status.ToString(),
            ["Company name validated", "Optional classification accepted", "Activation previewed"]);
    }
}
