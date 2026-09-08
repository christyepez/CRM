using CRM.Domain.Enums;

namespace CRM.Domain.OpportunityManagement;

public sealed record OpportunityPipelineStageDefinition(string StageId, string Name, int Order);

public sealed record OpportunityPipelineCommand(
    OpportunityPipelineOperation Operation,
    string? OpportunityId,
    string? AccountName,
    decimal ExpectedValue,
    string? Currency,
    int Probability,
    string? PipelineId,
    string? StageId,
    IReadOnlyCollection<OpportunityPipelineStageDefinition>? Stages,
    string? LeadId = null,
    string? ContactId = null,
    string? AccountId = null,
    string? ActivityId = null,
    OpportunityPipelineSnapshot? ExistingOpportunity = null);

public sealed record OpportunityPipelineSnapshot(
    string OpportunityId,
    string AccountName,
    decimal ExpectedValue,
    string Currency,
    int Probability,
    string PipelineId,
    string StageId,
    OpportunityStatus Status,
    string? LeadId = null,
    string? ContactId = null,
    string? AccountId = null,
    string? ActivityId = null);
