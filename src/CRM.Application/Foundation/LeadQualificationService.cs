using CRM.Application.Persistence;
using CRM.Application.Ports.Persistence;
using CRM.Domain.Enums;
using CRM.Domain.LeadQualification;

namespace CRM.Application.Foundation;

public sealed class LeadQualificationService(ILeadFoundationStore store) : ILeadQualificationService
{
    private const string FoundationPersistenceMode = "NonProductionSeam";
    private const string LeadEntityName = "Lead";

    public async Task<LeadQualificationResult> QualifyAsync(LeadQualificationRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (request.Decision is null || !Enum.IsDefined(request.Decision.Value))
        {
            return Rejected(
                request.LeadId ?? string.Empty,
                LeadStatus.New,
                request.Decision ?? 0,
                request.ReasonCode,
                LeadQualificationErrorCode.InvalidQualificationDecision,
                "Qualification decision is invalid.");
        }

        var leadId = (request.LeadId ?? string.Empty).Trim();
        var contractCommand = new LeadQualificationCommand(
            leadId,
            request.Decision.Value,
            request.ReasonCode,
            request.OtherReasonExplanation,
            request.Comment);

        var contractValidation = LeadQualificationPolicy.Evaluate(LeadStatus.New, contractCommand);
        if (contractValidation.ErrorCode == LeadQualificationErrorCode.LeadIdRequired)
        {
            return Map(contractValidation);
        }

        var preview = await store.GetPreviewByIdAsync(leadId, cancellationToken);
        if (preview is null)
        {
            return Rejected(leadId, LeadStatus.New, request.Decision.Value, request.ReasonCode, LeadQualificationErrorCode.LeadNotFound, "Lead was not found in the foundation seam.");
        }

        var currentStatus = ParseLeadStatus(preview.Status);
        var policyResult = LeadQualificationPolicy.Evaluate(currentStatus, contractCommand);
        if (!policyResult.Allowed || !policyResult.Changed)
        {
            return Map(policyResult);
        }

        var updated = preview with
        {
            Status = policyResult.CurrentStatus.ToString(),
            Metadata = BuildMetadata(preview.Metadata, policyResult, request)
        };

        await store.SavePreviewAsync(updated, cancellationToken);
        return Map(policyResult);
    }

    private static LeadStatus ParseLeadStatus(string status) =>
        Enum.TryParse<LeadStatus>(status, ignoreCase: true, out var parsed)
            ? parsed
            : LeadStatus.New;

    private static IReadOnlyDictionary<string, string> BuildMetadata(IReadOnlyDictionary<string, string> existing, LeadQualificationRuleResult result, LeadQualificationRequest request)
    {
        var metadata = new Dictionary<string, string>(existing, StringComparer.OrdinalIgnoreCase)
        {
            ["qualificationDecision"] = result.Decision.ToString(),
            ["qualificationStatus"] = result.CurrentStatus.ToString(),
            ["qualificationChanged"] = result.Changed.ToString()
        };

        if (result.ReasonCode is not null)
        {
            metadata["disqualificationReasonCode"] = result.ReasonCode.Value.ToString();
        }

        var otherReasonExplanation = (request.OtherReasonExplanation ?? string.Empty).Trim();
        if (otherReasonExplanation.Length > 0)
        {
            metadata["otherReasonExplanation"] = otherReasonExplanation;
        }

        var comment = (request.Comment ?? string.Empty).Trim();
        if (comment.Length > 0)
        {
            metadata["qualificationComment"] = comment;
        }

        return metadata;
    }

    private static LeadQualificationResult Map(LeadQualificationRuleResult result) =>
        new(
            result.LeadId,
            result.PreviousStatus,
            result.CurrentStatus,
            result.Decision,
            result.ReasonCode,
            result.Allowed,
            result.Changed,
            result.ErrorCode,
            result.Message,
            true,
            FoundationPersistenceMode,
            false,
            false,
            false);

    private static LeadQualificationResult Rejected(
        string leadId,
        LeadStatus currentStatus,
        LeadQualificationDecision decision,
        LeadDisqualificationReasonCode? reasonCode,
        LeadQualificationErrorCode errorCode,
        string message) =>
        Map(LeadQualificationRuleResult.Rejected(leadId, currentStatus, decision, reasonCode, errorCode, message));
}

