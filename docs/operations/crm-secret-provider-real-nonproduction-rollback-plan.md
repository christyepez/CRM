# CRM Secret Provider Real NonProduction Rollback Plan

Rollback for P1 is documentation and flag rollback only because no runtime activation exists.

If P2 is not approved:

- Keep approvalGranted=false.
- Keep runtimeEnabled=false.
- Keep runtimeConnected=false.
- Keep realSecretReadAttempted=false.
- Keep DB/Auth/Portal/productive routes disabled.

If a future probe is enabled by mistake, disable the probe flag, remove runtime registration and verify no secret values were logged.
