namespace CRM.Application.Foundation;

public sealed class CrmControlledRuntimePilotFirstSliceNonProductionActivationScaffoldStatusService
{
    public const string StatusName = "CrmSprint10P21ControlledRuntimePilotFirstSliceNonProductionActivationScaffold";
    public const string NextGate = "CrmSprint10P22ControlledRuntimePilotFirstSliceNonProductionActivationScaffoldValidation";
    public const string WarningText = "Sprint 10 P21 NonProduction activation scaffold is disabled-by-default and fail-closed; no Portal runtime call or activation is executed";

    public CrmControlledRuntimePilotFirstSliceNonProductionActivationScaffoldStatusResponse GetStatus() =>
        new(
            Module: "CRM",
            Status: StatusName,
            FoundationMode: true,
            CrmSprint10P21ControlledRuntimePilotFirstSliceNonProductionActivationScaffoldExists: true,
            CrmSprint10P20ActivationReadinessReviewed: true,
            PortalSprint21ContractAlignmentReviewed: true,
            ProductizationStatus: "PreparationOnly",
            ProductionActivationDecision: "NoGo",
            CrmProductionReady: false,
            FirstSliceNonProductionActivationScaffoldAttempted: true,
            FirstSliceNonProductionActivationScaffoldPrepared: true,
            FirstSliceNonProductionActivationScaffoldFeatureFlagsPrepared: true,
            FirstSliceNonProductionActivationScaffoldSafeConfigurationPrepared: true,
            FirstSliceNonProductionActivationScaffoldDisabledServicesPrepared: true,
            FirstSliceNonProductionActivationScaffoldFoundationEndpointPrepared: true,
            FirstSliceNonProductionActivationScaffoldTestEvidencePrepared: true,
            FirstSliceNonProductionActivationScaffoldRollbackPrepared: true,
            FirstSliceNonProductionActivationScaffoldRunbookPrepared: true,
            FirstSliceNonProductionActivationScaffoldSecurityDecisionPrepared: true,
            NonProductionActivationScaffoldOnly: true,
            NonProductionActivationExecuted: false,
            ConditionalFutureGoDefined: true,
            ConditionalFutureGoExecuted: false,
            RuntimePortalCouplingEnabled: false,
            RuntimePortalCallsEnabled: false,
            ProductivePortalNavigationEnabled: false,
            ProductivePortalGatewayRoutesEnabled: false,
            RealPortalPrivateUrlsPresent: false,
            PortalServicesInCrmCompose: false,
            CommonDbRuntimeEnabled: false,
            RealCommonDatabaseConfigured: false,
            SharedPortalTablesAccessEnabled: false,
            CrossDomainMigrationsPresent: false,
            PortalDatabaseDirectAccessEnabled: false,
            PortalAuthDuplicated: false,
            PortalMenuDuplicated: false,
            PortalPermissionsDuplicated: false,
            PortalAuditDuplicated: false,
            PortalNotificationDuplicated: false,
            PortalConfigurationDuplicated: false,
            SsoOidcProductionConfigured: false,
            RealSecretProviderConfigured: false,
            RealNotificationProviderConfigured: false,
            RealObservabilityProviderConfigured: false,
            BrowserTokenStorageDetected: false,
            SecretsPresent: false,
            EnvRealFileCommitted: false,
            PrivateUrlsPresent: false,
            RealDataPresent: false,
            ControlledRuntimePilotFirstSliceNonProductionActivationScaffoldReadiness: "NonProductionActivationScaffoldPreparedDisabledOnly",
            NextGate: NextGate,
            Warning: WarningText,
            FeatureFlags: GetFeatureFlags(),
            DisabledServices: GetDisabledServices());

    public IReadOnlyCollection<string> GetFeatureFlags() =>
    [
        "Crm:ControlledRuntimePilot:FirstSlice:NonProductionActivation:Enabled=false",
        "Crm:ControlledRuntimePilot:FirstSlice:NonProductionActivation:PortalClientEnabled=false",
        "Crm:ControlledRuntimePilot:FirstSlice:NonProductionActivation:HealthSmokeEnabled=false",
        "Crm:ControlledRuntimePilot:FirstSlice:NonProductionActivation:GatewayRoutesEnabled=false",
        "Crm:ControlledRuntimePilot:FirstSlice:NonProductionActivation:PortalNavigationEnabled=false"
    ];

    public IReadOnlyCollection<string> GetDisabledServices() =>
    [
        "DisabledNonProductionActivationService",
        "DisabledPortalRuntimeClient",
        "No Common DB runtime",
        "No production route registration",
        "No external Portal call"
    ];
}
