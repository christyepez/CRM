# CRM Runtime Probe Observability Plan

Observability is required before any future runtime probe activation.

Minimum signals:

- Health: `/health`, `/health/live`, `/health/ready`.
- Foundation status endpoint for the active probe.
- Negative route evidence for productive CRM paths.
- Structured logs without secrets, tokens, passwords, connection strings or personal data.

Sprint 5 P1 does not add telemetry runtime. It documents the requirements for P2/P3/P4/P5 evidence.
