using CRM.Application.Foundation;
using CRM.Domain.Enums;
using CRM.Domain.LeadQualification;
using Xunit;

namespace CRM.UnitTests;

public sealed class LeadQualificationContractsTests
{
    [Fact]
    public void ResultContract_Preserves_Foundation_Guardrail_Metadata()
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

        Assert.True(result.FoundationMode);
        Assert.Equal("NonProductionSeam", result.PersistenceMode);
        Assert.False(result.ProductiveLeadRouteUnlocked);
        Assert.False(result.PortalAuthRuntimeEnabled);
        Assert.False(result.CommonDbRuntimeEnabled);
    }
}

