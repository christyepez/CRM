using CRM.Application.Ports.ReadModels;
using CRM.Domain.Customer360;
namespace CRM.Application.Customer360;
public sealed class Customer360ReadService(ICustomer360FoundationProvider provider) : ICustomer360ReadService
{
 private const string SourceMode="DeterministicSyntheticFoundation";
 public async Task<IReadOnlyCollection<Customer360ApplicationItem>> GetAllAsync(CancellationToken ct=default)=>(await provider.GetAllAsync(ct)).Select(Map).ToArray();
 public async Task<Customer360ApplicationItem?> GetByIdAsync(string customerId,CancellationToken ct=default)
 {
  if(!Guid.TryParse(customerId,out var id)||id==Guid.Empty)return null;
  var item=await provider.GetByIdAsync(id.ToString("D"),ct); return item is null?null:Map(item);
 }
 private static Customer360ApplicationItem Map(Customer360Snapshot x)
 {
  var v=Customer360Policy.Validate(x); if(!v.Valid||v.NormalizedSnapshot is null) throw new InvalidOperationException(v.Message); var n=v.NormalizedSnapshot;
  return new(n.CustomerId,n.DisplayName,n.ContactCount,n.OpenOpportunityCount,n.OpenCaseCount,n.InteractionCount,n.NoteCount,n.DocumentCount,n.TagCount,n.AssignmentCount,SourceMode,false,false,false);
 }
}
