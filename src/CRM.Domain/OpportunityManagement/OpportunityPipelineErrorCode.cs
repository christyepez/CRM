namespace CRM.Domain.OpportunityManagement;

public enum OpportunityPipelineErrorCode
{
    None = 0, InvalidOperation, InvalidOpportunityId, OpportunityNotFound,
    AccountNameRequired, AccountNameTooLong, InvalidExpectedValue, InvalidCurrency,
    InvalidProbability, InvalidPipelineId, InvalidStageId, PipelineStagesRequired,
    InvalidStageOrder, DuplicateStageOrder, DuplicateStageName, CurrentStageNotInPipeline,
    InvalidStageProgression, InvalidLeadId, InvalidContactId, InvalidAccountId, InvalidActivityId,
    TerminalOpportunityCannotBeModified, WonOpportunityCannotBeLost, LostOpportunityCannotBeWon,
    CancelledOpportunityCannotBeChanged, ValidationFailed
}
