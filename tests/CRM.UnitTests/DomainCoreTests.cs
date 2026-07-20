using CRM.Domain.Common;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using CRM.Domain.ValueObjects;
using Xunit;

namespace CRM.UnitTests;

public sealed class DomainCoreTests
{
    [Fact]
    public void EmailAddress_RejectsInvalidEmail()
    {
        Assert.Throws<ArgumentException>(() => EmailAddress.From("not-an-email"));
    }

    [Fact]
    public void Probability_RejectsOutOfRangeValue()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Probability.From(101));
    }

    [Fact]
    public void Lead_Qualify_MarksLeadAsQualified()
    {
        var lead = Lead.Create(
            CrmId.New(),
            PersonName.From("Ada", "Lovelace"),
            EmailAddress.From("ada@example.test"),
            CompanyName.From("Example Co"),
            new DateTimeOffset(2026, 7, 20, 0, 0, 0, TimeSpan.Zero));

        lead.MarkContacted();
        lead.Qualify();

        Assert.Equal(LeadStatus.Qualified, lead.Status);
    }

    [Fact]
    public void Opportunity_MarkWon_SetsWonAndProbabilityToOneHundred()
    {
        var opportunity = Opportunity.Create(
            CrmId.New(),
            CompanyName.From("Example Co"),
            MoneyAmount.From(100, "USD"),
            Probability.From(50));

        opportunity.MarkWon(new DateTimeOffset(2026, 7, 20, 0, 0, 0, TimeSpan.Zero));

        Assert.Equal(OpportunityStatus.Won, opportunity.Status);
        Assert.Equal(100, opportunity.Probability.Percentage);
    }

    [Fact]
    public void Activity_Complete_MarksActivityAsCompleted()
    {
        var activity = Activity.Schedule(
            CrmId.New(),
            ActivityType.Call,
            "Follow-up",
            new DateTimeOffset(2026, 7, 20, 0, 0, 0, TimeSpan.Zero));

        activity.Complete(new DateTimeOffset(2026, 7, 20, 1, 0, 0, TimeSpan.Zero));

        Assert.Equal(ActivityStatus.Completed, activity.Status);
        Assert.NotNull(activity.CompletedAtUtc);
    }
}
