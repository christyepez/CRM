using CRM.Application.Persistence;
using CRM.Domain.Entities;

namespace CRM.Application.Ports.Persistence;

/// <summary>NonProductionPersistenceSeam: activity foundation storage contract only; no durable database or productive CRUD.</summary>
public interface IActivityFoundationStore
{
    Task<IReadOnlyCollection<Activity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Activity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    Task<Activity> SaveAsync(Activity activity, CancellationToken cancellationToken = default);

    Task<CrmFoundationStoreStatusContract> GetStatusAsync(CancellationToken cancellationToken = default);
}
