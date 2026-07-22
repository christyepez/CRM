namespace CRM.Application.Ports.Reporting;

/// <summary>
/// FutureReportingAdapter port for Portal-owned reporting authorization context.
/// </summary>
public interface ICrmReportAuthorizationContext
{
    string GetPlannedAuthorizationMode();
}
