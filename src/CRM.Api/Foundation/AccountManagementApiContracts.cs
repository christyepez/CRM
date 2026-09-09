using CRM.Application.AccountManagement;
using CRM.Domain.AccountManagement;

namespace CRM.Api.Foundation;

public sealed record FoundationAccountManagementCreateRequest(string? Name,string? TaxId,string? Industry,string? Segment);
public sealed record FoundationAccountManagementUpdateRequest(string? Name,string? TaxId,string? Industry,string? Segment);

public sealed record AccountManagementApiResponse(
    string? Id,
    string Operation,
    bool Allowed,
    bool Changed,
    string ErrorCode,
    string Message,
    AccountManagementApplicationAccount? Account,
    bool FoundationMode,
    string PersistenceMode,
    bool DurablePersistence,
    bool ProductiveCrudEnabled,
    bool PortalRuntimeEnabled,
    bool CommonDbRuntimeEnabled,
    string Warning)
{
    public static AccountManagementApiResponse From(AccountManagementApplicationResult result) =>
        new(result.AccountId,result.Operation,result.Allowed,result.Changed,result.ErrorCode,result.Message,result.Account,true,
            result.Account?.PersistenceMode ?? "NonProductionSeam",false,false,false,false,
            "Foundation Account API only; productive route remains locked");

    public static int ToStatusCode(AccountManagementApplicationResult result) => result.ErrorCode switch
    {
        nameof(AccountManagementErrorCode.None) => StatusCodes.Status200OK,
        nameof(AccountManagementErrorCode.AccountNotFound) => StatusCodes.Status404NotFound,
        nameof(AccountManagementErrorCode.InvalidStatusTransition) => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status400BadRequest
    };

    public static AccountManagementCreateRequest ToApplication(FoundationAccountManagementCreateRequest request) =>
        new(request.Name,request.TaxId,request.Industry,request.Segment);

    public static AccountManagementUpdateRequest ToApplication(FoundationAccountManagementUpdateRequest request) =>
        new(request.Name,request.TaxId,request.Industry,request.Segment);
}
