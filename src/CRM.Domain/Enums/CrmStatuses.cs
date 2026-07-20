namespace CRM.Domain.Enums;

public enum LeadStatus
{
    New = 0,
    Contacted = 1,
    Qualified = 2,
    Disqualified = 3,
    Converted = 4
}

public enum OpportunityStatus
{
    Open = 0,
    Won = 1,
    Lost = 2,
    Cancelled = 3
}

public enum ActivityType
{
    Call = 0,
    Email = 1,
    Meeting = 2,
    Task = 3
}

public enum ActivityStatus
{
    Scheduled = 0,
    Completed = 1,
    Cancelled = 2
}

public enum CustomerConversionStatus
{
    Pending = 0,
    Converted = 1,
    Rejected = 2
}

public enum AccountStatus
{
    Draft = 0,
    Active = 1,
    Inactive = 2
}

public enum ContactStatus
{
    Draft = 0,
    Active = 1,
    Inactive = 2
}

public enum PreferredContactMethod
{
    NotSpecified = 0,
    Email = 1,
    Phone = 2
}
