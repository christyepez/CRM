namespace CRM.Api.ProductiveRoutes;

public sealed class LockedProductiveRouteRuntimeRegistrationOptions
{
    public bool LockedRegistrationEnabled { get; init; }
    public bool LockedAuthorizationPolicyEnabled { get; init; }
}
