using CRM.Application.ReadModels;

namespace CRM.Application.Persistence;

/// <summary>FuturePersistencePort: conceptual lead read model access contract only.</summary>
public interface ILeadReadModelStore
{
    Task<CrmReadModelResponse<LeadReadModel>> SearchAsync(CrmReadModelQuery query, CancellationToken cancellationToken = default);
}

/// <summary>FuturePersistencePort: conceptual account read model access contract only.</summary>
public interface IAccountReadModelStore
{
    Task<CrmReadModelResponse<AccountReadModel>> SearchAsync(CrmReadModelQuery query, CancellationToken cancellationToken = default);
}

/// <summary>FuturePersistencePort: conceptual contact read model access contract only.</summary>
public interface IContactReadModelStore
{
    Task<CrmReadModelResponse<ContactReadModel>> SearchAsync(CrmReadModelQuery query, CancellationToken cancellationToken = default);
}

/// <summary>FuturePersistencePort: conceptual commit boundary, not active in foundation runtime.</summary>
public interface ICrmUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}

public interface ICrmClock
{
    DateTimeOffset UtcNow { get; }
}
