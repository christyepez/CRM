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
