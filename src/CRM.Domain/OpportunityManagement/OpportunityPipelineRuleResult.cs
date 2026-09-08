using CRM.Domain.Enums;

namespace CRM.Domain.OpportunityManagement;

public sealed record OpportunityPipelineRuleResult(
    string? OpportunityId,
    OpportunityPipelineOperation Operation,
    bool Allowed,
    bool Changed,
    OpportunityPipelineErrorCode ErrorCode,
    string Message,
    string? NormalizedAccountName,
    decimal ExpectedValue,
    string? NormalizedCurrency,
    int Probability,
    string? PipelineId,
    string? StageId,
    OpportunityStatus ResultStatus,
    string? LeadId,
    string? ContactId,
    string? AccountId,
    string? ActivityId)
{
    public bool Success => Allowed && ErrorCode == OpportunityPipelineErrorCode.None;
}
