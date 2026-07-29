namespace CRM.Infrastructure.Portal.RuntimeProbe;

public sealed record PortalAuthRealRuntimeProbeOptions(
    bool ApprovalGranted = false,
    bool ProbeEnabled = false,
    string SyntheticPortalAuthReference = PortalAuthRealRuntimeProbe.SyntheticPortalAuthReference,
    string SyntheticUserReference = PortalAuthRealRuntimeProbe.SyntheticUserReference);
