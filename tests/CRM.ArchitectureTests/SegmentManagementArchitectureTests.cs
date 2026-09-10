using Xunit;
namespace CRM.ArchitectureTests;
public sealed class SegmentManagementArchitectureTests
{
    private static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}
    [Fact] public void SegmentDomain_RemainsDependencyFree(){var r=Root();var files=Directory.GetFiles(Path.Combine(r,"src","CRM.Domain","SegmentManagement"),"*.cs");var text=string.Join("\n",files.Select(File.ReadAllText));foreach(var x in new[]{"CRM.Application","CRM.Infrastructure","CRM.Api","Microsoft.EntityFrameworkCore","HttpClient","DbContext"})Assert.DoesNotContain(x,text,StringComparison.OrdinalIgnoreCase);}
    [Fact] public void SegmentPolicy_ContainsLifecycleAndLimits(){var r=Root();var s=File.ReadAllText(Path.Combine(r,"src","CRM.Domain","SegmentManagement","SegmentManagementPolicy.cs"));foreach(var x in new[]{"MaxNameLength = 160","MaxCriteriaSummaryLength = 1000","SegmentStatus.Draft","SegmentStatus.Active","SegmentStatus.Inactive","EvaluateActivate","EvaluateDeactivate"})Assert.Contains(x,s,StringComparison.Ordinal);}
    [Fact] public void SegmentRuntime_RemainsAbsentInS1701(){var r=Root();var p=File.ReadAllText(Path.Combine(r,"src","CRM.Api","Program.cs"));foreach(var x in new[]{"/api/crm/foundation/segments","/api/crm/segments","MapDelete(\"/api/crm/segments"})Assert.DoesNotContain(x,p,StringComparison.OrdinalIgnoreCase);}
}