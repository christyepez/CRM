namespace CRM.Application.Foundation;

public sealed class CrmControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationStatusService
{
    public const string StatusName = "CrmSprint10P24ControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementation";
    public const string NextGate = "CrmSprint10P25ControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationValidation";
    public const string WarningText = "Sprint 10 P24 controlled NonProduction activation implementation is prepared disabled-by-default and fail-closed; no Portal runtime call or activation is executed";

    public CrmControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationStatusResponse GetStatus() =>
        new(
            Module: "CRM",
            Status: StatusName,
            FoundationMode: true,
            CrmSprint10P24ControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationExists: true,
            CrmSprint10P23FinalApprovalGateReviewed: true,
            PortalSprint21ContractAlignmentReviewed: true,
            ProductizationStatus: "PreparationOnly",
            ProductionActivationDecision: "NoGo",
            CrmProductionReady: false,
            FirstSliceNonProductionActivationControlledImplementationAttempted: true,
            FirstSliceNonProductionActivationControlledImplementationPrepared: true,
            FirstSliceNonProductionActivationControlledImplementationBoundariesPrepared: true,
            FirstSliceNonProductionActivationControlledImplementationFeatureFlagsPrepared: true,
            FirstSliceNonProductionActivationControlledImplementationSafeConfigurationPrepared: true,
            FirstSliceNonProductionActivationControlledImplementationDisabledServicesPrepared: true,
            FirstSliceNonProductionActivationControlledImplementationFoundationEndpointPrepared: true,
            FirstSliceNonProductionActivationControlledImplementationDryRunPrepared: true,
            FirstSliceNonProductionActivationControlledImplementationTestEvidencePrepared: true,
            FirstSliceNonProductionActivationControlledImplementationRollbackPrepared: true,
            FirstSliceNonProductionActivationControlledImplementationRunbookPrepared: true,
            FirstSliceNonProductionActivationControlledImplementationSecurityDecisionPrepared: true,
            NonProductionActivationControlledImplementationPrepared: true,
            NonProductionActivationControlledImplementationExecuted: false,
            ConditionalGoFutureDefined: true,
            ConditionalGoFutureExecuted: false,
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
            ControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationReadiness: "ControlledImplementationPreparedDisabledOnly",
            NextGate: NextGate,
            Warning: WarningText,
            FeatureFlags: GetFeatureFlags(),
            DisabledServices: GetDisabledServices());

    public IReadOnlyCollection<string> GetFeatureFlags() =>
    [
        "Crm:ControlledRuntimePilot:FirstSlice:NonProductionControlledImplementation:Enabled=false",
        "Crm:ControlledRuntimePilot:FirstSlice:NonProductionControlledImplementation:PortalClientEnabled=false",
        "Crm:ControlledRuntimePilot:FirstSlice:NonProductionControlledImplementation:DryRunEnabled=false",
        "Crm:ControlledRuntimePilot:FirstSlice:NonProductionControlledImplementation:GatewayRoutesEnabled=false",
        "Crm:ControlledRuntimePilot:FirstSlice:NonProductionControlledImplementation:PortalNavigationEnabled=false"
    ];

    public IReadOnlyCollection<string> GetDisabledServices() =>
    [
        "DisabledControlledNonProductionActivationService",
        "No external Portal call",
        "No Common DB runtime",
        "No Gateway route registration",
        "No production navigation registration"
    ];
}
