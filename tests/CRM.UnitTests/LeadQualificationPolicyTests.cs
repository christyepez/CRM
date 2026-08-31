using CRM.Domain.Enums;
using CRM.Domain.LeadQualification;
using Xunit;

namespace CRM.UnitTests;

public sealed class LeadQualificationPolicyTests
{
    [Fact]
    public void Evaluate_Allows_New_To_Qualified()
    {
        var result = LeadQualificationPolicy.Evaluate(LeadStatus.New, new LeadQualificationCommand("lead-001", LeadQualificationDecision.Qualify));

        Assert.True(result.Allowed);
        Assert.True(result.Changed);
        Assert.Equal(LeadStatus.New, result.PreviousStatus);
        Assert.Equal(LeadStatus.Qualified, result.CurrentStatus);
        Assert.Equal(LeadQualificationErrorCode.None, result.ErrorCode);
    }

    [Fact]
    public void Evaluate_Allows_Contacted_To_Disqualified_WithReason()
    {
        var result = LeadQualificationPolicy.Evaluate(LeadStatus.Contacted, new LeadQualificationCommand("lead-001", LeadQualificationDecision.Disqualify, LeadDisqualificationReasonCode.NoInterest));

        Assert.True(result.Allowed);
        Assert.True(result.Changed);
        Assert.Equal(LeadStatus.Disqualified, result.CurrentStatus);
        Assert.Equal(LeadDisqualificationReasonCode.NoInterest, result.ReasonCode);
    }

    [Fact]
    public void Evaluate_Requires_DisqualificationReason()
    {
        var result = LeadQualificationPolicy.Evaluate(LeadStatus.New, new LeadQualificationCommand("lead-001", LeadQualificationDecision.Disqualify));

        Assert.False(result.Allowed);
        Assert.Equal(LeadQualificationErrorCode.DisqualificationReasonRequired, result.ErrorCode);
    }

    [Fact]
    public void Evaluate_Rejects_Reason_When_Qualifying()
    {
        var result = LeadQualificationPolicy.Evaluate(LeadStatus.New, new LeadQualificationCommand("lead-001", LeadQualificationDecision.Qualify, LeadDisqualificationReasonCode.Duplicate));

        Assert.False(result.Allowed);
        Assert.Equal(LeadQualificationErrorCode.DisqualificationReasonNotAllowed, result.ErrorCode);
    }

    [Fact]
    public void Evaluate_Rejects_Disqualified_To_Qualified()
    {
        var result = LeadQualificationPolicy.Evaluate(LeadStatus.Disqualified, new LeadQualificationCommand("lead-001", LeadQualificationDecision.Qualify));

        Assert.False(result.Allowed);
        Assert.Equal(LeadQualificationErrorCode.InvalidTransition, result.ErrorCode);
    }

    [Fact]
    public void Evaluate_Is_Idempotent_For_Already_Qualified()
    {
        var result = LeadQualificationPolicy.Evaluate(LeadStatus.Qualified, new LeadQualificationCommand("lead-001", LeadQualificationDecision.Qualify));

        Assert.True(result.Allowed);
        Assert.False(result.Changed);
        Assert.Equal(LeadStatus.Qualified, result.CurrentStatus);
    }

    [Fact]
    public void Evaluate_Requires_OtherReasonExplanation_When_Other_Is_Used()
    {
        var result = LeadQualificationPolicy.Evaluate(LeadStatus.New, new LeadQualificationCommand("lead-001", LeadQualificationDecision.Disqualify, LeadDisqualificationReasonCode.Other));

        Assert.False(result.Allowed);
        Assert.Equal(LeadQualificationErrorCode.OtherReasonExplanationRequired, result.ErrorCode);
    }

    [Fact]
    public void Evaluate_Rejects_Invalid_Enum_Decision()
    {
        var result = LeadQualificationPolicy.Evaluate(LeadStatus.New, new LeadQualificationCommand("lead-001", (LeadQualificationDecision)99));

        Assert.False(result.Allowed);
        Assert.Equal(LeadQualificationErrorCode.InvalidQualificationDecision, result.ErrorCode);
    }
}

