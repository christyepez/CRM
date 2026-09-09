# Riesgos

| Riesgo | Impacto | Mitigacion |
|---|---|---|
| Portal API incompleta | Alto | Usar contratos y stubs temporales. |
| Duplicacion de capacidades del portal | Alto | Revisar documentos de arquitectura. |
| Acoplamiento con proveedores externos | Alto | Usar Integration Hub. |
| Configuracion visual fija | Medio | Usar componentes dinamicos del portal. |
| Configuracion sensible en repo | Alto | Usar archivos de ejemplo y proveedor seguro. |
| Activacion accidental de Activity productivo durante validacion local | Alto | Verificador S13-06 valida ausencia de rutas productivas y DELETE. |
| Persistencia durable accidental en Activity foundation | Alto | S13-06 conserva store in-memory y Common DB/EF/migraciones deshabilitados. |
| Activity foundation permanece in-memory y sintetico tras cierre S13 | Medio | Registrar cierre foundation-only y mantener Common DB runtime separado hasta aprobacion futura. |
| Opportunity Pipeline podria intentar activar Account, assignment o Lead conversion prematuramente | Alto | Sprint 14 P1 debe iniciar como baseline/backlog y conservar esas dependencias diferidas. |
| PipelineStage solo existe como concepto/catalogo y puede generar reglas inconsistentes | Medio | S14-01 debe definir orden, unicidad y transiciones antes de servicios, API o UI. |
| Catalogo sintetico de stages puede divergir del dominio si S14-05 no endurece paridad | Medio | S14-04 usa IDs/orden/nombres deterministas y S14-05 debe agregar guardrails cross-layer. |
| UI foundation podria presentar acciones terminales repetidas | Medio | S14-04 oculta acciones para Won/Lost/Cancelled y deshabilita edicion; S14-05 debe cubrirlo. |
| Opportunity Pipeline permanece FoundationOnly e in-memory tras S14-06 | Medio | Cerrar Sprint 14 como foundation-only en S14-07 y mantener Common DB/productive activation como gates separados. |
| Validacion local S14-06 podria confundirse con disponibilidad productiva | Alto | Verificador S14-06 y docs preservan productive route false, DELETE false, Portal/CommonDB false y `crm-prod-sim` untouched. |
| Campaign conceptual puede crecer sin reglas claras de lifecycle/fechas | Medio | Sprint 15 P1 debe fijar baseline y S15-01 reglas deterministas antes de servicio/API/UI. |
