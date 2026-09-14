namespace CRM.Domain.Customer360;

public sealed record Customer360ValidationResult(
    bool Valid,
    Customer360ValidationErrorCode ErrorCode,
    string Message,
    Customer360Snapshot? NormalizedSnapshot)
{
    public static Customer360ValidationResult Accepted(Customer360Snapshot snapshot) =>
        new(true, Customer360ValidationErrorCode.None, "Customer 360 snapshot accepted.", snapshot);

    public static Customer360ValidationResult Rejected(Customer360ValidationErrorCode errorCode, string message) =>
        new(false, errorCode, message, null);
}
