namespace CRM.Application.Ports.Persistence;

/// <summary>NonProductionPersistenceSeam: in-memory coordination boundary only; commit is non-durable.</summary>
public interface ICrmFoundationUnitOfWork
{
    Task SavePreviewAsync(CancellationToken cancellationToken = default);
}
