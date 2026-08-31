using CRM.Domain.Enums;

namespace CRM.Domain.LeadQualification;

public static class LeadQualificationPolicy
{
    public const int MaxCommentLength = 500;
    public const int MaxOtherReasonExplanationLength = 250;

    public static LeadQualificationRuleResult Evaluate(LeadStatus currentStatus, LeadQualificationCommand command)
    {
        var leadId = (command.LeadId ?? string.Empty).Trim();
        if (leadId.Length == 0)
        {
            return LeadQualificationRuleResult.Rejected(string.Empty, currentStatus, command.Decision, command.ReasonCode, LeadQualificationErrorCode.LeadIdRequired, "Lead id is required.");
        }

        if (!Enum.IsDefined(command.Decision))
        {
            return LeadQualificationRuleResult.Rejected(leadId, currentStatus, command.Decision, command.ReasonCode, LeadQualificationErrorCode.InvalidQualificationDecision, "Qualification decision is invalid.");
        }

        var comment = (command.Comment ?? string.Empty).Trim();
        if (comment.Length > MaxCommentLength)
        {
            return LeadQualificationRuleResult.Rejected(leadId, currentStatus, command.Decision, command.ReasonCode, LeadQualificationErrorCode.CommentTooLong, "Qualification comment is too long.");
        }

        return command.Decision switch
        {
            LeadQualificationDecision.Qualify => EvaluateQualify(currentStatus, command, leadId),
            LeadQualificationDecision.Disqualify => EvaluateDisqualify(currentStatus, command, leadId),
            _ => LeadQualificationRuleResult.Rejected(leadId, currentStatus, command.Decision, command.ReasonCode, LeadQualificationErrorCode.InvalidQualificationDecision, "Qualification decision is invalid.")
        };
    }

    private static LeadQualificationRuleResult EvaluateQualify(LeadStatus currentStatus, LeadQualificationCommand command, string leadId)
    {
        if (command.ReasonCode is not null)
        {
            return LeadQualificationRuleResult.Rejected(leadId, currentStatus, command.Decision, command.ReasonCode, LeadQualificationErrorCode.DisqualificationReasonNotAllowed, "Disqualification reason is not allowed when qualifying a lead.");
        }

        return currentStatus switch
        {
            LeadStatus.New or LeadStatus.Contacted => LeadQualificationRuleResult.Success(leadId, currentStatus, LeadStatus.Qualified, command.Decision, null, true),
            LeadStatus.Qualified => LeadQualificationRuleResult.Success(leadId, currentStatus, LeadStatus.Qualified, command.Decision, null, false),
            LeadStatus.Disqualified => LeadQualificationRuleResult.Rejected(leadId, currentStatus, command.Decision, null, LeadQualificationErrorCode.InvalidTransition, "Disqualified leads cannot be qualified by this foundation rule."),
            LeadStatus.Converted => LeadQualificationRuleResult.Rejected(leadId, currentStatus, command.Decision, null, LeadQualificationErrorCode.InvalidTransition, "Converted leads cannot be re-qualified."),
            _ => LeadQualificationRuleResult.Rejected(leadId, currentStatus, command.Decision, null, LeadQualificationErrorCode.InvalidTransition, "Lead status cannot transition to qualified.")
        };
    }

    private static LeadQualificationRuleResult EvaluateDisqualify(LeadStatus currentStatus, LeadQualificationCommand command, string leadId)
    {
        if (command.ReasonCode is null)
        {
            return LeadQualificationRuleResult.Rejected(leadId, currentStatus, command.Decision, null, LeadQualificationErrorCode.DisqualificationReasonRequired, "Disqualification reason is required.");
        }

        if (command.ReasonCode == LeadDisqualificationReasonCode.Other)
        {
            var other = (command.OtherReasonExplanation ?? string.Empty).Trim();
            if (other.Length == 0)
            {
                return LeadQualificationRuleResult.Rejected(leadId, currentStatus, command.Decision, command.ReasonCode, LeadQualificationErrorCode.OtherReasonExplanationRequired, "Other disqualification reason requires an explanation.");
            }

            if (other.Length > MaxOtherReasonExplanationLength)
            {
                return LeadQualificationRuleResult.Rejected(leadId, currentStatus, command.Decision, command.ReasonCode, LeadQualificationErrorCode.OtherReasonExplanationTooLong, "Other disqualification reason explanation is too long.");
            }
        }

        return currentStatus switch
        {
            LeadStatus.New or LeadStatus.Contacted or LeadStatus.Qualified => LeadQualificationRuleResult.Success(leadId, currentStatus, LeadStatus.Disqualified, command.Decision, command.ReasonCode, true),
            LeadStatus.Disqualified => LeadQualificationRuleResult.Success(leadId, currentStatus, LeadStatus.Disqualified, command.Decision, command.ReasonCode, false),
            LeadStatus.Converted => LeadQualificationRuleResult.Rejected(leadId, currentStatus, command.Decision, command.ReasonCode, LeadQualificationErrorCode.InvalidTransition, "Converted leads cannot be disqualified."),
            _ => LeadQualificationRuleResult.Rejected(leadId, currentStatus, command.Decision, command.ReasonCode, LeadQualificationErrorCode.InvalidTransition, "Lead status cannot transition to disqualified.")
        };
    }
}

