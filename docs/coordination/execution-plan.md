# Plan de Ejecucion

1. Gobierno y estructura.
2. Integracion con PortalCorporativo.
3. Backend CRM Core.
4. Base de datos CRM.
5. Frontend CRM.
6. CRM Integration Hub.
7. Workers.
8. Docker.
9. QA y documentacion.

## Sprint 18 S18-02 execution

1. Leer AGENTS, instrucciones CRM, prompt S18-02 y playbook backend comun.
2. Implementar Case Application/contracts/store foundation-only sobre `CaseManagementPolicy`.
3. Agregar pruebas unitarias y arquitectura para servicio/store y ausencia de rutas.
4. Agregar docs, verifier S18-02, prompt S18-03 y next-task.
5. Ejecutar focused verifiers, Release build/test, Angular build/test, foundation verifier y guardrails.
6. Registrar evidencia real y preparar commit local sin push.

## Sprint 18 S18-03 execution

1. Leer AGENTS, instrucciones CRM, prompt S18-03 y playbook backend comun.
2. Exponer Case foundation API solo bajo `/api/crm/foundation/cases`.
3. Usar DTOs explicitos y mapear resultados de `ICaseManagementService` a 200/400/404/409.
4. Agregar pruebas de endpoint y arquitectura para rutas foundation, idempotencia y guardrails.
5. Agregar docs, verifier S18-03, prompt S18-04 y next-task.
6. Ejecutar Release build/test, Angular build/test, foundation verifier, guardrails y verificadores Sprint 18.
