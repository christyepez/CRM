# CRM Controlled Runtime Pilot First Slice NonProduction Activation Dry Run Risk Register

| Risk | Dry run result | Mitigation |
| --- | --- | --- |
| Dry run mistaken for activation | Not activated | Keep `NonProductionActivationExecuted: false` |
| Flag drift | All false | Guardrail checks false flags |
| Unsafe configuration | Placeholder-only | Scan for URLs, secrets, tokens and certificates |

## Markers

- FirstSliceNonProductionActivationDryRunEvidencePrepared: true.
- FirstSliceNonProductionActivationDryRunRollbackPrepared: true.
