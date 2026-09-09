using CRM.Application.Persistence;
using CRM.Domain.Enums;

namespace CRM.Application.Ports.Persistence;

public sealed record AccountFoundationRecord(string Id,string Name,string? TaxId,string? Industry,string? Segment,AccountStatus Status);

/// <summary>NonProductionPersistenceSeam: Account synthetic foundation storage only; no durable database or productive CRUD.</summary>
public interface IAccountFoundationStore
{
    Task<IReadOnlyCollection<AccountFoundationRecord>> GetAllAsync(CancellationToken cancellationToken=default);
    Task<AccountFoundationRecord?> GetByIdAsync(string id,CancellationToken cancellationToken=default);
    Task<AccountFoundationRecord> SaveAsync(AccountFoundationRecord account,CancellationToken cancellationToken=default);

    Task<IReadOnlyCollection<CrmFoundationPreviewItemContract>> GetPreviewAsync(CancellationToken cancellationToken=default);
    Task<CrmFoundationPreviewItemContract?> GetPreviewByIdAsync(string id,CancellationToken cancellationToken=default);
    Task<CrmFoundationPreviewItemContract> SavePreviewAsync(CrmFoundationPreviewItemContract preview,CancellationToken cancellationToken=default);
    Task ClearPreviewAsync(CancellationToken cancellationToken=default);
    Task<CrmFoundationStoreStatusContract> GetStatusAsync(CancellationToken cancellationToken=default);
}
