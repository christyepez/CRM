using CRM.Application.ActivityManagement;
using CRM.Domain.ActivityManagement;
using CRM.Domain.Enums;

namespace CRM.Api.Foundation;

public sealed record FoundationActivityCreateRequest(
    ActivityType Type,
    string? Subject,
    DateTimeOffset ScheduledAtUtc,
    string? LeadId,
    string? ContactId);

public sealed record FoundationActivityUpdateRequest(
    ActivityType Type,
    string? Subject,
    DateTimeOffset ScheduledAtUtc,
    string? LeadId,
    string? ContactId);

public sealed record ActivityManagementApiResponse(
    string? Id,
    ActivityManagementOperation Operation,
    bool Allowed,
    bool Changed,
    string ErrorCode,
    string Message,
    ActivityStatus? Status,
    ActivityManagementApplicationActivity? Activity,
    bool FoundationMode,
    string PersistenceMode,
    bool DurablePersistence,
    bool ProductiveCrudEnabled,
    bool DatabaseConfigured,
    bool PortalRuntimeEnabled,
    bool CommonDbRuntimeEnabled,
    string Warning)
{
    public static ActivityManagementCreateApplicationRequest ToApplicationRequest(FoundationActivityCreateRequest request) =>
        new(request.Type, request.Subject, request.ScheduledAtUtc, request.LeadId, request.ContactId);

    public static ActivityManagementUpdateApplicationRequest ToApplicationRequest(FoundationActivityUpdateRequest request) =>
        new(request.Type, request.Subject, request.ScheduledAtUtc, request.LeadId, request.ContactId);

    public static ActivityManagementApiResponse From(ActivityManagementApplicationResult result) =>
        new(
            result.ActivityId,
            result.Operation,
            result.Allowed,
            result.Changed,
            result.ErrorCode,
            result.Message,
            result.Status,
            result.Activity,
            FoundationMode: true,
            result.Activity?.PersistenceMode ?? "NonProductionSeam",
            DurablePersistence: false,
            ProductiveCrudEnabled: false,
            DatabaseConfigured: false,
            PortalRuntimeEnabled: false,
            CommonDbRuntimeEnabled: false,
            "Foundation Activity API only; productive route remains locked");

    public static int ToStatusCode(ActivityManagementApplicationResult result) =>
        result.ErrorCode switch
        {
            nameof(ActivityManagementErrorCode.None) => StatusCodes.Status200OK,
            nameof(ActivityManagementErrorCode.ActivityNotFound) => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status400BadRequest
        };
}
