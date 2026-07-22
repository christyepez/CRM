using CRM.Application.Persistence;
using CRM.Application.Portal;
using CRM.Application.Ports.Persistence;

namespace CRM.Application.Foundation;

public sealed class FoundationLeadCrudService(
    ILeadFoundationStore store,
    ICrmPersistenceFeatureFlagProvider flags,
    CrmFoundationPermissionGuard permissionGuard)
{
    public async Task<FoundationCrudOperationResult<IReadOnlyCollection<FoundationLeadResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        FoundationCrudSupport.EnsureSafeFlags(flags);
        var permission = await permissionGuard.CheckAsync("crm.foundation.read", cancellationToken);
        IReadOnlyCollection<FoundationLeadResponse> data = permission.Allowed ? (await store.GetPreviewAsync(cancellationToken)).Select(ToResponse).ToArray() : [];
        return FoundationCrudSupport.Result<IReadOnlyCollection<FoundationLeadResponse>>("Read", "Lead", data, "crm.foundation.read", permission);
    }

    public async Task<FoundationCrudOperationResult<FoundationLeadResponse>> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        FoundationCrudSupport.EnsureSafeFlags(flags);
        var permission = await permissionGuard.CheckAsync("crm.foundation.read", cancellationToken);
        var item = permission.Allowed ? await store.GetPreviewByIdAsync(id, cancellationToken) : null;
        return FoundationCrudSupport.Result("ReadById", "Lead", item is null ? null : ToResponse(item), "crm.foundation.read", permission);
    }

    public async Task<FoundationCrudOperationResult<FoundationLeadResponse>> CreateAsync(FoundationLeadCreateRequest request, CancellationToken cancellationToken = default)
    {
        FoundationCrudSupport.EnsureSafeFlags(flags);
        var permission = await permissionGuard.CheckAsync("crm.foundation.preview.write", cancellationToken);
        var response = FromCreate($"lead-foundation-{Guid.NewGuid():N}"[..28], request);
        if (permission.Allowed)
        {
            await store.SavePreviewAsync(ToPreview(response), cancellationToken);
        }

        return FoundationCrudSupport.Result("Create", "Lead", response, "crm.foundation.preview.write", permission);
    }

    public async Task<FoundationCrudOperationResult<FoundationLeadResponse>> UpdateAsync(string id, FoundationLeadUpdateRequest request, CancellationToken cancellationToken = default)
    {
        FoundationCrudSupport.EnsureSafeFlags(flags);
        var permission = await permissionGuard.CheckAsync("crm.foundation.preview.write", cancellationToken);
        var existing = await store.GetPreviewByIdAsync(id, cancellationToken);
        var response = FromUpdate(id, request, existing);
        if (permission.Allowed)
        {
            await store.SavePreviewAsync(ToPreview(response), cancellationToken);
        }

        return FoundationCrudSupport.Result("Update", "Lead", response, "crm.foundation.preview.write", permission);
    }

    private static FoundationLeadResponse FromCreate(string id, FoundationLeadCreateRequest request) =>
        new(
            id,
            FoundationCrudSupport.CleanOptional(request.FirstName),
            FoundationCrudSupport.CleanOptional(request.LastName),
            FoundationCrudSupport.CleanOptional(request.Email),
            FoundationCrudSupport.CleanOptional(request.CompanyName),
            FoundationCrudSupport.CleanOptional(request.Title),
            FoundationCrudSupport.CleanOptional(request.Phone),
            "PreviewOnly",
            true,
            "NonProductionSeam",
            false,
            false,
            false,
            "FoundationSimulation",
            FoundationCrudSupport.WarningText);

    private static FoundationLeadResponse FromUpdate(string id, FoundationLeadUpdateRequest request, CrmFoundationPreviewItemContract? existing) =>
        new(
            id,
            FoundationCrudSupport.CleanOptional(request.FirstName),
            FoundationCrudSupport.CleanOptional(request.LastName),
            FoundationCrudSupport.CleanOptional(request.Email),
            FoundationCrudSupport.CleanOptional(request.CompanyName),
            FoundationCrudSupport.CleanOptional(request.Title),
            FoundationCrudSupport.CleanOptional(request.Phone),
            FoundationCrudSupport.Clean(request.Status, existing?.Status ?? "PreviewOnly"),
            true,
            "NonProductionSeam",
            false,
            false,
            false,
            "FoundationSimulation",
            FoundationCrudSupport.WarningText);

    private static CrmFoundationPreviewItemContract ToPreview(FoundationLeadResponse response) =>
        FoundationCrudSupport.ToPreview(
            response.Id,
            "Lead",
            FoundationCrudSupport.Clean($"{response.FirstName} {response.LastName}", "Lead Foundation Preview"),
            response.Status,
            new Dictionary<string, string>
            {
                ["email"] = response.Email ?? "",
                ["companyName"] = response.CompanyName ?? "",
                ["title"] = response.Title ?? "",
                ["phone"] = response.Phone ?? ""
            });

    private static FoundationLeadResponse ToResponse(CrmFoundationPreviewItemContract item) =>
        new(
            item.Id,
            item.DisplayName.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(),
            item.DisplayName.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).FirstOrDefault(),
            item.Metadata.GetValueOrDefault("email"),
            item.Metadata.GetValueOrDefault("companyName"),
            item.Metadata.GetValueOrDefault("title"),
            item.Metadata.GetValueOrDefault("phone"),
            item.Status,
            true,
            "NonProductionSeam",
            false,
            false,
            false,
            "FoundationSimulation",
            FoundationCrudSupport.WarningText);
}
