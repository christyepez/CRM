using CRM.Domain.Customer360;
namespace CRM.Application.Customer360;
public sealed record Customer360ApplicationItem(string CustomerId,string DisplayName,int ContactCount,int OpenOpportunityCount,int OpenCaseCount,int InteractionCount,int NoteCount,int DocumentCount,int TagCount,int AssignmentCount,string SourceMode,bool ProductiveRuntimeEnabled,bool PortalRuntimeEnabled,bool CommonDbRuntimeEnabled);
public interface ICustomer360ReadService
{
 Task<IReadOnlyCollection<Customer360ApplicationItem>> GetAllAsync(CancellationToken ct=default);
 Task<Customer360ApplicationItem?> GetByIdAsync(string customerId,CancellationToken ct=default);
}
