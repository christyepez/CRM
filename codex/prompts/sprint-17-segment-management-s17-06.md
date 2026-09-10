# CRM Sprint 17 S17-06 - Segment Local Integration Validation

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 17 S17-05 merge commit required
Branch: crm-sprint-17-s17-06-segment-local-integration-validation
Suggested commit: test(crm): validate segment management local integration
PR title: CRM Sprint 17 S17-06 - Segment Local Integration Validation

## Objective
Validate the full foundation-only Segment workflow through Angular 4200 -> proxy -> API 8093 -> Application -> Domain -> InMemorySegmentFoundationStore.

## Required scenarios
Health/live/ready; frontend route `/foundation/segments`; list/detail/create/update/no-change; invalid request -> 400; missing -> 404; Draft deactivate -> 409; activate/deactivate and repeated idempotent lifecycle; read-after-write; productive `/api/crm/segments` unavailable; Segment DELETE unavailable.

## Safety
Use synthetic data only. Do not touch port 8094 or `crm-prod-sim`. No criteria execution, Campaign targeting, Account auto-classification, Portal runtime, Common DB, connectors, real data or real Production.

## Validation
Capture runtime evidence and latency, clean up only owned 8093/4200 processes, run full .NET/Angular/Foundation/Sprint 17 regression, then prepare S17-07 closure.