using CRM.Application.Ports.Persistence;

namespace CRM.Infrastructure.Persistence.Foundation;

public sealed class InMemoryCrmFoundationUnitOfWork : ICrmFoundationUnitOfWork
{
    public Task SavePreviewAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.CompletedTask;
    }
}
