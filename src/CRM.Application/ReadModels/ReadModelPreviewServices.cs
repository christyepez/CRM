using CRM.Domain.Enums;
using CRM.Domain.Foundation;

namespace CRM.Application.ReadModels;

public sealed class LeadReadModelPreviewService
{
    public CrmReadModelResponse<LeadReadModel> Preview(CrmReadModelQuery query) =>
        new(
            CrmFoundationStatus.Module,
            true,
            "FoundationMock",
            "None",
            CrmFoundationStatus.RuntimeMode,
            "Read model preview only, not persisted",
            [new LeadReadModel("foundation-lead-1", "Ada Lovelace", "ada@example.test", "Example Co", LeadStatus.Contacted)],
            1);
}

public sealed class AccountReadModelPreviewService
{
    public CrmReadModelResponse<AccountReadModel> Preview(CrmReadModelQuery query) =>
        new(
            CrmFoundationStatus.Module,
            true,
            "FoundationMock",
            "None",
            CrmFoundationStatus.RuntimeMode,
            "Read model preview only, not persisted",
            [new AccountReadModel("foundation-account-1", "Example Co", "Technology", "Enterprise", AccountStatus.Active)],
            1);
}

public sealed class ContactReadModelPreviewService
{
    public CrmReadModelResponse<ContactReadModel> Preview(CrmReadModelQuery query) =>
        new(
            CrmFoundationStatus.Module,
            true,
            "FoundationMock",
            "None",
            CrmFoundationStatus.RuntimeMode,
            "Read model preview only, not persisted",
            [new ContactReadModel("foundation-contact-1", "Grace Hopper", "grace@example.test", null, "Decision maker", ContactStatus.Draft)],
            1);
}

public sealed class CrmReadModelStatusService
{
    public CrmReadModelStatusResponse GetStatus() =>
        new(
            CrmFoundationStatus.Module,
            true,
            "FoundationMock",
            "None",
            CrmFoundationStatus.RuntimeMode,
            "Read model preview only, not persisted",
            "Draft",
            [
                "Shared local SQL strategy approved",
                "Portal Security and Audit adapters approved",
                "Migration ownership accepted",
                "No duplicate customer master data decision accepted"
            ]);
}
