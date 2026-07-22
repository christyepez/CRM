using CRM.Application.Persistence;
using CRM.Application.Portal;
using CRM.Application.Ports.Persistence;

namespace CRM.Application.Foundation;

public sealed class FoundationAccountCrudService(
    IAccountFoundationStore store,
    ICrmPersistenceFeatureFlagProvider flags,
    CrmFoundationPermissionGuard permissionGuard)
{
    public async Task<FoundationCrudOperationResult<IReadOnlyCollection<FoundationAccountResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        FoundationCrudSupport.EnsureSafeFlags(flags);
        var permission = await permissionGuard.CheckAsync("crm.foundation.read", cancellationToken);
        IReadOnlyCollection<FoundationAccountResponse> data = permission.Allowed ? (await store.GetPreviewAsync(cancellationToken)).Select(ToResponse).ToArray() : [];
        return FoundationCrudSupport.Result<IReadOnlyCollection<FoundationAccountResponse>>("Read", "Account", data, "crm.foundation.read", permission);
    }

    public async Task<FoundationCrudOperationResult<FoundationAccountResponse>> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        FoundationCrudSupport.EnsureSafeFlags(flags);
        var permission = await permissionGuard.CheckAsync("crm.foundation.read", cancellationToken);
        var item = permission.Allowed ? await store.GetPreviewByIdAsync(id, cancellationToken) : null;
        return FoundationCrudSupport.Result("ReadById", "Account", item is null ? null : ToResponse(item), "crm.foundation.read", permission);
    }

    public async Task<FoundationCrudOperationResult<FoundationAccountResponse>> CreateAsync(FoundationAccountCreateRequest request, CancellationToken cancellationToken = default)
    {
        FoundationCrudSupport.EnsureSafeFlags(flags);
        var permission = await permissionGuard.CheckAsync("crm.foundation.preview.write", cancellationToken);
        var response = FromCreate($"account-foundation-{Guid.NewGuid():N}"[..31], request);
        if (permission.Allowed)
        {
            await store.SavePreviewAsync(ToPreview(response), cancellationToken);
        }

        return FoundationCrudSupport.Result("Create", "Account", response, "crm.foundation.preview.write", permission);
    }

    public async Task<FoundationCrudOperationResult<FoundationAccountResponse>> UpdateAsync(string id, FoundationAccountUpdateRequest request, CancellationToken cancellationToken = default)
    {
        FoundationCrudSupport.EnsureSafeFlags(flags);
        var permission = await permissionGuard.CheckAsync("crm.foundation.preview.write", cancellationToken);
        var existing = await store.GetPreviewByIdAsync(id, cancellationToken);
        var response = FromUpdate(id, request, existing);
        if (permission.Allowed)
        {
            await store.SavePreviewAsync(ToPreview(response), cancellationToken);
        }

        return FoundationCrudSupport.Result("Update", "Account", response, "crm.foundation.preview.write", permission);
    }

    private static FoundationAccountResponse FromCreate(string id, FoundationAccountCreateRequest request) =>
        new(id, FoundationCrudSupport.CleanOptional(request.Name), FoundationCrudSupport.CleanOptional(request.Industry), FoundationCrudSupport.CleanOptional(request.Segment), FoundationCrudSupport.CleanOptional(request.Phone), FoundationCrudSupport.CleanOptional(request.Email), "PreviewOnly", true, "NonProductionSeam", false, false, false, "FoundationSimulation", FoundationCrudSupport.WarningText);

    private static FoundationAccountResponse FromUpdate(string id, FoundationAccountUpdateRequest request, CrmFoundationPreviewItemContract? existing) =>
        new(id, FoundationCrudSupport.CleanOptional(request.Name), FoundationCrudSupport.CleanOptional(request.Industry), FoundationCrudSupport.CleanOptional(request.Segment), FoundationCrudSupport.CleanOptional(request.Phone), FoundationCrudSupport.CleanOptional(request.Email), FoundationCrudSupport.Clean(request.Status, existing?.Status ?? "PreviewOnly"), true, "NonProductionSeam", false, false, false, "FoundationSimulation", FoundationCrudSupport.WarningText);

    private static CrmFoundationPreviewItemContract ToPreview(FoundationAccountResponse response) =>
        FoundationCrudSupport.ToPreview(response.Id, "Account", FoundationCrudSupport.Clean(response.Name, "Account Foundation Preview"), response.Status, new Dictionary<string, string> { ["industry"] = response.Industry ?? "", ["segment"] = response.Segment ?? "", ["phone"] = response.Phone ?? "", ["email"] = response.Email ?? "" });

    private static FoundationAccountResponse ToResponse(CrmFoundationPreviewItemContract item) =>
        new(item.Id, item.DisplayName, item.Metadata.GetValueOrDefault("industry"), item.Metadata.GetValueOrDefault("segment"), item.Metadata.GetValueOrDefault("phone"), item.Metadata.GetValueOrDefault("email"), item.Status, true, "NonProductionSeam", false, false, false, "FoundationSimulation", FoundationCrudSupport.WarningText);
}
