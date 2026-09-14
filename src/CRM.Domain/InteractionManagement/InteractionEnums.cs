namespace CRM.Domain.InteractionManagement;

public enum InteractionManagementOperation
{
    Create = 0,
    Update = 1,
    Void = 2
}

public enum InteractionRelatedEntityType
{
    Customer = 0,
    Contact = 1,
    Lead = 2,
    Opportunity = 3,
    Case = 4
}

public enum InteractionChannel
{
    Email = 0,
    Phone = 1,
    Meeting = 2,
    Chat = 3,
    Other = 4
}

public enum InteractionDirection
{
    Inbound = 0,
    Outbound = 1
}

public enum InteractionStatus
{
    Recorded = 0,
    Voided = 1
}

public enum InteractionManagementErrorCode
{
    None = 0,
    InvalidOperation,
    InvalidInteractionId,
    InteractionNotFound,
    InvalidRelatedEntityType,
    RelatedEntityIdRequired,
    InvalidRelatedEntityId,
    InvalidChannel,
    InvalidDirection,
    SubjectRequired,
    SubjectTooLong,
    SummaryRequired,
    SummaryTooLong,
    OccurredAtUtcRequired,
    OccurredAtUtcMustBeUtc,
    OccurredAtUtcInFuture,
    EvaluationTimestampMustBeUtc,
    InvalidStatus,
    VoidedInteractionCannotBeModified
}
