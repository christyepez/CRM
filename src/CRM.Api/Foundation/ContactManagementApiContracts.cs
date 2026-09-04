using CRM.Application.ContactManagement;
using CRM.Application.Foundation;
using CRM.Domain.ContactManagement;
using CRM.Domain.Enums;

namespace CRM.Api.Foundation;

public sealed record ContactManagementApiResponse(
    string? Id,
    string? Name,
    string? FirstName,
    string? LastName,
    string? Email,
    string? Phone,
    string? Title,
    string? AccountId,
    PreferredContactMethod PreferredContactMethod,
    string Status,
    bool Allowed,
    bool Changed,
    string ErrorCode,
    string Message,
    bool FoundationMode,
    string PersistenceMode,
    bool DurablePersistence,
    bool ProductiveCrudEnabled,
    bool DatabaseConfigured,
    bool PortalRuntimeEnabled,
    bool CommonDbRuntimeEnabled,
    string Warning)
{
    public static ContactManagementCreateApplicationRequest ToApplicationRequest(FoundationContactCreateRequest request) =>
        new(
            BuildName(request.FirstName, request.LastName),
            request.Email,
            request.Phone,
            request.Title,
            request.AccountId,
            request.PreferredContactMethod);

    public static ContactManagementUpdateApplicationRequest ToApplicationRequest(FoundationContactUpdateRequest request) =>
        new(
            BuildName(request.FirstName, request.LastName),
            request.Email,
            request.Phone,
            request.Title,
            request.AccountId,
            request.PreferredContactMethod);

    public static ContactManagementApiResponse From(ContactManagementApplicationResult result)
    {
        var splitName = SplitName(result.Contact?.Name);
        return new(
            result.ContactId,
            result.Contact?.Name,
            splitName.FirstName,
            splitName.LastName,
            result.Contact?.Email,
            result.Contact?.Phone,
            result.Contact?.Role,
            result.Contact?.AccountId,
            result.Contact?.PreferredContactMethod ?? PreferredContactMethod.NotSpecified,
            result.Contact?.Status ?? "PreviewOnly",
            result.Allowed,
            result.Changed,
            result.ErrorCode,
            result.Message,
            FoundationMode: true,
            result.Contact?.PersistenceMode ?? "NonProductionSeam",
            DurablePersistence: false,
            ProductiveCrudEnabled: false,
            DatabaseConfigured: false,
            PortalRuntimeEnabled: false,
            CommonDbRuntimeEnabled: false,
            "Foundation Contact API only; productive route remains locked");
    }

    public static int ToStatusCode(ContactManagementApplicationResult result) =>
        result.ErrorCode switch
        {
            nameof(ContactManagementErrorCode.None) => StatusCodes.Status200OK,
            nameof(ContactManagementErrorCode.ContactNotFound) => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status400BadRequest
        };

    private static string BuildName(string? firstName, string? lastName) =>
        string.Join(" ", new[] { firstName, lastName }.Where(value => !string.IsNullOrWhiteSpace(value)).Select(value => value!.Trim()));

    private static (string? FirstName, string? LastName) SplitName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return (null, null);
        }

        var parts = name.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        return parts.Length == 1 ? (parts[0], null) : (parts[0], parts[1]);
    }
}
