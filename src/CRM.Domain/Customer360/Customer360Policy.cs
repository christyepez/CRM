namespace CRM.Domain.Customer360;

public static class Customer360Policy
{
    public const int MaxDisplayNameLength = 160;

    public static Customer360ValidationResult Validate(Customer360Snapshot snapshot)
    {
        if (!Guid.TryParse(snapshot.CustomerId, out var customerId) || customerId == Guid.Empty)
            return Customer360ValidationResult.Rejected(Customer360ValidationErrorCode.InvalidCustomerId, "Customer id must be a non-empty GUID.");

        var displayName = (snapshot.DisplayName ?? string.Empty).Trim();
        if (displayName.Length == 0)
            return Customer360ValidationResult.Rejected(Customer360ValidationErrorCode.DisplayNameRequired, "Display name is required.");
        if (displayName.Length > MaxDisplayNameLength)
            return Customer360ValidationResult.Rejected(Customer360ValidationErrorCode.DisplayNameTooLong, "Display name is too long.");

        var counts = new[] { snapshot.ContactCount, snapshot.OpenOpportunityCount, snapshot.OpenCaseCount, snapshot.InteractionCount, snapshot.NoteCount, snapshot.DocumentCount, snapshot.TagCount, snapshot.AssignmentCount };
        if (counts.Any(value => value < 0))
            return Customer360ValidationResult.Rejected(Customer360ValidationErrorCode.NegativeAggregateCount, "Aggregate counts cannot be negative.");

        return Customer360ValidationResult.Accepted(snapshot with
        {
            CustomerId = customerId.ToString("D"),
            DisplayName = displayName
        });
    }
}
