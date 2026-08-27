# CRM OPS-04 Security Evidence

EnvironmentClassification: SimulatedProduction
RealProduction: false
LocalSimulation: true

NoProductionCredentials: true
NoSecretsCommitted: true
NoCertificatesCommitted: true
NoConnectionStringsCommitted: true
NoSqlServerService: true
PortalIncluded: false
CommonDbIncluded: false
ProductionDataChangesApproved: false
ApprovedProductionExternalDependencies: none

ContainerUser: 65532:65532
NonRootRuntime: true
PortBinding: 127.0.0.1:8094

Note: `/api/crm/readiness` currently reports `portalIntegration` and `financialIntegration` from the existing application contract. OPS-04 does not modify runtime code; the compose target still supplies disabled integration environment values and includes no Portal, Common DB, or SQL Server service.
