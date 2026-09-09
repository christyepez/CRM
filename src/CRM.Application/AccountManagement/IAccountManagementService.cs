namespace CRM.Application.AccountManagement;

public interface IAccountManagementService
{
    Task<IReadOnlyCollection<AccountManagementApplicationAccount>> GetAllAsync(CancellationToken cancellationToken=default);
    Task<AccountManagementApplicationAccount?> GetByIdAsync(string id,CancellationToken cancellationToken=default);
    Task<AccountManagementApplicationResult> CreateAsync(AccountManagementCreateRequest request,CancellationToken cancellationToken=default);
    Task<AccountManagementApplicationResult> UpdateAsync(string id,AccountManagementUpdateRequest request,CancellationToken cancellationToken=default);
    Task<AccountManagementApplicationResult> ActivateAsync(string id,CancellationToken cancellationToken=default);
    Task<AccountManagementApplicationResult> DeactivateAsync(string id,CancellationToken cancellationToken=default);
}
