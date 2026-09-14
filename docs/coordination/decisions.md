# Decisiones Arquitectonicas

| ID | Decision | Estado |
|---|---|---|
| ADR-001 | CRM sera modulo integrado al PortalCorporativo. | Aprobada |
| ADR-002 | CRM reutilizara servicios transversales del portal. | Aprobada |
| ADR-003 | Backend .NET alineado al portal. | Aprobada |
| ADR-004 | Frontend Angular alineado al portal. | Aprobada |
| ADR-005 | SQL Outbox e Inbox inicial. | Aprobada |
| ADR-006 | Salesforce y Dynamics mediante Integration Hub. | Aprobada |
| ADR-007 | Activity / Follow-Up S13-06 se valida solo en stack foundation-only con datos sinteticos. | Aprobada |
| ADR-008 | Productive Activity API, DELETE, Portal Auth runtime y Common DB runtime permanecen deshabilitados para S13-06. | Aprobada |
| ADR-009 | Sprint 13 Activity / Follow-Up se cierra como foundation-only y Sprint 14 continua con Opportunity Pipeline Foundation. | Aprobada |
| ADR-010 | Sprint 14 P1 selecciona S14-01 Opportunity Pipeline Contracts and Domain Rules como primer story; API, UI, store, Lead conversion, Account activation, assignment, Portal Auth y Common DB permanecen diferidos. | Aprobada |
| ADR-011 | S14-04 implementa Opportunity Pipeline UI foundation-only con catalogo sintetico deterministico y sin rutas productivas, DELETE, Portal Auth, Common DB, Lead conversion, Account runtime ni assignment. | Aprobada |
| ADR-012 | S14-06 valida Opportunity Pipeline localmente solo en foundation, con datos sinteticos y sin tocar rutas productivas, DELETE, Portal runtime, Common DB ni `crm-prod-sim`. | Aprobada |
| ADR-013 | Sprint 14 Opportunity Pipeline se cierra como foundation-only; Sprint 15 inicia Campaign Management Foundation por existir concepto Campaign y permitir valor comercial sin activar Productive/Portal/CommonDB. | Aprobada |
| ADR-014 | S18-02 implementa Case Management Application/store como CREATE de dominio CRM foundation-only; API, Angular, Productive, DELETE, Portal runtime, Common DB, conectores y Production permanecen diferidos. | Aprobada |
| ADR-015 | S18-03 expone Case Management solo mediante API foundation `/api/crm/foundation/cases` usando `ICaseManagementService`; rutas productivas, DELETE, Angular, Customer mutation, assignment, SLA, notifications, Portal runtime, Common DB y Production permanecen diferidos. | Aprobada |
| ADR-016 | S19-01 implementa Interaction Management como dominio puro con contratos y politica deterministica; Application/store/API/UI, rutas productivas, DELETE, Activity scheduling, mutacion cross-entity, Portal runtime, Common DB y Production permanecen diferidos. | Aprobada |
| ADR-017 | S19-02 implementa Interaction Management Application/store como CREATE de dominio CRM foundation-only; API, Angular, Productive, DELETE, Activity scheduling, cross-entity mutation, Portal runtime, Common DB, conectores y Production permanecen diferidos. | Aprobada |
| ADR-018 | S19-03 expone Interaction Management solo mediante API foundation `/api/crm/foundation/interactions` usando `IInteractionManagementService`; rutas productivas, DELETE, Angular, Activity scheduling, cross-entity mutation, Portal runtime, Common DB y Production permanecen diferidos. | Aprobada |
