using CRM.Api.Foundation;
using CRM.Application.Foundation;
using CRM.Domain.Enums;
using CRM.Domain.LeadQualification;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace CRM.UnitTests;

public sealed class LeadQualificationApiContractsTests
{
    [Fact]
    public void Request_Maps_Route_LeadId_To_ApplicationContract()
    {
        var request = new LeadQualificationApiRequest(LeadQualificationDecision.Disqualify, LeadDisqualificationReasonCode.Other, "Synthetic reason", "Synthetic comment");

        var mapped = request.ToApplicationRequest("lead-001");

        Assert.Equal("lead-001", mapped.LeadId);
        Assert.Equal(LeadQualificationDecision.Disqualify, mapped.Decision);
        Assert.Equal(LeadDisqualificationReasonCode.Other, mapped.ReasonCode);
        Assert.Equal("Synthetic reason", mapped.OtherReasonExplanation);
        Assert.Equal("Synthetic comment", mapped.Comment);
    }

    [Fact]
    public void Response_Does_Not_Expose_Runtime_Details()
    {
        var result = new LeadQualificationResult(
            "lead-001",
            LeadStatus.New,
            LeadStatus.Qualified,
            LeadQualificationDecision.Qualify,
            null,
            true,
            true,
            LeadQualificationErrorCode.None,
            "Lead qualification state changed.",
            true,
            "NonProductionSeam",
            false,
            false,
            false);

        var response = LeadQualificationApiResponse.From(result);

        Assert.True(response.FoundationMode);
        Assert.Equal("NonProductionSeam", response.PersistenceMode);
        Assert.False(response.ProductiveLeadQualificationRouteEnabled);
        Assert.False(response.PortalRuntimeEnabled);
        Assert.False(response.CommonDbRuntimeEnabled);
    }

    [Theory]
    [InlineData(LeadQualificationErrorCode.None, StatusCodes.Status200OK)]
    [InlineData(LeadQualificationErrorCode.LeadNotFound, StatusCodes.Status404NotFound)]
    [InlineData(LeadQualificationErrorCode.InvalidTransition, StatusCodes.Status409Conflict)]
    [InlineData(LeadQualificationErrorCode.InvalidQualificationDecision, StatusCodes.Status400BadRequest)]
    [InlineData(LeadQualificationErrorCode.CommentTooLong, StatusCodes.Status400BadRequest)]
    public void ToStatusCode_Maps_Deterministically(LeadQualificationErrorCode errorCode, int expectedStatus)
    {
        var result = new LeadQualificationResult(
            "lead-001",
            LeadStatus.New,
            LeadStatus.New,
            LeadQualificationDecision.Qualify,
            null,
            errorCode == LeadQualificationErrorCode.None,
            false,
            errorCode,
            "Safe message.",
            true,
            "NonProductionSeam",
            false,
            false,
            false);

        Assert.Equal(expectedStatus, LeadQualificationApiResponse.ToStatusCode(result));
    }
}

