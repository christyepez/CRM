# CRM Controlled Runtime Preflight Validations

## Future preflight checks

- Verify CRM branch and base commit.
- Verify Portal Sprint 21 handoff documentation.
- Verify no real Portal private URLs or secrets are present.
- Verify CRM compose has no Portal services.
- Verify Common DB runtime remains disabled until explicit approval.
- Verify productive Portal navigation and Gateway routes are disabled.
- Verify health/smoke probes use synthetic data only.

## Markers

- ControlledRuntimePreflightValidationsPrepared: true.
- RealPortalPrivateUrlsPresent: false.
- SecretsPresent: false.
- EnvRealFileCommitted: false.
- RealDataPresent: false.
