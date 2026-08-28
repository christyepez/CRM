# CRM Sprint 10 P50 - Security Postcheck

SecurityPostcheckExists: true
SecurityPostExecutionValidation: PASS

NonRootRuntime: true
ContainerUser: 65532:65532
LoopbackBindingPreserved: true
PublishedPort: 127.0.0.1:8094
UnexpectedPortExposureDetected: false

SecretsIntroduced: false
CertificatesOrPrivateKeysIntroduced: false
RealProductionCredentialsDetected: false

SqlServerInSimulatedProductionScope: false
PortalIncluded: false
PortalRuntimeCallsDetected: false
CommonDbIncluded: false
CommonDbRuntimeCallsDetected: false
ProductionDataWritePathDetected: false
UnexpectedExternalDependencyDetected: false
