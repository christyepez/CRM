namespace CRM.Domain.Customer360;

public sealed record Customer360Snapshot(
    string CustomerId,
    string DisplayName,
    int ContactCount,
    int OpenOpportunityCount,
    int OpenCaseCount,
    int InteractionCount,
    int NoteCount,
    int DocumentCount,
    int TagCount,
    int AssignmentCount);

public enum Customer360ValidationErrorCode
{
    None = 0,
    InvalidCustomerId,
    DisplayNameRequired,
    DisplayNameTooLong,
    NegativeAggregateCount
}
