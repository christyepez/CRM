namespace CRM.Application.ContactManagement;

public interface IContactManagementService
{
    Task<ContactManagementApplicationResult> CreateAsync(ContactManagementCreateApplicationRequest request, CancellationToken cancellationToken = default);

    Task<ContactManagementApplicationResult> UpdateAsync(string contactId, ContactManagementUpdateApplicationRequest request, CancellationToken cancellationToken = default);
}
