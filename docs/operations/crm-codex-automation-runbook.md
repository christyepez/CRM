# CRM Codex Automation Runbook

## Actualizar `codex/next-task.md`

1. Sincronizar `main` desde GitHub.
2. Crear una rama nueva para la preparación de la tarea.
3. Completar `codex/next-task.md` con repository, phase, base main commit, branch esperada, commit sugerido, PR title, objetivo, guardrails, validaciones y cierre esperado.
4. No incluir secretos, tokens, certificados, `.env` ni datos reales.
5. Abrir PR hacia `main` y esperar revisión.

## Creación automática del Issue

Cuando un cambio en `codex/next-task.md` llega a `main`, el workflow `.github/workflows/create-codex-task-issue.yml` crea un GitHub Issue con labels:

- `codex-task`
- `crm-sprint`
- `ready-for-codex`

El Issue contiene el contenido de `codex/next-task.md` como prompt de tarea.

## Lectura de `codex/current-task.md`

Codex debe leer `codex/current-task.md` antes de ejecutar una tarea vigente. Si el archivo no fue actualizado con la tarea vigente, Codex no debe ejecutar la tarea.

## Abrir PR

Cada ejecución debe:

- Usar `main` actualizado como base.
- Crear rama nueva.
- Hacer commit con alcance claro.
- Hacer push de la rama.
- Crear Pull Request hacia `main`.
- No hacer merge automático.

## Mantener guardrails

- No duplicar capacidades del Portal.
- No subir secretos, tokens, certificados ni `.env`.
- No activar runtime productivo sin aprobación explícita.
- No agregar servicios Docker si la tarea no lo autoriza.
- No tocar API, frontend, tests runtime o `Program.cs` salvo autorización explícita.

## Volver al flujo manual

Si falla la automatización:

1. Crear el GitHub Issue manualmente usando `.github/ISSUE_TEMPLATE/codex-task.yml`.
2. Copiar el contenido vigente de `codex/next-task.md`.
3. Aplicar los labels `codex-task`, `crm-sprint` y `ready-for-codex`.
4. Continuar con rama, commit y PR manuales.
