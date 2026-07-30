# CRM Sprint 9 Gates

## Sprint 9 P1 gate

P1 approves only controlled NonProduction trial planning. No trial is enabled now.

- RuntimeTrialsEnabledNow: false.
- ProductionRuntimeEnabledNow: false.
- ExplicitNonProductionFlagsRequired: true.
- NextGate: Sprint9P2SecretProviderRuntimeEnablementTrial.

## Sprint 9 P2 gate

P2 adds the Secret Provider runtime enablement trial:

- SecretProviderRuntimeEnablementTrialExists: true.
- SecretProviderRuntimeEnablementTrialEnabled: false by default.
- AllowedLogicalSecretNamesEnforced: true.
- ObservabilityMetadataOnly: true.
- NextGate: Sprint9P3CommonDbRuntimeConnectivityTrial.

## Sprint 9 P3 gate

P3 adds the Common DB runtime connectivity trial:

- CommonDbRuntimeConnectivityTrialExists: true.
- CommonDbRuntimeConnectivityTrialEnabled: false by default.
- CommonDbConnectionAttempted: false by default.
- SecretProviderMetadataDependencyValidated: true.
- ObservabilityMetadataOnly: true.
- NextGate: Sprint9P4PortalAuthRuntimeValidationTrial.

## Sprint 9 P4 gate

P4 adds the Portal Auth runtime validation trial:

- PortalAuthRuntimeValidationTrialExists: true.
- PortalAuthRuntimeValidationTrialEnabled: false by default.
- PortalAuthValidationAttempted: false by default.
- AuthHeaderRead: false by default.
- TokenRead: false by default.
- SecretProviderMetadataDependencyValidated: true.
- CommonDbMetadataDependencyValidated: true.
- ObservabilityMetadataOnly: true.
- NextGate: Sprint9P5ProductiveRouteDryRunTrial.

## Sprint 9 P1

Gate: `Sprint9P1ControlledRuntimeActivationDecision`.

## Sprint 9 P2

Gate: `Sprint9P2SecretProviderRuntimeEnablementTrial`.

## Sprint 9 P3

Gate: `Sprint9P3CommonDbRuntimeConnectivityTrial`.

## Sprint 9 P4

Gate: `Sprint9P4PortalAuthRuntimeValidationTrial`.

## Sprint 9 P5

Gate: `Sprint9P5ProductiveRouteDryRunTrial`.

## Sprint 9 P6

Gate: `Sprint9P6Sprint9GateDecision`.

No Sprint 9 gate implies production activation until explicitly approved.
