# CRM Corporativo

Repositorio del modulo CRM corporativo integrado al Portal Corporativo.

## Portal base

El CRM debe integrarse con:

```text
https://github.com/christyepez/PortalCorporativo
```

## Objetivo

Implementar un CRM modular, parametrizable y extensible, reutilizando los componentes transversales del portal.

## Componentes del portal que se deben reutilizar

- Seguridad
- Usuarios
- Roles
- Permisos
- Menu dinamico
- Temas
- Configuracion
- Catalogos
- Auditoria
- Notificaciones
- Documentos
- Reporteria

## Modulos CRM

- Clientes
- Contactos
- Leads
- Oportunidades
- Actividades
- Casos
- Campanas
- CRM Integration Hub
- Workers CRM

## Lectura obligatoria para Codex

Codex debe leer en este orden:

1. `codex/COORDINADOR_SOLUCION.md`
2. `AGENTS.md`
3. `codex/INSTRUCTIONS.md`
4. `codex/ARCHITECTURE_RULES.md`
5. `codex/PORTAL_INTEGRATION_CONTRACTS.md`
6. `codex/TASKS.md`
7. `codex/TASK_TEMPLATE.md`

## Regla principal

El CRM es un modulo funcional. El Portal Corporativo es la plataforma transversal. El CRM consume servicios del portal y no los reconstruye.
