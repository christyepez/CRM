# CRM Sprint 4 Recommended Path

## Sprint 4 P2 result

P2 establishes `CommonDbRuntimeProbe` behind a disabled flag. The API exposes only status metadata, does not attempt a database connection and keeps CRM without SQL Server ownership. The next gate is `Sprint4P3PortalAuthRuntimeProbeBehindDisabledFlag`.

## Sprint 4 P1 result

P1 establishes `RuntimeEnvironmentReadiness` and local tooling hardening. The next gate is `Sprint4P2ControlledCommonDbRuntimeProbeBehindDisabledFlag`; real activation remains blocked.

Recommended path: conservative runtime gate preparation.

Start with `Sprint4P1RuntimeEnvironmentReadinessAndLocalToolingHardening`.

Then proceed only through disabled-flag probes for common DB, Portal Auth and locked productive routes. No production activation should happen before a Sprint 4 gate decision.
