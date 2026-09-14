using CRM.Domain.Customer360;
namespace CRM.Application.Ports.ReadModels;
public interface ICustomer360FoundationProvider
{
 Task<IReadOnlyCollection<Customer360Snapshot>> GetAllAsync(CancellationToken ct=default);
 Task<Customer360Snapshot?> GetByIdAsync(string customerId,CancellationToken ct=default);
}
