using CRM.Infrastructure.Portal.Simulation;
using Xunit;

namespace CRM.UnitTests;

public sealed class SimulatedPortalAuthorizationProviderTests
{
    [Fact]
    public void ScenarioProvider_ReturnsRequiredFoundationScenarios()
    {
        var scenarios = new SimulatedPortalAuthorizationScenarioProvider().GetScenarios();
        var scenarioNames = scenarios.Select(scenario => scenario.Scenario).ToArray();

        Assert.Contains("CrmAdminCanPreviewFoundationData", scenarioNames);
        Assert.Contains("CrmSalesCanPreviewAssignedLeads", scenarioNames);
        Assert.Contains("CrmReadOnlyCanViewReadiness", scenarioNames);
        Assert.Contains("CrmUnauthorizedCannotUseFoundationMutation", scenarioNames);
        Assert.All(scenarios, scenario => Assert.Equal("FoundationSimulation", scenario.SimulationMode));
    }

    [Fact]
    public async Task UserContextProvider_ReturnsDisconnectedPortalContext()
    {
        var context = await new SimulatedPortalUserContextProvider().GetCurrentUserContextAsync();

        Assert.False(context.Connected);
        Assert.Equal("PortalCorporativo", context.CapabilityOwner);
        Assert.Equal("FoundationSimulation", context.IntegrationMode);
    }
}
