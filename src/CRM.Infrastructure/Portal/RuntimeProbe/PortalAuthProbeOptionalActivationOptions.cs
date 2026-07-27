namespace CRM.Infrastructure.Portal.RuntimeProbe;

public sealed record PortalAuthProbeOptionalActivationOptions(
    bool Enabled = false,
    bool ActivationApproved = false,
    bool NonProductionOnly = true,
    bool RollbackRequired = true);
