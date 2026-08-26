# CRM Sprint 10 P47T - ArchitectureDependencyTests Test-Only Remediation

P47TArchitectureDependencyTestsTestOnlyRemediationExists: true
ArchitectureTestsFixApplied: true
ArchitectureTestsRuntimeBehaviorChanged: false

Remediation:

- Added deterministic source content caching by normalized root list inside `ArchitectureDependencyTests`.
- Added text/source file extension filtering.
- Added exclusions for `.git`, `.vs`, `.vscode`, `coverage` and `TestResults`.
- Preserved all existing assertions.
- Did not skip or ignore tests.
- Did not change CRM application runtime source.

