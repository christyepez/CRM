# CRM Sprint 21 - Pipeline Catalog Functional Baseline

S21P1Decision: PipelineCatalogFoundationReadOnly
SelectedSliceId: S21-PIPELINE-CATALOG
SelectedSliceName: Pipeline Catalog Foundation

## Repository evidence
- `Pipeline`: CRM-owned conceptual entity with `Id`, `Name` and `ReadContract`.
- `PipelineStage`: CRM-owned conceptual entity with `Id`, `Name`, `Order` and `ReadContract`.
- A Pipeline contains ordered PipelineStages.
- Sprint 14 delivered Opportunity Pipeline behavior using deterministic synthetic pipeline data.
- Sprint 14 residual explicitly defers Portal Catalog integration until separate authorization.

## P1 contract
- Pipeline catalog is deterministic synthetic foundation data only.
- Pipeline Id must be a non-empty GUID.
- Pipeline Name is required, trimmed, max 120 characters.
- Each Pipeline contains one or more stages.
- Stage Id must be a non-empty GUID.
- Stage Name is required, trimmed, max 120 characters.
- Stage Order must be positive and unique within its Pipeline.
- Stage ordering is ascending by Order.
- Catalog is read-only: list/detail only.

## Explicit exclusions
- No create/update/delete/archive/activate operations.
- No Opportunity mutation from this slice.
- No Portal Catalog adapter/runtime.
- No productive Pipeline routes.
- No Common DB/EF/SQL, real data or external connectors.
## Sprint 21 backlog
- S21-01 Pipeline Catalog contracts and deterministic domain validation.
- S21-02 Application read service and typed synthetic catalog provider.
- S21-03 Foundation read-only API: GET list/detail only.
- S21-04 Angular read-only foundation page.
- S21-05 Test and guardrail hardening.
- S21-06 Local HTTP integration validation.
- S21-07 Sprint closure and next bounded capability selection.

## Safety markers
ProductivePipelineRouteEnabled: false
PipelineMutationEnabled: false
DeleteBehaviorAdded: false
OpportunityMutationEnabled: false
PortalCatalogRuntimeEnabled: false
CommonDbRuntimeEnabled: false
RealDataEnabled: false
ExternalConnectorsEnabled: false
SimulatedProductionTouched: false
Port8094Touched: false