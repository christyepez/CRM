import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter, RouterOutlet, Routes } from '@angular/router';
import { Component, Injectable, signal } from '@angular/core';
import { JsonPipe } from '@angular/common';

@Injectable({ providedIn: 'root' })
class CrmReadinessService {
  readonly apiBaseUrl = '/api';

  getReadiness() {
    return {
      module: 'CRM',
      status: 'ReadyForFoundationOnly',
      domainCatalog: 'Draft',
      leadsFoundation: 'PreviewOnly',
      accountsFoundation: 'PreviewOnly',
      contactsFoundation: 'PreviewOnly',
      readModels: 'PreviewOnly',
      persistenceStrategy: 'Draft',
      persistence: 'None',
      portalIntegration: 'Planned',
      portalConnected: false,
      capabilityOwner: 'PortalCorporativo',
      portalCapabilities: 'Auth/Menu/Permissions/Audit/Notification/Configuration: External',
      financialIntegration: 'Planned',
      financialConnected: false,
      financialCapabilityOwner: 'Financiero',
      financialIntegrationPattern: 'API + Events + NoSharedDatabase',
      taxArtifacts: 'SRI/ATS/RIDE/XAdES: NotImplementedInCRM',
      reportingIntegration: 'Planned',
      reportingConnected: false,
      analyticsMode: 'Planned',
      kpiCatalog: 'Foundation',
      dashboardCatalog: 'Foundation',
      powerBiEmbed: 'NotConfigured',
      sprint1Foundation: 'Closed',
      productization: 'NotReady',
      nextGate: 'Sprint2Planning',
      dockerExternalRegistryNote: 'BLOCKED_EXTERNAL_REGISTRY documented when MCR times out',
      persistenceDesignReview: 'Active',
      persistenceSeam: 'Active',
      persistenceMode: 'NonProductionSeam',
      foundationStore: 'Enabled',
      databaseConfigured: false,
      dbContextConfigured: false,
      migrationReady: false,
      durablePersistence: false,
      productiveCrud: false,
      persistenceNextGate: 'Sprint2P3PortalAuthorizationAdapterSimulation',
      portalAuthorizationSimulation: 'Active',
      portalRuntimeConnected: false,
      authOwnedBy: 'PortalCorporativo',
      crmOwnsAuth: false,
      credentialStorage: false,
      productiveAuthorization: false,
      authorizationNextGate: 'Sprint2P4ControlledCrudBehindFoundationFlag',
      foundationCrud: 'Enabled',
      leadFoundationCrud: 'Enabled',
      accountFoundationCrud: 'Enabled',
      contactFoundationCrud: 'Enabled',
      crudAuthorizationMode: 'FoundationSimulation',
      crudNextGate: 'Sprint2P5IntegrationReadinessReview',
      sprint2P5ReadinessReview: 'Active',
      databaseReady: false,
      authReady: false,
      productiveCrudReady: false,
      recommendedDecision: 'ContinueReview',
      productizationNextGate: 'Sprint2P6ProductizationGateDecision',
      sprint2: 'Closed',
      productizationStatus: 'NotReady',
      overallDecision: 'NoGoForProductiveActivation',
      foundationCrudDecision: 'GoFoundationOnly',
      durablePersistenceDecision: 'NoGo',
      realDatabaseDecision: 'NoGo',
      portalAuthRuntimeDecision: 'NoGo',
      productiveCrudApiDecision: 'NoGo',
      sprint3PlanningDecision: 'Go',
      sprint3NextGate: 'Sprint3P1DurablePersistenceSetupDesign',
      sprint3P1DurablePersistenceSetup: 'DesignOnly',
      realDatabaseConfigured: false,
      efRuntimeEnabled: false,
      migrationsCreated: false,
      connectionStringsConfigured: false,
      sqlServerOwnedByCrm: false,
      secretStrategy: 'PlannedOnly',
      migrationStrategy: 'PlannedOnly',
      durablePersistenceSetupNextGate: 'Sprint3P2CommonDbConnectionContractAndSecretStrategy',
      sprint3P2CommonDbStrategy: 'ContractOnly',
      logicalDatabaseName: 'CrmDb',
      logicalDbPlaceholder: true,
      secretProviderConfigured: false,
      secretProviderRuntimeConnected: false,
      commonDbStrategyNextGate: 'Sprint3P3EfDbContextPrototypeBehindDisabledFlag',
      sprint3P3EfPrototype: 'Exists',
      efPrototypeExists: true,
      dbContextRuntimeActive: false,
      providerConfigured: false,
      useSqlServerConfigured: false,
      foundationStoresRemainActive: true,
      productiveCrudEnabled: false,
      efPrototypeWarning: 'EF/DbContext prototype only; runtime disabled and no database configured',
      efPrototypeNextGate: 'Sprint3P4PortalAuthRuntimeContractValidation',
      sprint3P4PortalAuthRuntimeContract: 'ContractOnly',
      authRuntimeEnabled: false,
      credentialRuntimeStorageEnabled: false,
      crmLoginImplementedByCrm: false,
      identityImplementedByCrm: false,
      permissionsPersistedInCrm: false,
      foundationSimulationActive: true,
      productiveAuthorizationEnabled: false,
      portalAuthRuntimeWarning: 'Portal Auth runtime contract validation only; no real Auth runtime configured',
      portalAuthRuntimeNextGate: 'Sprint3P5ProductiveApiRouteDraftBehindDisabledFlag',
      sprint3P5ProductiveApiDraft: 'Exists',
      productiveApiRouteDraftExists: true,
      productiveRoutesRegistered: false,
      durablePersistenceEnabled: false,
      deleteEndpointsEnabled: false,
      foundationCrudStillSeparate: true,
      productiveApiDraftWarning: 'Productive API route draft only; routes are not active',
      productiveApiDraftNextGate: 'Sprint3P6Sprint3ProductizationReview',
      sprint3: 'Closed',
      productizationReview: 'Completed',
      sprint3OverallDecision: 'NoGoForRealActivation',
      sprint3ProductizationStatus: 'NotReady',
      sprint3DurablePersistence: 'NoGo',
      sprint3RealDatabase: 'NoGo',
      sprint3EfRuntime: 'NoGo',
      sprint3PortalAuthRuntime: 'NoGo',
      sprint3ProductiveApiRoutes: 'NoGo',
      sprint3ProductiveCrmUi: 'NoGo',
      sprint3FoundationCapabilities: 'GoFoundationOnly',
      sprint4Planning: 'Go',
      sprint3ProductizationWarning: 'Sprint 3 productization review only; no real activation',
      sprint4NextGate: 'Sprint4P1RuntimeEnvironmentReadinessAndLocalToolingHardening',
      sprint4P1RuntimeReadiness: 'Active',
      dockerComposeExpected: true,
      crmApiPort: 8093,
      nodePathRequiredForFrontendVerifier: false,
      productiveRoutesActive: false,
      sprint4RuntimeReadinessWarning: 'Runtime readiness only; no real activation',
      sprint4P1NextGate: 'Sprint4P2ControlledCommonDbRuntimeProbeBehindDisabledFlag',
      sprint4P2CommonDbRuntimeProbe: 'Exists',
      commonDbRuntimeProbeEnabled: false,
      commonDbRealDatabaseConfigured: false,
      commonDbConnectionStringsConfigured: false,
      commonDbSecretProviderRuntimeConnected: false,
      dbConnectionAttemptedByRuntime: false,
      commonDbSqlServerOwnedByCrm: false,
      commonDbEfRuntimeEnabled: false,
      commonDbContextRuntimeActive: false,
      commonDbMigrationsCreated: false,
      commonDbDurablePersistenceEnabled: false,
      commonDbApiRequiresDatabase: false,
      commonDbRuntimeProbeWarning: 'Common DB runtime probe exists but is disabled; no database connection is attempted',
      commonDbRuntimeProbeNextGate: 'Sprint4P3PortalAuthRuntimeProbeBehindDisabledFlag',
      sprint4P3PortalAuthRuntimeProbe: 'Exists',
      portalAuthRuntimeProbeEnabled: false,
      portalAuthProbePortalRuntimeConnected: false,
      portalAuthProbeAuthRuntimeEnabled: false,
      portalAuthProbeProductiveAuthorizationEnabled: false,
      tokenReadAttemptedByRuntime: false,
      portalHttpAttemptedByRuntime: false,
      portalAuthProbeLoginImplementedByCrm: false,
      portalAuthProbeIdentityImplementedByCrm: false,
      portalAuthProbePermissionsPersistedInCrm: false,
      portalAuthProbeFoundationSimulationActive: true,
      portalAuthRuntimeProbeWarning: 'Portal Auth runtime probe exists but is disabled; no tokens are read and no Portal HTTP calls are attempted',
      portalAuthRuntimeProbeNextGate: 'Sprint4P4ProductiveRoutesLockedStubValidation',
      sprint4P4ProductiveRoutesLockedStubValidation: 'Active',
      lockedStubsStrategy: 'DocumentOnlyPreferred',
      p4ProductiveRoutesRegistered: false,
      lockedStubsRegistered: false,
      p4ProductiveCrudEnabled: false,
      p4ProductiveAuthorizationEnabled: false,
      p4DeleteEndpointsEnabled: false,
      dbRequired: false,
      authRuntimeRequired: false,
      p4FoundationCrudStillSeparate: true,
      productiveRoutesLockedStubWarning: 'Productive routes locked stub validation only; no productive routes are active',
      productiveRoutesLockedStubNextGate: 'Sprint4P5NonProductionE2EPilotReadiness',
      sprint4P5NonProductionE2EPilotReadiness: 'Prepared',
      e2ePilotCanRun: true,
      e2ePilotScope: 'FoundationOnly',
      productiveRoutesUsed: false,
      realDatabaseUsed: false,
      portalAuthRuntimeUsed: false,
      durablePersistenceUsed: false,
      deleteOperationsUsed: false,
      syntheticDataOnly: true,
      foundationEndpointsOnly: true,
      negativeRouteValidationRequired: true,
      e2ePilotReadinessWarning: 'Non-production E2E pilot readiness only; no real activation',
      e2ePilotReadinessNextGate: 'Sprint4P6Sprint4GateDecision',
      sprint4: 'Closed',
      sprint4GateDecision: 'Completed',
      sprint4OverallDecision: 'GoForNonProductionFoundationPilot',
      realActivationDecision: 'NoGo',
      sprint4ProductizationStatus: 'NotReady',
      commonDbRuntimeDecision: 'NoGoForRuntimeActivation',
      sprint4PortalAuthRuntimeDecision: 'NoGoForRuntimeActivation',
      productiveRoutesDecision: 'NoGo',
      productiveCrudDecision: 'NoGo',
      deleteDecision: 'NoGo',
      productiveUiDecision: 'NoGo',
      nonProductionE2EPilotDecision: 'GoFoundationOnly',
      sprint5PlanningDecision: 'Go',
      sprint4GateDecisionWarning: 'Sprint 4 gate decision only; no real activation',
      sprint4GateDecisionNextGate: 'Sprint5P1ControlledRuntimeProbeActivationPlan',
      sprint5P1ControlledRuntimeProbeActivationPlan: 'Exists',
      runtimeProbeActivationApproved: false,
      commonDbProbeActivationApproved: false,
      portalAuthProbeActivationApproved: false,
      productiveRoutesActivationApproved: false,
      realActivationApproved: false,
      nonProductionOnly: true,
      syntheticDataRequired: true,
      rollbackPlanRequired: true,
      observabilityRequired: true,
      secretProviderRequired: true,
      deleteStillNoGo: true,
      runtimeProbeActivationPlanWarning: 'Runtime probe activation plan only; no runtime activation approved',
      runtimeProbeActivationPlanNextGate: 'Sprint5P2SecretProviderRuntimeContractValidation',
      sprint5P2SecretProviderRuntimeContract: 'Exists',
      secretProviderContractExists: true,
      p2SecretProviderRuntimeConnected: false,
      secretProviderReadsEnabled: false,
      secretReadAttemptedByRuntime: false,
      realSecretsConfigured: false,
      envFileRequired: false,
      p2ConnectionStringsConfigured: false,
      keyVaultClientConfigured: false,
      secretValuesExposed: false,
      p2RuntimeProbeActivationApproved: false,
      p2CommonDbProbeActivationApproved: false,
      p2PortalAuthProbeActivationApproved: false,
      secretProviderRuntimeContractWarning: 'Secret Provider contract validation only; no secrets are read',
      secretProviderRuntimeContractNextGate: 'Sprint5P3CommonDbProbeOptionalActivationInNonProduction',
      sprint5P3CommonDbProbeOptionalActivation: 'Exists',
      commonDbProbeOptionalActivationExists: true,
      p3CommonDbProbeActivationApproved: false,
      p3CommonDbProbeEnabled: false,
      p3CommonDbConnectionAttempted: false,
      p3SecretProviderRuntimeRequired: true,
      p3SecretProviderRuntimeConnected: false,
      secretReadsRequiredBeforeActivation: true,
      p3SecretReadsEnabled: false,
      p3RealDatabaseConfigured: false,
      p3ConnectionStringsConfigured: false,
      p3EfRuntimeEnabled: false,
      p3MigrationsCreated: false,
      p3DurablePersistenceEnabled: false,
      p3ApiRequiresDatabase: false,
      p3NonProductionOnly: true,
      p3SyntheticDataRequired: true,
      p3RollbackRequired: true,
      commonDbProbeOptionalActivationWarning: 'Common DB probe optional activation only; no database connection is attempted',
      commonDbProbeOptionalActivationNextGate: 'Sprint5P4PortalAuthProbeOptionalActivationInNonProduction',
      sprint5P5LockedProductiveRouteStubTrial: 'Exists',
      lockedProductiveRouteStubTrialExists: true,
      lockedProductiveRouteStubRegistrationApproved: false,
      lockedProductiveRouteStubsRegistered: false,
      p5ProductiveRoutesRegistered: false,
      p5ProductiveCrudEnabled: false,
      p5ProductiveAuthorizationEnabled: false,
      p5DeleteEndpointsEnabled: false,
      runtimeFlagDefaultEnabled: false,
      lockedResponseIfEnabled: 423,
      defaultNegativeRouteStatus: 404,
      p5FoundationCrudStillSeparate: true,
      p5DbRequired: false,
      p5AuthRuntimeRequired: false,
      p5PortalRuntimeRequired: false,
      lockedProductiveRouteStubTrialWarning: 'Locked productive route stub trial only; no productive routes are registered by default',
      lockedProductiveRouteStubTrialNextGate: 'Sprint5P6Sprint5GateDecision',
      sprint5: 'Closed',
      sprint5GateDecision: 'Completed',
      sprint5OverallDecision: 'GoForControlledNonProductionPreparation',
      sprint5RealActivationDecision: 'NoGo',
      sprint5ProductizationStatus: 'NotReady',
      sprint5SecretProviderRuntimeDecision: 'NoGoForRuntimeRead',
      sprint5CommonDbRuntimeDecision: 'NoGoForConnectionAttempt',
      sprint5PortalAuthRuntimeDecision: 'NoGoForPortalHttpOrTokenRead',
      sprint5ProductiveRoutesDecision: 'NoGo',
      sprint5LockedStubRuntimeDecision: 'NoGoForRuntimeRegistration',
      sprint5ProductiveCrudDecision: 'NoGo',
      sprint5DeleteDecision: 'NoGo',
      sprint5ProductiveUiDecision: 'NoGo',
      sprint6PlanningDecision: 'Go',
      sprint5GateDecisionWarning: 'Sprint 5 gate decision only; no real activation',
      sprint5GateDecisionNextGate: 'Sprint6P1NonProductionRuntimeApprovalPackage',
      sprint6P1NonProductionRuntimeApprovalPackage: 'Exists',
      nonProductionRuntimeApprovalPackageExists: true,
      nonProductionRuntimeApprovalGranted: false,
      secretProviderMockApprovalGranted: false,
      commonDbDryRunApprovalGranted: false,
      portalAuthDryRunApprovalGranted: false,
      lockedStubRuntimeTrialApprovalGranted: false,
      sprint6RealActivationApprovalGranted: false,
      sprint6ProductiveRoutesApprovalGranted: false,
      sprint6DeleteApprovalGranted: false,
      sprint6SyntheticDataApprovalRequired: true,
      sprint6RollbackApprovalRequired: true,
      sprint6ObservabilityApprovalRequired: true,
      sprint6SecurityReviewRequired: true,
      sprint6ArchitectureReviewRequired: true,
      sprint6P1Warning: 'NonProduction runtime approval package only; no runtime approval is granted',
      sprint6P1NextGate: 'Sprint6P2SecretProviderSafeMockActivation',
      sprint6P2SecretProviderSafeMockActivation: 'Enabled',
      secretProviderSafeMockExists: true,
      secretProviderSafeMockEnabled: true,
      sprint6SecretProviderRuntimeConnected: false,
      readsRealSecrets: false,
      readsSyntheticValues: true,
      readsEnabledForMockOnly: true,
      sprint6RealSecretsConfigured: false,
      sprint6EnvFileRequired: false,
      sprint6KeyVaultClientConfigured: false,
      azureSdkForSecretsConfigured: false,
      secretValuesExposedInLogs: false,
      sprint6CommonDbDryRunApprovalGranted: false,
      sprint6PortalAuthDryRunApprovalGranted: false,
      sprint6P2RealActivationApprovalGranted: false,
      sprint6P2Warning: 'Secret Provider safe mock only; no real secrets are read',
      sprint6P2NextGate: 'Sprint6P3CommonDbConnectivityDryRunContract',
      sprint6P3CommonDbConnectivityDryRunContract: 'Exists',
      commonDbConnectivityDryRunContractExists: true,
      sprint6P3CommonDbDryRunApprovalGranted: false,
      sprint6P3CommonDbDryRunEnabled: false,
      sprint6P3CommonDbConnectionAttempted: false,
      usesSecretProviderSafeMockMetadata: true,
      usesSyntheticConnectionReference: true,
      syntheticConnectionReference: 'mock://crm/common-db',
      realConnectionStringUsed: false,
      connectionStringResolved: false,
      sqlConnectionCreated: false,
      dbConnectionCreated: false,
      sprint6P3EfRuntimeEnabled: false,
      sprint6P3MigrationsCreated: false,
      sprint6P3ApiRequiresDatabase: false,
      sprint6P3Warning: 'Common DB connectivity dry-run contract only; no database connection is attempted',
      sprint6P3NextGate: 'Sprint6P4PortalAuthTokenPropagationDryRunContract',
      sprint6P4PortalAuthTokenPropagationDryRunContract: 'Exists',
      portalAuthTokenPropagationDryRunContractExists: true,
      sprint6P4PortalAuthDryRunApprovalGranted: false,
      sprint6P4PortalAuthDryRunEnabled: false,
      sprint6P4PortalAuthRuntimeConnected: false,
      tokenReadAttempted: false,
      headerReadAttempted: false,
      portalHttpAttempted: false,
      usesSyntheticTokenMetadata: true,
      syntheticTokenReference: 'mock://crm/portal-auth-token',
      syntheticUserReference: 'mock://crm/portal-user',
      realTokenUsed: false,
      realHeadersRead: false,
      sprint6P4LoginImplementedByCrm: false,
      sprint6P4IdentityImplementedByCrm: false,
      sprint6P4PermissionsPersistedInCrm: false,
      sprint6P4ProductiveAuthorizationEnabled: false,
      sprint6P4Warning: 'Portal Auth token propagation dry-run contract only; no real tokens or headers are read',
      sprint6P4NextGate: 'Sprint6P5LockedStubRuntimeRegistrationTrial',
      sprint6P5LockedStubRuntimeRegistrationTrial: 'Exists',
      lockedStubRuntimeRegistrationTrialExists: true,
      lockedStubRuntimeRegistrationApprovalGranted: false,
      lockedStubRuntimeRegistrationEnabled: false,
      lockedStubsRegisteredAtRuntime: false,
      sprint6P5ProductiveRoutesRegistered: false,
      sprint6P5ProductiveCrudEnabled: false,
      sprint6P5DeleteEndpointsEnabled: false,
      sprint6P5DefaultNegativeRouteStatus: 404,
      sprint6P5FutureLockedResponseStatusIfExplicitlyEnabled: 423,
      sprint6P5RuntimeFlagDefaultEnabled: false,
      usesDomainServices: false,
      usesFoundationStores: false,
      usesDatabase: false,
      usesPortalAuth: false,
      usesTokenOrHeaderReads: false,
      runtimeRegistrationDecision: 'DocumentOnlyPreferredWithNoRuntimeRegistration',
      sprint6P5Warning: 'Locked stub runtime registration trial only; no productive routes are registered by default',
      sprint6P5NextGate: 'Sprint6P6Sprint6GateDecision',
      sprint6: 'Closed',
      sprint6GateDecision: 'Completed',
      sprint6OverallDecision: 'GoForSprint7ControlledNonProductionActivationPlanning',
      sprint6RealActivationDecision: 'NoGo',
      secretProviderRealRuntimeDecision: 'NoGo',
      commonDbRealConnectionDecision: 'NoGo',
      sprint6PortalAuthRealRuntimeDecision: 'NoGo',
      lockedStubRuntimeRegistrationDecision: 'NoGo',
      sprint6ProductiveRoutesDecision: 'NoGo',
      sprint6ProductiveCrudDecision: 'NoGo',
      sprint6DeleteDecision: 'NoGo',
      sprint6ProductiveUiDecision: 'NoGo',
      sprint6ProductizationStatus: 'NotReady',
      sprint7PlanningDecision: 'Go',
      sprint6GateDecisionWarning: 'Sprint 6 gate decision only; no real activation',
      sprint6GateDecisionNextGate: 'Sprint7P1SecretProviderRealNonProductionApproval',
      sprint7P1SecretProviderRealNonProductionApproval: 'Exists',
      secretProviderRealNonProductionApprovalPackageExists: true,
      secretProviderRealNonProductionApprovalGranted: false,
      secretProviderRealRuntimeEnabled: false,
      secretProviderRealRuntimeConnected: false,
      realSecretReadAttempted: false,
      keyVaultRuntimeClientEnabled: false,
      azureSecretSdkRuntimeEnabled: false,
      sprint7EnvFileRequired: false,
      envSecretReadAllowed: false,
      secretsLogged: false,
      secretNamesApproved: false,
      secretValuesApproved: false,
      sprint7SecurityReviewRequired: true,
      sprint7ArchitectureReviewRequired: true,
      devOpsReviewRequired: true,
      sprint7RollbackRequired: true,
      sprint7ObservabilityRequired: true,
      sprint7P1Warning: 'Secret Provider real NonProduction approval package only; no real secrets are read',
      sprint7P1NextGate: 'Sprint7P2SecretProviderRealNonProductionRuntimeProbe',
      sprint7P2SecretProviderRealNonProductionRuntimeProbe: 'Exists',
      secretProviderRealNonProductionRuntimeProbeExists: true,
      secretProviderRealRuntimeProbeEnabled: false,
      secretProviderRealRuntimeProbeAttempted: false,
      secretProviderRealValueMaterialized: false,
      secretProviderRealValueLogged: false,
      secretValueReturnedToApi: false,
      keyVaultRuntimeClientCreated: false,
      keyVaultRuntimeCallAttempted: false,
      envSecretReadAttempted: false,
      logicalSecretNamesValidated: true,
      p2SecretValuesValidated: false,
      probeSkippedBecauseApprovalNotGranted: true,
      sprint7P2Warning: 'Secret Provider real NonProduction runtime probe is prepared but skipped because approval is not granted',
      sprint7P2NextGate: 'Sprint7P3CommonDbRealConnectivityNonProductionProbe',
      sprint7P3CommonDbRealConnectivityNonProductionProbe: 'Exists',
      commonDbRealConnectivityNonProductionProbeExists: true,
      commonDbRealConnectivityApprovalGranted: false,
      p3ConnectionStringResolved: false,
      p3ConnectionStringValueMaterialized: false,
      p3ConnectionStringLogged: false,
      p3ConnectionStringReturnedToApi: false,
      commonDbProbeEnabled: false,
      commonDbProbeAttempted: false,
      commonDbConnected: false,
      p3SqlConnectionCreated: false,
      p3DbConnectionCreated: false,
      useSqlServerEnabled: false,
      addDbContextRuntimeEnabled: false,
      databaseSchemaChanged: false,
      productivePersistenceEnabled: false,
      apiRequiresDatabase: false,
      usesSecretProviderRuntime: false,
      usesSyntheticFallback: true,
      p3SyntheticConnectionReference: 'mock://crm/common-db',
      connectionProbeSkippedBecauseSecretProviderApprovalNotGranted: true,
      sprint7P3Warning: 'Common DB real connectivity NonProduction probe is prepared but skipped because Secret Provider approval is not granted',
      sprint7P3NextGate: 'Sprint7P4PortalAuthRealRuntimeProbe',
      sprint7P4PortalAuthRealRuntimeProbe: 'Exists',
      portalAuthRealRuntimeProbeExists: true,
      portalAuthRealRuntimeApprovalGranted: false,
      p4SecretProviderRealNonProductionApprovalGranted: false,
      portalAuthRealRuntimeProbeEnabled: false,
      portalAuthRealRuntimeProbeAttempted: false,
      portalAuthRealRuntimeConnected: false,
      portalAuthBaseUrlResolved: false,
      portalAuthBaseUrlMaterialized: false,
      portalAuthBaseUrlLogged: false,
      portalAuthBaseUrlReturnedToApi: false,
      portalHttpClientCreated: false,
      portalHttpCallAttempted: false,
      portalAuthTokenValidationAttempted: false,
      p4TokenReadAttempted: false,
      p4HeaderReadAttempted: false,
      authorizationHeaderReadAttempted: false,
      realTokenMaterialized: false,
      realTokenLogged: false,
      tokenReturnedToApi: false,
      p4LoginImplementedByCrm: false,
      p4LogoutImplementedByCrm: false,
      p4IdentityImplementedByCrm: false,
      rolesPersistedInCrm: false,
      p4PermissionsPersistedInCrm: false,
      sprint7P4ProductiveAuthorizationEnabled: false,
      apiRequiresPortalAuth: false,
      p4UsesSyntheticFallback: true,
      syntheticPortalAuthReference: 'mock://crm/portal-auth',
      p4SyntheticUserReference: 'mock://crm/portal-user',
      probeSkippedBecausePortalAuthApprovalNotGranted: true,
      sprint7P4Warning: 'Portal Auth real runtime probe is prepared but skipped because Portal Auth approval is not granted',
      sprint7P4NextGate: 'Sprint7P5LockedProductiveRouteRuntimeRegistrationWith423',
      sprint7P5LockedProductiveRouteRuntimeRegistrationWith423: 'Exists',
      lockedProductiveRouteRuntimeRegistrationApprovalGranted: false,
      lockedProductiveRouteRuntimeRegistrationEnabled: false,
      sprint7P5ProductiveRoutesRegisteredByDefault: false,
      sprint7P5ProductiveRoutesRegisteredWhenExplicitlyEnabled: true,
      sprint7P5DefaultNegativeRouteStatus: 404,
      sprint7P5ExplicitlyEnabledLockedRouteStatus: 423,
      sprint7P5ProductiveCrudEnabled: false,
      sprint7P5ProductiveDomainExecutionEnabled: false,
      sprint7P5ProductivePersistenceEnabled: false,
      sprint7P5DeleteEndpointsEnabled: false,
      sprint7P5PortalAuthRuntimeRequired: false,
      sprint7P5PortalAuthRuntimeEnabled: false,
      sprint7P5TokenReadAttempted: false,
      sprint7P5HeaderReadAttempted: false,
      sprint7P5DbRuntimeEnabled: false,
      sprint7P5EfRuntimeEnabled: false,
      sprint7P5MigrationsCreated: false,
      sprint7P5SideEffectsAllowed: false,
      sprint7P5Warning: 'Locked productive routes are not registered by default; explicit NonProduction flag returns 423 without side effects',
      sprint7P5NextGate: 'Sprint7P6Sprint7GateDecision',
      sprint7: 'Closed',
      sprint7GateDecision: 'Completed',
      sprint7OverallDecision: 'GoForSprint8ControlledRuntimeApprovalAndPilotPlanning',
      sprint7RealActivationDecision: 'NoGo',
      sprint7SecretProviderRealRuntime: 'NoGo',
      sprint7CommonDbRealConnection: 'NoGo',
      sprint7PortalAuthRealRuntime: 'NoGo',
      sprint7LockedProductiveRouteRegistration: 'GoOnlyAsExplicitNonProductionLocked423',
      sprint7ProductiveRoutesDefault: 'NoGo',
      sprint7ProductiveCrud: 'NoGo',
      sprint7Delete: 'NoGo',
      sprint7ProductiveUi: 'NoGo',
      sprint7ProductizationStatus: 'NotReady',
      sprint8Planning: 'Go',
      sprint7GateDecisionWarning: 'Sprint 7 gate decision only; no real activation',
      sprint7GateDecisionNextGate: 'Sprint8P1SecretProviderApprovalDecision',
      sprint8P1SecretProviderApprovalDecision: 'Exists',
      sprint8P1SecretProviderApprovalDecisionValue: 'ApprovedForControlledNonProductionReadPlanning',
      sprint8P1SecretProviderRealReadApprovedForNextSprint: true,
      sprint8P1SecretProviderRealReadEnabledNow: false,
      sprint8P1RealSecretReadAttempted: false,
      sprint8P1RealSecretValueMaterialized: false,
      sprint8P1RealSecretValueLogged: false,
      sprint8P1SecretValueReturnedToApi: false,
      sprint8P1KeyVaultRuntimeClientCreated: false,
      sprint8P1KeyVaultRuntimeCallAttempted: false,
      sprint8P1AzureSecretSdkRuntimeEnabled: false,
      sprint8P1EnvFileRequired: false,
      sprint8P1EnvSecretReadAllowed: false,
      sprint8P1ApprovedSecretNamesOnly: true,
      sprint8P1ApprovedSecretValues: false,
      sprint8P1ApprovedForNonProductionOnly: true,
      sprint8P1SecurityApprovalRecorded: true,
      sprint8P1ArchitectureApprovalRecorded: true,
      sprint8P1DevOpsApprovalRecorded: true,
      sprint8P1RollbackPlanApproved: true,
      sprint8P1ObservabilityPlanApproved: true,
      sprint8P1RedactionPlanApproved: true,
      sprint8P1Warning: 'Secret Provider approval decision only; no real secret read in Sprint 8 P1',
      sprint8P1NextGate: 'Sprint8P2SecretProviderControlledRealNonProductionRead',
      sprint8P2SecretProviderControlledRealNonProductionRead: 'Exists',
      secretProviderControlledRealNonProductionReadApproved: true,
      secretProviderControlledRealNonProductionReadEnabled: false,
      secretProviderControlledRealNonProductionReadAttempted: false,
      sprint8P2RealSecretReadAttempted: false,
      sprint8P2RealSecretValueMaterialized: false,
      sprint8P2RealSecretValueLogged: false,
      sprint8P2SecretValueReturnedToApi: false,
      sprint8P2SecretValuePersisted: false,
      sprint8P2SecretValueCached: false,
      sprint8P2KeyVaultRuntimeClientCreated: false,
      sprint8P2KeyVaultRuntimeCallAttempted: false,
      sprint8P2AzureSecretSdkRuntimeEnabled: false,
      sprint8P2UsesApprovedSecretNamesOnly: true,
      sprint8P2NonProductionOnly: true,
      sprint8P2FailClosedByDefault: true,
      sprint8P2NextGate: 'Sprint8P3CommonDbControlledRealConnectivity',
      sprint8P3CommonDbControlledRealConnectivity: 'Exists',
      commonDbControlledRealConnectivityApproved: true,
      commonDbControlledRealConnectivityEnabled: false,
      commonDbConnectivityAttempted: false,
      sprint8P3CommonDbConnected: false,
      secretProviderAvailabilityMetadataUsed: true,
      sprint8P3SecretValueReturnedToApi: false,
      sprint8P3ConnectionStringResolved: false,
      connectionStringMaterializedInPublicContract: false,
      connectionStringLogged: false,
      connectionStringReturnedToApi: false,
      sprint8P3SqlConnectionCreated: false,
      sprint8P3DbConnectionCreated: false,
      sprint8P3DbConnectionOpened: false,
      sprint8P3EfRuntimeEnabled: false,
      sprint8P3AddDbContextRuntimeEnabled: false,
      sprint8P3UseSqlServerEnabled: false,
      sprint8P3MigrationsCreated: false,
      sprint8P3DatabaseSchemaChanged: false,
      sprint8P3ProductivePersistenceEnabled: false,
      sprint8P3ProductiveCrudEnabled: false,
      sprint8P3ApiRequiresDatabase: false,
      sprint8P3NonProductionOnly: true,
      sprint8P3FailClosedByDefault: true,
      sprint8P3NextGate: 'Sprint8P4PortalAuthControlledRealRuntimeValidation',
      sprint8P4PortalAuthControlledRealRuntimeValidation: 'Exists',
      portalAuthControlledRealRuntimeValidationApproved: true,
      portalAuthControlledRealRuntimeValidationEnabled: false,
      portalAuthRuntimeValidationAttempted: false,
      portalAuthRuntimeConnected: false,
      sprint8P4SecretProviderAvailabilityMetadataUsed: true,
      sprint8P4PortalAuthBaseUrlResolved: false,
      portalAuthBaseUrlMaterializedInPublicContract: false,
      sprint8P4PortalAuthBaseUrlLogged: false,
      sprint8P4PortalAuthBaseUrlReturnedToApi: false,
      sprint8P4PortalHttpClientCreated: false,
      sprint8P4PortalHttpCallAttempted: false,
      sprint8P4TokenReadAttempted: false,
      sprint8P4HeaderReadAttempted: false,
      sprint8P4AuthorizationHeaderReadAttempted: false,
      sprint8P4RealTokenMaterialized: false,
      sprint8P4RealTokenLogged: false,
      sprint8P4TokenReturnedToApi: false,
      sprint8P4LoginImplementedByCrm: false,
      sprint8P4LogoutImplementedByCrm: false,
      sprint8P4IdentityImplementedByCrm: false,
      sprint8P4RolesPersistedInCrm: false,
      sprint8P4PermissionsPersistedInCrm: false,
      sprint8P4ProductiveAuthorizationEnabled: false,
      sprint8P4ApiRequiresPortalAuth: false,
      sprint8P4NonProductionOnly: true,
      sprint8P4FailClosedByDefault: true,
      sprint8P4NextGate: 'Sprint8P5LockedRouteAuthorizationPolicyIntegration',
      sprint8P5LockedRouteAuthorizationPolicyIntegration: 'Exists',
      lockedRouteAuthorizationPolicyIntegrationApproved: true,
      lockedRouteAuthorizationPolicyIntegrationEnabled: false,
      authorizationPolicyEvaluated: false,
      authorizationPolicyDecision: 'NotEvaluatedBecauseDisabled',
      portalAuthMetadataUsed: true,
      sprint8P5PortalAuthRuntimeRequired: false,
      sprint8P5PortalAuthRuntimeConnected: false,
      sprint8P5TokenReadAttempted: false,
      sprint8P5HeaderReadAttempted: false,
      sprint8P5AuthorizationHeaderReadAttempted: false,
      sprint8P5PortalHttpCallAttempted: false,
      productiveRoutesRegisteredByDefault: false,
      sprint8P5DefaultNegativeRouteStatus: 404,
      lockedRoutesEnabledOnlyWithExplicitNonProductionFlag: true,
      sprint8P5LockedRouteStatus: 423,
      lockedRouteAuthorizationDecisionReturned: false,
      sprint8P5ProductiveCrudEnabled: false,
      sprint8P5ProductiveDomainExecutionEnabled: false,
      sprint8P5ProductivePersistenceEnabled: false,
      sprint8P5DeleteEndpointsEnabled: false,
      sprint8P5SideEffectsAllowed: false,
      sprint8P5DbRuntimeEnabled: false,
      sprint8P5EfRuntimeEnabled: false,
      sprint8P5NonProductionOnly: true,
      sprint8P5FailClosedByDefault: true,
      sprint8P5NextGate: 'Sprint8P6Sprint8GateDecision',
      sprint8: 'Closed',
      sprint8GateDecision: 'Completed',
      sprint8OverallDecision: 'GoForSprint9ControlledRuntimeActivationPlanning',
      realProductionActivationDecision: 'NoGo',
      secretProviderControlledReadDecision: 'GoOnlyAsExplicitNonProductionFlag',
      commonDbControlledConnectivityDecision: 'GoOnlyAsExplicitNonProductionFlag',
      portalAuthControlledValidationDecision: 'GoOnlyAsExplicitNonProductionFlag',
      lockedRouteAuthorizationPolicyDecision: 'GoOnlyAsExplicitNonProductionLocked423',
      sprint8ProductiveRoutesDefaultDecision: 'NoGo',
      sprint8ProductiveCrudDecision: 'NoGo',
      sprint8DeleteDecision: 'NoGo',
      sprint8ProductiveUiDecision: 'NoGo',
      sprint8ProductizationStatus: 'NotReady',
      sprint9PlanningDecision: 'Go',
      sprint8GateDecisionNextGate: 'Sprint9P1ControlledRuntimeActivationDecision',
      runtimeMode: 'NonProduction',
      apiBaseUrl: this.apiBaseUrl
    };
  }
}

@Component({
  standalone: true,
  selector: 'crm-home',
  template: `
    <section class="card">
      <h1>CRM Foundation</h1>
      <p>CRM Domain Catalog: Draft</p>
      <p>Leads Foundation: PreviewOnly</p>
      <p>Accounts Foundation: PreviewOnly</p>
      <p>Contacts Foundation: PreviewOnly</p>
      <p>Read Models: PreviewOnly</p>
      <p>Persistence Strategy: Draft</p>
      <p>Persistence: None</p>
      <p>Portal Integration Planned</p>
      <p>Portal Connected: false</p>
      <p>Capability Owner: PortalCorporativo</p>
      <p>Auth/Menu/Permissions/Audit/Notification/Configuration: External</p>
      <p>Financial Integration Planned</p>
      <p>Financial Connected: false</p>
      <p>Financial Capability Owner: Financiero</p>
      <p>Integration Pattern: API + Events + NoSharedDatabase</p>
      <p>SRI/ATS/RIDE/XAdES: NotImplementedInCRM</p>
      <p>Reporting Integration: Planned</p>
      <p>Reporting Connected: false</p>
      <p>Analytics Mode: Planned</p>
      <p>KPI Catalog: Foundation</p>
      <p>Dashboard Catalog: Foundation</p>
      <p>Power BI Embed: NotConfigured</p>
      <p>Sprint 1 Foundation: Closed</p>
      <p>Productization: NotReady</p>
      <p>Next Gate: Sprint2Planning</p>
      <p>Runtime: NonProduction</p>
      <p>Docker External Registry Note: BLOCKED_EXTERNAL_REGISTRY documented when MCR times out</p>
      <p>Persistence Design Review: Active</p>
      <p>Persistence Seam: Active</p>
      <p>Persistence Mode: NonProductionSeam</p>
      <p>Foundation Store: Enabled</p>
      <p>Database Configured: false</p>
      <p>DbContext Configured: false</p>
      <p>Migration Ready: false</p>
      <p>Durable Persistence: false</p>
      <p>Productive CRUD: false</p>
      <p>Persistence Next Gate: Sprint2P3PortalAuthorizationAdapterSimulation</p>
      <p>Portal Authorization Simulation: Active</p>
      <p>Portal Runtime Connected: false</p>
      <p>Auth Owned By: PortalCorporativo</p>
      <p>CRM Owns Auth: false</p>
      <p>Token Storage: false</p>
      <p>Productive Authorization: false</p>
      <p>Authorization Next Gate: Sprint2P4ControlledCrudBehindFoundationFlag</p>
      <p>Foundation CRUD: Enabled</p>
      <p>Lead Foundation CRUD: Enabled</p>
      <p>Account Foundation CRUD: Enabled</p>
      <p>Contact Foundation CRUD: Enabled</p>
      <p>Authorization Mode: FoundationSimulation</p>
      <p>CRUD Next Gate: Sprint2P5IntegrationReadinessReview</p>
      <p>Sprint 2 P5 Readiness Review: Active</p>
      <p>Database Ready: false</p>
      <p>Auth Ready: false</p>
      <p>Productive CRUD Ready: false</p>
      <p>Recommended Decision: ContinueReview</p>
      <p>Productization Next Gate: Sprint2P6ProductizationGateDecision</p>
      <p>Sprint 2: Closed</p>
      <p>Productization Status: NotReady</p>
      <p>Overall Decision: NoGoForProductiveActivation</p>
      <p>Foundation CRUD Decision: GoFoundationOnly</p>
      <p>Durable Persistence Decision: NoGo</p>
      <p>Real Database Decision: NoGo</p>
      <p>Portal Auth Runtime Decision: NoGo</p>
      <p>Productive CRUD API Decision: NoGo</p>
      <p>Sprint 3 Planning: Go</p>
      <p>Next Gate: Sprint3P1DurablePersistenceSetupDesign</p>
      <p>Sprint 3 P1 Durable Persistence Setup: DesignOnly</p>
      <p>Real Database Configured: false</p>
      <p>EF Runtime Enabled: false</p>
      <p>Migrations Created: false</p>
      <p>Connection Strings Configured: false</p>
      <p>SQL Server Owned By CRM: false</p>
      <p>Secret Strategy: PlannedOnly</p>
      <p>Migration Strategy: PlannedOnly</p>
      <p>Productive Activation: NoGo</p>
      <p>Next Gate: Sprint3P2CommonDbConnectionContractAndSecretStrategy</p>
      <p>Sprint 3 P2 Common DB Strategy: ContractOnly</p>
      <p>Logical Database Name: CrmDb</p>
      <p>Logical DB Placeholder: true</p>
      <p>Secret Provider Configured: false</p>
      <p>Secret Provider Runtime Connected: false</p>
      <p>Next Gate: Sprint3P3EfDbContextPrototypeBehindDisabledFlag</p>
      <p>Sprint 3 P3 EF Prototype: Exists</p>
      <p>EF Prototype Exists: true</p>
      <p>EF Runtime Enabled: false</p>
      <p>DbContext Runtime Active: false</p>
      <p>Migrations Created: false</p>
      <p>Real Database Configured: false</p>
      <p>Connection Strings Configured: false</p>
      <p>Provider Configured: false</p>
      <p>UseSqlServer Configured: false</p>
      <p>Foundation Stores Remain Active: true</p>
      <p>Productive CRUD Enabled: false</p>
      <p>EF Prototype Warning: EF/DbContext prototype only; runtime disabled and no database configured</p>
      <p>Next Gate: Sprint3P4PortalAuthRuntimeContractValidation</p>
      <p>Sprint 3 P4 Portal Auth Runtime Contract: ContractOnly</p>
      <p>Portal Runtime Connected: false</p>
      <p>Auth Runtime Enabled: false</p>
      <p>CRM Owns Auth: false</p>
      <p>Auth Owned By: PortalCorporativo</p>
      <p>Token Storage Enabled: false</p>
      <p>Login Implemented By CRM: false</p>
      <p>Identity Implemented By CRM: false</p>
      <p>Permissions Persisted In CRM: false</p>
      <p>Foundation Simulation Active: true</p>
      <p>Productive Authorization Enabled: false</p>
      <p>Portal Auth Runtime Warning: Portal Auth runtime contract validation only; no real Auth runtime configured</p>
      <p>Next Gate: Sprint3P5ProductiveApiRouteDraftBehindDisabledFlag</p>
      <p>Sprint 3 P5 Productive API Draft: Exists</p>
      <p>Productive Routes Registered: false</p>
      <p>Productive CRUD Enabled: false</p>
      <p>Productive Authorization Enabled: false</p>
      <p>Auth Runtime Enabled: false</p>
      <p>Portal Runtime Connected: false</p>
      <p>Durable Persistence Enabled: false</p>
      <p>Real Database Configured: false</p>
      <p>EF Runtime Enabled: false</p>
      <p>DELETE Endpoints Enabled: false</p>
      <p>Foundation CRUD Still Separate: true</p>
      <p>Productive API Draft Warning: Productive API route draft only; routes are not active</p>
      <p>Next Gate: Sprint3P6Sprint3ProductizationReview</p>
      <p>Sprint 3: Closed</p>
      <p>Productization Review: Completed</p>
      <p>Overall Decision: NoGoForRealActivation</p>
      <p>Productization Status: NotReady</p>
      <p>Durable Persistence: NoGo</p>
      <p>Real Database: NoGo</p>
      <p>EF Runtime: NoGo</p>
      <p>Portal Auth Runtime: NoGo</p>
      <p>Productive API Routes: NoGo</p>
      <p>Productive CRM UI: NoGo</p>
      <p>Foundation Capabilities: GoFoundationOnly</p>
      <p>Sprint 4 Planning: Go</p>
      <p>Productization Review Warning: Sprint 3 productization review only; no real activation</p>
      <p>Next Gate: Sprint4P1RuntimeEnvironmentReadinessAndLocalToolingHardening</p>
      <p>Sprint 4 P1 Runtime Readiness: Active</p>
      <p>Docker Compose Expected: true</p>
      <p>CRM API Port: 8093</p>
      <p>SQL Server Owned By CRM: false</p>
      <p>Node PATH Required For Frontend Verifier: false</p>
      <p>Productive Routes Active: false</p>
      <p>DELETE Endpoints Enabled: false</p>
      <p>Real Database Configured: false</p>
      <p>Auth Runtime Enabled: false</p>
      <p>Portal Runtime Connected: false</p>
      <p>Productization Status: NotReady</p>
      <p>Runtime Readiness Warning: Runtime readiness only; no real activation</p>
      <p>Next Gate: Sprint4P2ControlledCommonDbRuntimeProbeBehindDisabledFlag</p>
      <p>Sprint 4 P2 Common DB Runtime Probe: Exists</p>
      <p>Common DB Runtime Probe Enabled: false</p>
      <p>Real Database Configured: false</p>
      <p>Connection Strings Configured: false</p>
      <p>Secret Provider Runtime Connected: false</p>
      <p>DB Connection Attempted By Runtime: false</p>
      <p>SQL Server Owned By CRM: false</p>
      <p>EF Runtime Enabled: false</p>
      <p>DbContext Runtime Active: false</p>
      <p>Migrations Created: false</p>
      <p>Durable Persistence Enabled: false</p>
      <p>API Requires Database: false</p>
      <p>Common DB Runtime Probe Warning: Common DB runtime probe exists but is disabled; no database connection is attempted</p>
      <p>Next Gate: Sprint4P3PortalAuthRuntimeProbeBehindDisabledFlag</p>
      <p>Sprint 4 P3 Portal Auth Runtime Probe: Exists</p>
      <p>Portal Auth Runtime Probe Enabled: false</p>
      <p>Portal Runtime Connected: false</p>
      <p>Auth Runtime Enabled: false</p>
      <p>Productive Authorization Enabled: false</p>
      <p>Token Read Attempted By Runtime: false</p>
      <p>Portal HTTP Attempted By Runtime: false</p>
      <p>Login Implemented By CRM: false</p>
      <p>Identity Implemented By CRM: false</p>
      <p>Permissions Persisted In CRM: false</p>
      <p>Foundation Simulation Active: true</p>
      <p>Portal Auth Runtime Probe Warning: Portal Auth runtime probe exists but is disabled; no tokens are read and no Portal HTTP calls are attempted</p>
      <p>Next Gate: Sprint4P4ProductiveRoutesLockedStubValidation</p>
      <p>Sprint 4 P4 Productive Routes Locked Stub Validation: Active</p>
      <p>Locked Stubs Strategy: DocumentOnlyPreferred</p>
      <p>Productive Routes Registered: false</p>
      <p>Locked Stubs Registered: false</p>
      <p>Productive CRUD Enabled: false</p>
      <p>Productive Authorization Enabled: false</p>
      <p>DELETE Endpoints Enabled: false</p>
      <p>DB Required: false</p>
      <p>Auth Runtime Required: false</p>
      <p>Foundation CRUD Still Separate: true</p>
      <p>Productive Routes Locked Stub Warning: Productive routes locked stub validation only; no productive routes are active</p>
      <p>Next Gate: Sprint4P5NonProductionE2EPilotReadiness</p>
      <p>Sprint 4 P5 Non-Production E2E Pilot Readiness: Prepared</p>
      <p>E2E Pilot Can Run: true</p>
      <p>E2E Pilot Scope: FoundationOnly</p>
      <p>Productive Routes Used: false</p>
      <p>Real Database Used: false</p>
      <p>Portal Auth Runtime Used: false</p>
      <p>Durable Persistence Used: false</p>
      <p>DELETE Operations Used: false</p>
      <p>Synthetic Data Only: true</p>
      <p>Foundation Endpoints Only: true</p>
      <p>Negative Route Validation Required: true</p>
      <p>E2E Pilot Readiness Warning: Non-production E2E pilot readiness only; no real activation</p>
      <p>Next Gate: Sprint4P6Sprint4GateDecision</p>
      <p>Sprint 4: Closed</p>
      <p>Sprint 4 Gate Decision: Completed</p>
      <p>Overall Decision: GoForNonProductionFoundationPilot</p>
      <p>Real Activation Decision: NoGo</p>
      <p>Productization Status: NotReady</p>
      <p>Common DB Runtime: NoGoForRuntimeActivation</p>
      <p>Portal Auth Runtime: NoGoForRuntimeActivation</p>
      <p>Productive Routes: NoGo</p>
      <p>Productive CRUD: NoGo</p>
      <p>DELETE: NoGo</p>
      <p>Productive UI: NoGo</p>
      <p>Non-Production E2E Pilot: GoFoundationOnly</p>
      <p>Sprint 5 Planning: Go</p>
      <p>Sprint 4 Gate Decision Warning: Sprint 4 gate decision only; no real activation</p>
      <p>Next Gate: Sprint5P1ControlledRuntimeProbeActivationPlan</p>
      <p>Sprint 5 P1 Controlled Runtime Probe Activation Plan: Exists</p>
      <p>Runtime Probe Activation Approved: false</p>
      <p>Common DB Probe Activation Approved: false</p>
      <p>Portal Auth Probe Activation Approved: false</p>
      <p>Productive Routes Activation Approved: false</p>
      <p>Real Activation Approved: false</p>
      <p>Non-Production Only: true</p>
      <p>Synthetic Data Required: true</p>
      <p>Rollback Plan Required: true</p>
      <p>Observability Required: true</p>
      <p>Secret Provider Required: true</p>
      <p>DELETE Still NoGo: true</p>
      <p>Runtime Probe Activation Plan Warning: Runtime probe activation plan only; no runtime activation approved</p>
      <p>Next Gate: Sprint5P2SecretProviderRuntimeContractValidation</p>
      <p>Sprint 5 P2 Secret Provider Runtime Contract: Exists</p>
      <p>Secret Provider Contract Exists: true</p>
      <p>Secret Provider Runtime Connected: false</p>
      <p>Secret Provider Reads Enabled: false</p>
      <p>Secret Read Attempted By Runtime: false</p>
      <p>Real Secrets Configured: false</p>
      <p>Env File Required: false</p>
      <p>Connection Strings Configured: false</p>
      <p>Key Vault Client Configured: false</p>
      <p>Secret Values Exposed: false</p>
      <p>Runtime Probe Activation Approved: false</p>
      <p>Common DB Probe Activation Approved: false</p>
      <p>Portal Auth Probe Activation Approved: false</p>
      <p>Secret Provider Runtime Contract Warning: Secret Provider contract validation only; no secrets are read</p>
      <p>Next Gate: Sprint5P3CommonDbProbeOptionalActivationInNonProduction</p>
      <p>Sprint 5 P3 Common DB Probe Optional Activation: Exists</p>
      <p>Common DB Probe Optional Activation Exists: true</p>
      <p>Common DB Probe Activation Approved: false</p>
      <p>Common DB Probe Enabled: false</p>
      <p>Common DB Connection Attempted: false</p>
      <p>Secret Provider Runtime Required: true</p>
      <p>Secret Provider Runtime Connected: false</p>
      <p>Secret Reads Required Before Activation: true</p>
      <p>Secret Reads Enabled: false</p>
      <p>Real Database Configured: false</p>
      <p>Connection Strings Configured: false</p>
      <p>EF Runtime Enabled: false</p>
      <p>Migrations Created: false</p>
      <p>API Requires Database: false</p>
      <p>Non-Production Only: true</p>
      <p>Synthetic Data Required: true</p>
      <p>Rollback Required: true</p>
      <p>Common DB Probe Optional Activation Warning: Common DB probe optional activation only; no database connection is attempted</p>
      <p>Next Gate: Sprint5P4PortalAuthProbeOptionalActivationInNonProduction</p>
      <p>Sprint 5 P5 Locked Productive Route Stub Trial: Exists</p>
      <p>Locked Productive Route Stub Trial Exists: true</p>
      <p>Locked Productive Route Stub Registration Approved: false</p>
      <p>Locked Productive Route Stubs Registered: false</p>
      <p>Productive Routes Registered: false</p>
      <p>Productive CRUD Enabled: false</p>
      <p>Productive Authorization Enabled: false</p>
      <p>DELETE Endpoints Enabled: false</p>
      <p>Runtime Flag Default Enabled: false</p>
      <p>Locked Response If Enabled: 423</p>
      <p>Default Negative Route Status: 404</p>
      <p>Foundation CRUD Still Separate: true</p>
      <p>DB Required: false</p>
      <p>Auth Runtime Required: false</p>
      <p>Portal Runtime Required: false</p>
      <p>Locked Productive Route Stub Trial Warning: Locked productive route stub trial only; no productive routes are registered by default</p>
      <p>Next Gate: Sprint5P6Sprint5GateDecision</p>
      <p>Sprint 5: Closed</p>
      <p>Sprint 5 Gate Decision: Completed</p>
      <p>Overall Decision: GoForControlledNonProductionPreparation</p>
      <p>Real Activation Decision: NoGo</p>
      <p>Productization Status: NotReady</p>
      <p>Secret Provider Runtime: NoGoForRuntimeRead</p>
      <p>Common DB Runtime: NoGoForConnectionAttempt</p>
      <p>Portal Auth Runtime: NoGoForPortalHttpOrTokenRead</p>
      <p>Productive Routes: NoGo</p>
      <p>Locked Stub Runtime: NoGoForRuntimeRegistration</p>
      <p>Productive CRUD: NoGo</p>
      <p>DELETE: NoGo</p>
      <p>Productive UI: NoGo</p>
      <p>Sprint 6 Planning: Go</p>
      <p>Sprint 5 Gate Decision Warning: Sprint 5 gate decision only; no real activation</p>
      <p>Next Gate: Sprint6P1NonProductionRuntimeApprovalPackage</p>
      <p>Sprint 6 P1 NonProduction Runtime Approval Package: Exists</p>
      <p>NonProduction Runtime Approval Package Exists: true</p>
      <p>NonProduction Runtime Approval Granted: false</p>
      <p>Secret Provider Mock Approval Granted: false</p>
      <p>Common DB Dry-Run Approval Granted: false</p>
      <p>Portal Auth Dry-Run Approval Granted: false</p>
      <p>Locked Stub Runtime Trial Approval Granted: false</p>
      <p>Real Activation Approval Granted: false</p>
      <p>Productive Routes Approval Granted: false</p>
      <p>DELETE Approval Granted: false</p>
      <p>Synthetic Data Approval Required: true</p>
      <p>Rollback Approval Required: true</p>
      <p>Observability Approval Required: true</p>
      <p>Security Review Required: true</p>
      <p>Architecture Review Required: true</p>
      <p>NonProduction Runtime Approval Warning: NonProduction runtime approval package only; no runtime approval is granted</p>
      <p>Next Gate: Sprint6P2SecretProviderSafeMockActivation</p>
      <p>Sprint 6 P2 Secret Provider Safe Mock Activation: Enabled</p>
      <p>Secret Provider Safe Mock Exists: true</p>
      <p>Secret Provider Safe Mock Enabled: true</p>
      <p>Secret Provider Runtime Connected: false</p>
      <p>Reads Real Secrets: false</p>
      <p>Reads Synthetic Values: true</p>
      <p>Reads Enabled For Mock Only: true</p>
      <p>Real Secrets Configured: false</p>
      <p>Env File Required: false</p>
      <p>Key Vault Client Configured: false</p>
      <p>Azure SDK For Secrets Configured: false</p>
      <p>Secret Values Exposed In Logs: false</p>
      <p>Common DB Dry-Run Approval Granted: false</p>
      <p>Portal Auth Dry-Run Approval Granted: false</p>
      <p>Real Activation Approval Granted: false</p>
      <p>Secret Provider Safe Mock Warning: Secret Provider safe mock only; no real secrets are read</p>
      <p>Next Gate: Sprint6P3CommonDbConnectivityDryRunContract</p>
      <p>Sprint 6 P3 Common DB Connectivity Dry-Run Contract: Exists</p>
      <p>Common DB Connectivity Dry-Run Contract Exists: true</p>
      <p>Common DB Dry-Run Approval Granted: false</p>
      <p>Common DB Dry-Run Enabled: false</p>
      <p>Common DB Connection Attempted: false</p>
      <p>Uses Secret Provider Safe Mock Metadata: true</p>
      <p>Uses Synthetic Connection Reference: true</p>
      <p>Synthetic Connection Reference: mock://crm/common-db</p>
      <p>Real Connection String Used: false</p>
      <p>Connection String Resolved: false</p>
      <p>SqlConnection Created: false</p>
      <p>DbConnection Created: false</p>
      <p>EF Runtime Enabled: false</p>
      <p>Migrations Created: false</p>
      <p>API Requires Database: false</p>
      <p>Common DB Connectivity Dry-Run Warning: Common DB connectivity dry-run contract only; no database connection is attempted</p>
      <p>Next Gate: Sprint6P4PortalAuthTokenPropagationDryRunContract</p>
      <p>Sprint 6 P4 Portal Auth Token Propagation Dry-Run Contract: Exists</p>
      <p>Portal Auth Token Propagation Dry-Run Contract Exists: true</p>
      <p>Portal Auth Dry-Run Approval Granted: false</p>
      <p>Portal Auth Dry-Run Enabled: false</p>
      <p>Portal Auth Runtime Connected: false</p>
      <p>Token Read Attempted: false</p>
      <p>Header Read Attempted: false</p>
      <p>Portal HTTP Attempted: false</p>
      <p>Uses Synthetic Token Metadata: true</p>
      <p>Synthetic Token Reference: mock://crm/portal-auth-token</p>
      <p>Synthetic User Reference: mock://crm/portal-user</p>
      <p>Real Token Used: false</p>
      <p>Real Headers Read: false</p>
      <p>Login Implemented By CRM: false</p>
      <p>Identity Implemented By CRM: false</p>
      <p>Permissions Persisted In CRM: false</p>
      <p>Productive Authorization Enabled: false</p>
      <p>Portal Auth Token Propagation Dry-Run Warning: Portal Auth token propagation dry-run contract only; no real tokens or headers are read</p>
      <p>Next Gate: Sprint6P5LockedStubRuntimeRegistrationTrial</p>
      <p>Sprint 6 P5 Locked Stub Runtime Registration Trial: Exists</p>
      <p>Locked Stub Runtime Registration Trial Exists: true</p>
      <p>Locked Stub Runtime Registration Approval Granted: false</p>
      <p>Locked Stub Runtime Registration Enabled: false</p>
      <p>Locked Stubs Registered At Runtime: false</p>
      <p>Productive Routes Registered: false</p>
      <p>Productive CRUD Enabled: false</p>
      <p>DELETE Endpoints Enabled: false</p>
      <p>Default Negative Route Status: 404</p>
      <p>Future Locked Response Status If Explicitly Enabled: 423</p>
      <p>Runtime Flag Default Enabled: false</p>
      <p>Uses Domain Services: false</p>
      <p>Uses Foundation Stores: false</p>
      <p>Uses Database: false</p>
      <p>Uses Portal Auth: false</p>
      <p>Uses Token Or Header Reads: false</p>
      <p>Runtime Registration Decision: DocumentOnlyPreferredWithNoRuntimeRegistration</p>
      <p>Locked Stub Runtime Registration Trial Warning: Locked stub runtime registration trial only; no productive routes are registered by default</p>
      <p>Next Gate: Sprint6P6Sprint6GateDecision</p>
      <p>Sprint 6: Closed</p>
      <p>Sprint 6 Gate Decision: Completed</p>
      <p>Overall Decision: GoForSprint7ControlledNonProductionActivationPlanning</p>
      <p>Real Activation Decision: NoGo</p>
      <p>Secret Provider Real Runtime: NoGo</p>
      <p>Common DB Real Connection: NoGo</p>
      <p>Portal Auth Real Runtime: NoGo</p>
      <p>Locked Stub Runtime Registration: NoGo</p>
      <p>Productive Routes: NoGo</p>
      <p>Productive CRUD: NoGo</p>
      <p>DELETE: NoGo</p>
      <p>Productive UI: NoGo</p>
      <p>Productization Status: NotReady</p>
      <p>Sprint 7 Planning: Go</p>
      <p>Sprint 6 Gate Decision Warning: Sprint 6 gate decision only; no real activation</p>
      <p>Next Gate: Sprint7P1SecretProviderRealNonProductionApproval</p>
      <p>Sprint 7 P1 Secret Provider Real NonProduction Approval: Exists</p>
      <p>Secret Provider Real NonProduction Approval Granted: false</p>
      <p>Secret Provider Real Runtime Enabled: false</p>
      <p>Secret Provider Real Runtime Connected: false</p>
      <p>Real Secret Read Attempted: false</p>
      <p>Key Vault Runtime Client Enabled: false</p>
      <p>Azure Secret SDK Runtime Enabled: false</p>
      <p>Env File Required: false</p>
      <p>Env Secret Read Allowed: false</p>
      <p>Secrets Logged: false</p>
      <p>Secret Names Approved: false</p>
      <p>Secret Values Approved: false</p>
      <p>Security Review Required: true</p>
      <p>Architecture Review Required: true</p>
      <p>DevOps Review Required: true</p>
      <p>Rollback Required: true</p>
      <p>Observability Required: true</p>
      <p>Secret Provider Real NonProduction Approval Warning: Secret Provider real NonProduction approval package only; no real secrets are read</p>
      <p>Next Gate: Sprint7P2SecretProviderRealNonProductionRuntimeProbe</p>
      <p>Sprint 7 P2 Secret Provider Real NonProduction Runtime Probe: Exists</p>
      <p>Secret Provider Real NonProduction Approval Granted: false</p>
      <p>Secret Provider Real Runtime Probe Enabled: false</p>
      <p>Secret Provider Real Runtime Probe Attempted: false</p>
      <p>Secret Provider Real Runtime Connected: false</p>
      <p>Real Secret Read Attempted: false</p>
      <p>Real Secret Value Materialized: false</p>
      <p>Real Secret Value Logged: false</p>
      <p>Secret Value Returned To API: false</p>
      <p>Key Vault Runtime Client Created: false</p>
      <p>Key Vault Runtime Call Attempted: false</p>
      <p>Azure Secret SDK Runtime Enabled: false</p>
      <p>Env Secret Read Attempted: false</p>
      <p>Env File Required: false</p>
      <p>Logical Secret Names Validated: true</p>
      <p>Secret Values Validated: false</p>
      <p>Probe Skipped Because Approval Not Granted: true</p>
      <p>Secret Provider Real NonProduction Runtime Probe Warning: Secret Provider real NonProduction runtime probe is prepared but skipped because approval is not granted</p>
      <p>Next Gate: Sprint7P3CommonDbRealConnectivityNonProductionProbe</p>
      <p>Sprint 7 P3 Common DB Real Connectivity NonProduction Probe: Exists</p>
      <p>Common DB Real Connectivity Approval Granted: false</p>
      <p>Secret Provider Real NonProduction Approval Granted: false</p>
      <p>Secret Provider Real Runtime Probe Enabled: false</p>
      <p>Connection String Resolved: false</p>
      <p>Connection String Value Materialized: false</p>
      <p>Connection String Logged: false</p>
      <p>Connection String Returned To API: false</p>
      <p>Common DB Probe Enabled: false</p>
      <p>Common DB Probe Attempted: false</p>
      <p>Common DB Connected: false</p>
      <p>SqlConnection Created: false</p>
      <p>DbConnection Created: false</p>
      <p>UseSqlServer Enabled: false</p>
      <p>EF Runtime Enabled: false</p>
      <p>AddDbContext Runtime Enabled: false</p>
      <p>Migrations Created: false</p>
      <p>Database Schema Changed: false</p>
      <p>Productive Persistence Enabled: false</p>
      <p>API Requires Database: false</p>
      <p>Uses Secret Provider Runtime: false</p>
      <p>Uses Synthetic Fallback: true</p>
      <p>Synthetic Connection Reference: mock://crm/common-db</p>
      <p>Connection Probe Skipped Because Secret Provider Approval Not Granted: true</p>
      <p>Common DB Real Connectivity NonProduction Probe Warning: Common DB real connectivity NonProduction probe is prepared but skipped because Secret Provider approval is not granted</p>
      <p>Next Gate: Sprint7P4PortalAuthRealRuntimeProbe</p>
      <p>Sprint 7 P4 Portal Auth Real Runtime Probe: Exists</p>
      <p>Portal Auth Real Runtime Approval Granted: false</p>
      <p>Secret Provider Real NonProduction Approval Granted: false</p>
      <p>Portal Auth Real Runtime Probe Enabled: false</p>
      <p>Portal Auth Real Runtime Probe Attempted: false</p>
      <p>Portal Auth Runtime Connected: false</p>
      <p>Portal Auth Base URL Resolved: false</p>
      <p>Portal Auth Base URL Materialized: false</p>
      <p>Portal Auth Base URL Logged: false</p>
      <p>Portal Auth Base URL Returned To API: false</p>
      <p>Portal HTTP Client Created: false</p>
      <p>Portal HTTP Call Attempted: false</p>
      <p>Portal Auth Token Validation Attempted: false</p>
      <p>Token Read Attempted: false</p>
      <p>Header Read Attempted: false</p>
      <p>Authorization Header Read Attempted: false</p>
      <p>Real Token Materialized: false</p>
      <p>Real Token Logged: false</p>
      <p>Token Returned To API: false</p>
      <p>Login Implemented By CRM: false</p>
      <p>Logout Implemented By CRM: false</p>
      <p>Identity Implemented By CRM: false</p>
      <p>Roles Persisted In CRM: false</p>
      <p>Permissions Persisted In CRM: false</p>
      <p>Productive Authorization Enabled: false</p>
      <p>API Requires Portal Auth: false</p>
      <p>Uses Synthetic Fallback: true</p>
      <p>Synthetic Portal Auth Reference: mock://crm/portal-auth</p>
      <p>Synthetic User Reference: mock://crm/portal-user</p>
      <p>Probe Skipped Because Portal Auth Approval Not Granted: true</p>
      <p>Portal Auth Real Runtime Probe Warning: Portal Auth real runtime probe is prepared but skipped because Portal Auth approval is not granted</p>
      <p>Next Gate: Sprint7P5LockedProductiveRouteRuntimeRegistrationWith423</p>
      <p>Sprint 7 P5 Locked Productive Route Runtime Registration With 423: Exists</p>
      <p>Locked Productive Route Runtime Registration Approval Granted: false</p>
      <p>Locked Productive Route Runtime Registration Enabled: false</p>
      <p>Productive Routes Registered By Default: false</p>
      <p>Productive Routes Registered When Explicitly Enabled: true</p>
      <p>Default Negative Route Status: 404</p>
      <p>Explicitly Enabled Locked Route Status: 423</p>
      <p>Productive CRUD Enabled: false</p>
      <p>Productive Domain Execution Enabled: false</p>
      <p>Productive Persistence Enabled: false</p>
      <p>Delete Endpoints Enabled: false</p>
      <p>Portal Auth Runtime Required: false</p>
      <p>Portal Auth Runtime Enabled: false</p>
      <p>Token Read Attempted: false</p>
      <p>Header Read Attempted: false</p>
      <p>DB Runtime Enabled: false</p>
      <p>EF Runtime Enabled: false</p>
      <p>Migrations Created: false</p>
      <p>Side Effects Allowed: false</p>
      <p>Locked Productive Route Runtime Registration Warning: Locked productive routes are not registered by default; explicit NonProduction flag returns 423 without side effects</p>
      <p>Next Gate: Sprint7P6Sprint7GateDecision</p>
      <p>Sprint 7: Closed</p>
      <p>Sprint 7 Gate Decision: Completed</p>
      <p>Overall Decision: GoForSprint8ControlledRuntimeApprovalAndPilotPlanning</p>
      <p>Real Activation Decision: NoGo</p>
      <p>Secret Provider Real Runtime: NoGo</p>
      <p>Common DB Real Connection: NoGo</p>
      <p>Portal Auth Real Runtime: NoGo</p>
      <p>Locked Productive Route Registration: GoOnlyAsExplicitNonProductionLocked423</p>
      <p>Productive Routes Default: NoGo</p>
      <p>Productive CRUD: NoGo</p>
      <p>DELETE: NoGo</p>
      <p>Productive UI: NoGo</p>
      <p>Productization Status: NotReady</p>
      <p>Sprint 8 Planning: Go</p>
      <p>Sprint 7 Gate Decision Warning: Sprint 7 gate decision only; no real activation</p>
      <p>Next Gate: Sprint8P1SecretProviderApprovalDecision</p>
      <p>Sprint 8 P1 Secret Provider Approval Decision: Exists</p>
      <p>Secret Provider Approval Decision: ApprovedForControlledNonProductionReadPlanning</p>
      <p>Secret Provider Real Read Approved For Next Sprint: true</p>
      <p>Secret Provider Real Read Enabled Now: false</p>
      <p>Real Secret Read Attempted: false</p>
      <p>Real Secret Value Materialized: false</p>
      <p>Real Secret Value Logged: false</p>
      <p>Secret Value Returned To API: false</p>
      <p>Key Vault Runtime Client Created: false</p>
      <p>Key Vault Runtime Call Attempted: false</p>
      <p>Azure Secret SDK Runtime Enabled: false</p>
      <p>Env File Required: false</p>
      <p>Env Secret Read Allowed: false</p>
      <p>Approved Secret Names Only: true</p>
      <p>Approved Secret Values: false</p>
      <p>Approved For NonProduction Only: true</p>
      <p>Security Approval Recorded: true</p>
      <p>Architecture Approval Recorded: true</p>
      <p>DevOps Approval Recorded: true</p>
      <p>Rollback Plan Approved: true</p>
      <p>Observability Plan Approved: true</p>
      <p>Redaction Plan Approved: true</p>
      <p>Secret Provider Approval Decision Warning: Secret Provider approval decision only; no real secret read in Sprint 8 P1</p>
      <p>Next Gate: Sprint8P2SecretProviderControlledRealNonProductionRead</p>
      <p>Sprint 8 P2 Secret Provider Controlled Real NonProduction Read: Exists</p>
      <p>Secret Provider Controlled Real NonProduction Read Approved: true</p>
      <p>Secret Provider Controlled Real NonProduction Read Enabled: false</p>
      <p>Secret Provider Controlled Real NonProduction Read Attempted: false</p>
      <p>Real Secret Read Attempted: false</p>
      <p>Real Secret Value Materialized: false</p>
      <p>Real Secret Value Logged: false</p>
      <p>Secret Value Returned To API: false</p>
      <p>Secret Value Persisted: false</p>
      <p>Secret Value Cached: false</p>
      <p>Key Vault Runtime Client Created: false</p>
      <p>Key Vault Runtime Call Attempted: false</p>
      <p>Azure Secret SDK Runtime Enabled: false</p>
      <p>Uses Approved Secret Names Only: true</p>
      <p>NonProduction Only: true</p>
      <p>Fail Closed By Default: true</p>
      <p>Next Gate: Sprint8P3CommonDbControlledRealConnectivity</p>
      <p>Sprint 8 P3 Common DB Controlled Real Connectivity: Exists</p>
      <p>Common DB Controlled Real Connectivity Approved: true</p>
      <p>Common DB Controlled Real Connectivity Enabled: false</p>
      <p>Common DB Connectivity Attempted: false</p>
      <p>Common DB Connected: false</p>
      <p>Secret Provider Availability Metadata Used: true</p>
      <p>Secret Value Returned To API: false</p>
      <p>Connection String Resolved: false</p>
      <p>Connection String Materialized In Public Contract: false</p>
      <p>Connection String Logged: false</p>
      <p>Connection String Returned To API: false</p>
      <p>SqlConnection Created: false</p>
      <p>DbConnection Created: false</p>
      <p>DbConnection Opened: false</p>
      <p>EF Runtime Enabled: false</p>
      <p>AddDbContext Runtime Enabled: false</p>
      <p>UseSqlServer Enabled: false</p>
      <p>Migrations Created: false</p>
      <p>Database Schema Changed: false</p>
      <p>Productive Persistence Enabled: false</p>
      <p>Productive CRUD Enabled: false</p>
      <p>API Requires Database: false</p>
      <p>NonProduction Only: true</p>
      <p>Fail Closed By Default: true</p>
      <p>Next Gate: Sprint8P4PortalAuthControlledRealRuntimeValidation</p>
      <p>Sprint 8 P4 Portal Auth Controlled Real Runtime Validation: Exists</p>
      <p>Portal Auth Controlled Real Runtime Validation Approved: true</p>
      <p>Portal Auth Controlled Real Runtime Validation Enabled: false</p>
      <p>Portal Auth Runtime Validation Attempted: false</p>
      <p>Portal Auth Runtime Connected: false</p>
      <p>Secret Provider Availability Metadata Used: true</p>
      <p>Portal Auth Base URL Resolved: false</p>
      <p>Portal Auth Base URL Materialized In Public Contract: false</p>
      <p>Portal Auth Base URL Logged: false</p>
      <p>Portal Auth Base URL Returned To API: false</p>
      <p>Portal HTTP Client Created: false</p>
      <p>Portal HTTP Call Attempted: false</p>
      <p>Token Read Attempted: false</p>
      <p>Header Read Attempted: false</p>
      <p>Authorization Header Read Attempted: false</p>
      <p>Real Token Materialized: false</p>
      <p>Real Token Logged: false</p>
      <p>Token Returned To API: false</p>
      <p>Login Implemented By CRM: false</p>
      <p>Logout Implemented By CRM: false</p>
      <p>Identity Implemented By CRM: false</p>
      <p>Roles Persisted In CRM: false</p>
      <p>Permissions Persisted In CRM: false</p>
      <p>Productive Authorization Enabled: false</p>
      <p>API Requires Portal Auth: false</p>
      <p>NonProduction Only: true</p>
      <p>Fail Closed By Default: true</p>
      <p>Next Gate: Sprint8P5LockedRouteAuthorizationPolicyIntegration</p>
      <p>Sprint 8 P5 Locked Route Authorization Policy Integration: Exists</p>
      <p>Locked Route Authorization Policy Integration Approved: true</p>
      <p>Locked Route Authorization Policy Integration Enabled: false</p>
      <p>Authorization Policy Evaluated: false</p>
      <p>Authorization Policy Decision: NotEvaluatedBecauseDisabled</p>
      <p>Portal Auth Metadata Used: true</p>
      <p>Portal Auth Runtime Required: false</p>
      <p>Portal Auth Runtime Connected: false</p>
      <p>Token Read Attempted: false</p>
      <p>Header Read Attempted: false</p>
      <p>Authorization Header Read Attempted: false</p>
      <p>Portal HTTP Call Attempted: false</p>
      <p>Productive Routes Registered By Default: false</p>
      <p>Default Negative Route Status: 404</p>
      <p>Locked Routes Enabled Only With Explicit NonProduction Flag: true</p>
      <p>Locked Route Status: 423</p>
      <p>Locked Route Authorization Decision Returned: false</p>
      <p>Productive CRUD Enabled: false</p>
      <p>Productive Domain Execution Enabled: false</p>
      <p>Productive Persistence Enabled: false</p>
      <p>Delete Endpoints Enabled: false</p>
      <p>Side Effects Allowed: false</p>
      <p>DB Runtime Enabled: false</p>
      <p>EF Runtime Enabled: false</p>
      <p>NonProduction Only: true</p>
      <p>Fail Closed By Default: true</p>
      <p>Next Gate: Sprint8P6Sprint8GateDecision</p>
      <p>Sprint 8: Closed</p>
      <p>Sprint 8 Gate Decision: Completed</p>
      <p>Overall Decision: GoForSprint9ControlledRuntimeActivationPlanning</p>
      <p>Real Production Activation Decision: NoGo</p>
      <p>Secret Provider Controlled Read: GoOnlyAsExplicitNonProductionFlag</p>
      <p>Common DB Controlled Connectivity: GoOnlyAsExplicitNonProductionFlag</p>
      <p>Portal Auth Controlled Validation: GoOnlyAsExplicitNonProductionFlag</p>
      <p>Locked Route Authorization Policy: GoOnlyAsExplicitNonProductionLocked423</p>
      <p>Productive Routes Default: NoGo</p>
      <p>Productive CRUD: NoGo</p>
      <p>DELETE: NoGo</p>
      <p>Productive UI: NoGo</p>
      <p>Productization Status: NotReady</p>
      <p>Sprint 9 Planning: Go</p>
      <p>Next Gate: Sprint9P1ControlledRuntimeActivationDecision</p>
      <p>Non-Production</p>
    </section>
  `
})
class HomeComponent {
}

@Component({
  standalone: true,
  selector: 'crm-readiness',
  template: `
    <section class="card">
      <h1>CRM Readiness</h1>
      <pre>{{ readiness() | json }}</pre>
    </section>
  `,
  imports: [JsonPipe]
})
class ReadinessComponent {
  readonly readiness = signal(this.readinessService.getReadiness());

  constructor(private readonly readinessService: CrmReadinessService) {
  }
}

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'readiness', component: ReadinessComponent }
];

@Component({
  standalone: true,
  selector: 'crm-root',
  template: '<main><router-outlet /></main>',
  imports: [RouterOutlet]
})
class AppComponent {
}

bootstrapApplication(AppComponent, {
  providers: [provideRouter(routes)]
}).catch(error => console.error(error));
