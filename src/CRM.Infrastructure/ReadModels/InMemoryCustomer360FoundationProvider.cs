using CRM.Application.Ports.ReadModels;
using CRM.Domain.Customer360;
namespace CRM.Infrastructure.ReadModels;
public sealed class InMemoryCustomer360FoundationProvider : ICustomer360FoundationProvider
{
 private static readonly Customer360Snapshot[] Items = [new("11111111-1111-1111-1111-111111111111","Synthetic Customer 360",3,2,1,6,4,2,2,1)];
 public Task<IReadOnlyCollection<Customer360Snapshot>> GetAllAsync(CancellationToken ct=default){ct.ThrowIfCancellationRequested();return Task.FromResult<IReadOnlyCollection<Customer360Snapshot>>(Items);}
 public Task<Customer360Snapshot?> GetByIdAsync(string customerId,CancellationToken ct=default){ct.ThrowIfCancellationRequested();return Task.FromResult(Items.FirstOrDefault(x=>string.Equals(x.CustomerId,customerId,StringComparison.OrdinalIgnoreCase)));}
}
