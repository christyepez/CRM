namespace CRM.Application.Foundation;

public sealed class CrmControlledRuntimePilotFirstSliceScaffoldStatusService
{
    public const string StatusName = "CrmSprint10P14ControlledRuntimePilotFirstImplementationSliceScaffold";
    public const string NextGate = "CrmSprint10P15ControlledRuntimePilotFirstSliceScaffoldValidation";
    public const string WarningText = "Sprint 10 P14 scaffold is disabled-by-default; no Portal runtime call, route, navigation or Common DB activation is enabled";

    public CrmControlledRuntimePilotFirstSliceScaffoldStatusResponse GetStatus() =>
        new(
            Module: "CRM",
            Status: StatusName,
            FoundationMode: true,
            CrmSprint10P14ControlledRuntimePilotFirstImplementationSliceScaffoldExists: true,
            CrmSprint10P13FirstSliceDesignReviewed: true,
            PortalSprint21ContractAlignmentReviewed: true,
            ProductizationStatus: "PreparationOnly",
            ProductionActivationDecision: "NoGo",
            CrmProductionReady: false,
            FirstImplementationSliceScaffoldAttempted: true,
            FirstImplementationSliceScaffoldPrepared: true,
            FirstSliceScaffoldFeatureFlagsPrepared: true,
            FirstSliceScaffoldSafeConfigurationPrepared: true,
            FirstSliceScaffoldDisabledClientPrepared: true,
            FirstSliceScaffoldHealthSmokePrepared: true,
            FirstSliceScaffoldTestEvidencePrepared: true,
            FirstSliceScaffoldRollbackPrepared: true,
            FirstSliceScaffoldRunbookPrepared: true,
            FirstSliceScaffoldSecurityDecisionPrepared: true,
            FirstImplementationSliceScaffoldOnly: true,
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
            ControlledRuntimePilotFirstImplementationSliceScaffoldReadiness: "FirstSliceScaffoldPreparedDisabledOnly",
            NextGate: NextGate,
            Warning: WarningText,
            FeatureFlags: GetFeatureFlags(),
            BlockedItems: GetBlockedItems());

    public IReadOnlyCollection<string> GetFeatureFlags() =>
    [
        "Crm:ControlledRuntimePilot:FirstSlice:Enabled=false",
        "Crm:ControlledRuntimePilot:FirstSlice:PortalClientEnabled=false",
        "Crm:ControlledRuntimePilot:FirstSlice:HealthSmokeEnabled=false",
        "Crm:ControlledRuntimePilot:FirstSlice:GatewayRoutesEnabled=false",
        "Crm:ControlledRuntimePilot:FirstSlice:PortalNavigationEnabled=false"
    ];

    public IReadOnlyCollection<string> GetBlockedItems() =>
    [
        "Production activation",
        "Runtime Portal calls",
        "Productive Portal navigation and Gateway routes",
        "Common DB runtime",
        "Real secrets, tokens, certificates and private URLs",
        "Portal Auth/Menu/Permissions/Audit/Notification/Configuration duplication"
    ];
}
