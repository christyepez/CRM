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
