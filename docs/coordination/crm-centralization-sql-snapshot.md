# CRM centralization — isolated migration slice

Owner approved SQL persistence and safe export/import on 2026-09-18. HIGH data-impact change. Current source CRM remains active and unchanged; existing dirty worktrees are not modified.

Responsible streams: CRM Database (Agent 03) and QA CRM (Agent 08), coordinated sequentially in this branch; no new external agent threads were created. Ownership: `tools/CRM.Runtime.Sql`, `tools/CRM.Sql.Acceptance`, migration tests and this coordination document. Acceptance criteria for this slice: all 13 typed ports compile, full field/lifecycle round-trips, tenant/store isolation, no automatic seeds or destructive clears, guarded isolated SQL acceptance, component coverage above the common baseline, and existing CRM regressions. Security-sensitive runtime activation remains deferred.

Classification: CREATE domain-specific migration use cases; REUSE existing central SQL infrastructure; ADAPT Portal authorization/audit for future runtime integration. No replacement login, permissions, audit engine, notifications or UI configuration is introduced.

## Scope delivered by this slice

The .NET 10 tool under `tools/` provides versioned JSON snapshot validation, canonical payload integrity, tenant-scoped EF Core SQL storage, transactional empty-target import, exact idempotent retries and no-overwrite SQL export. It has no HTTP endpoints and does not integrate into or replace the .NET 8 CRM runtime. It persists migration snapshots, NOT the live business stores.

Tests use synthetic data only. SQLite relational checks are not proof of SQL Server runtime compatibility. The SQL script must also be tested on the existing SQL Server in a newly created dedicated `CrmMigration_` database before accepting this slice.

## Typed SQL adapter slice

`tools/CRM.Runtime.Sql` implements the 13 existing foundation store ports against a separate `crm.FoundationRecords` table. The migration snapshot table is not repurposed as mutable business storage. Tenant/store matching uses binary SQL collation; record-ID lookup is case-insensitive, while updates must preserve the original ID casing. Records retain their complete typed contract fields, including preview metadata. Activity uses an explicit lifecycle codec for scheduled/completed/cancelled state, lead/contact references and completion timestamps. There are no automatic seeds, migrations, fallback to memory, new HTTP endpoints or runtime DI activation. Bulk preview deletion throws and is intentionally disabled; this is a candidate behavior change that must be mapped explicitly if the runtime is activated.

This slice implements adapters for the reviewed `main` contracts, not automatic compatibility with the dirty, more advanced MarketingIndo source tree. The live artifact's complete contract inventory and export mapping still require verification before cutover. Existing .NET 8 projects are unchanged; only the isolated candidate infrastructure is .NET 10.

`tools/CRM.Sql.Acceptance` is an explicit synthetic acceptance harness, not an application initializer. It accepts only a configured local `tcp:127.0.0.1,1433` master administrator connection and requires `provision-and-test-confirmed`. It creates a new uniquely named `CrmMigration_Acceptance_...` database, applies the versioned schema scripts twice, provisions a random-password test login with SELECT/INSERT/UPDATE only, tests adapters and snapshot replay, and disables the login afterward. It retains the synthetic database for evidence, never opens an existing business database and suppresses provider/credential details. Administrator provisioning and restricted data operations are separate. No runtime should receive this administrator connection.

Verified at `2b0858acf72eaafa860e3cd98629ce49db21c259`: zero-warning/error acceptance build and 43/43 migration/adapter tests on both machines. Coverage for new SQL adapter code: 100% lines, 90% branches on both; migration Application 100% / 98.57%, Infrastructure 100% / 100%. These are component metrics, NOT full CRM coverage. SQL Server acceptance passed on trabajo in `CrmMigration_Acceptance_20260918163833_ebc5987e`, including schema idempotence, tenant/store isolation, fresh-context read, update, SQL-denied DELETE, idempotent import and different-data overwrite refusal. Acceptance process exited 0 without a login-cleanup warning. Full CRM regression and runtime activation remain separate gates.

Security/audit impact: no authorization surface or identity/audit engine added; existing Portal contracts remain authoritative. Menu/notification/configuration impact: none. Production/migration operations require the existing Portal authorization and audit boundary before candidate activation. All source runtimes and original databases/volumes remain preserved.

Framework integration constraint: the existing .NET 8 API cannot directly reference the new .NET 10 SQL project. Activating it requires a reviewed .NET 10 API candidate (with its test-host/runtime dependencies) or an expressly approved older-framework exception; neither was silently applied. The baseline backend rule favors a reviewed .NET 10 candidate. The current live CRM must not be restarted as part of framework preparation.

Full existing CRM regression at `2b0858acf72eaafa860e3cd98629ce49db21c259`: 733 Unit + 180 Architecture tests passed on both machines. MarketingIndo's existing Unit project has no XPlat collector, so its requested coverage collection produced a collector warning; no full-CRM coverage result is claimed. On trabajo, official Microsoft ASP.NET Core 8.0.31 and .NET Runtime 8.0.31 prerequisites were installed side-by-side with .NET 10 after SHA512 verification against official release metadata. Both installers exited 0, without a workstation or service restart. This corrected a test-host prerequisite failure, not a source-code defect.

Hardening checkpoint `810cc90ca3564b8b81abc3b357fbd07f1a880b15`: SQL context now rejects every non-`CrmMigration_` catalog before opening a connection. Regression cases include PortalSecurity, master, a missing suffix and a differently cased prefix. 48/48 migration/adapter tests and zero-warning/error acceptance builds passed on both machines; SQL adapter component coverage is 100% lines / 90.69% branches on both.

Real SQL acceptance was repeated after catalog hardening at that same commit and passed, exit 0, on trabajo in `CrmMigration_Acceptance_20260918164705_17e7e9eb`. No temporary-login cleanup warning was emitted. Both synthetic databases were retained rather than deleting evidence or original resources. No application image was built/published/deployed by this slice; no Compose service, port, database container or volume was recreated. The existing local SQL Server was reused with dedicated database and restricted temporary-login isolation. Registry visibility/digest parity and runtime promotion are not claimed by these source/test results.

Dependency audit on MarketingIndo at `810cc90`: `dotnet list tools/CRM.Runtime.Sql/CRM.Runtime.Sql.csproj package --vulnerable --include-transitive` and the equivalent acceptance-project command exited 0 and reported no vulnerable packages using the current official NuGet feed. This is an advisory-feed check, not proof of absence of all vulnerabilities or a replacement for pending security review.

## Operator prerequisites

- .NET 10 SDK; existing CRM .NET 8 behavior remains unchanged.
- A dedicated empty migration database on the existing SQL Server; never a Portal database.
- Apply `tools/CRM.Migration.Infrastructure/sql/001_snapshot_records.sql` through a reviewed migration step. The tool does not create databases/schema automatically.
- A restricted DB account configured locally through `CRM_MIGRATION_CONNECTION`; do not commit or print this value. Provisioning privileges are separate from runtime import/export privileges.
- A protected directory for sensitive snapshot files. Export uses CreateNew and refuses to overwrite files.

Commands: `dotnet run --project tools/CRM.Migration.Tool -- validate <tenant> <snapshot-file>`, `export <tenant> <new-file>`, or `import-confirmed <tenant> <snapshot-file>`. Output includes counts only. Checksums detect corruption, not authorization or authenticity. Unknown schema/store, ambiguous JSON, duplicate IDs, mismatched payload IDs, tenant mismatch, checksum corruption and oversized payloads are rejected before writes.

## Explicit remaining gates

Checkpoint 2026-09-18: core commit `fa45a5dd5489eaea5c1244d333ab6fa0f41b832a` built in Release with zero warnings/errors on MarketingIndo and trabajo, and passed 34/34 tests on each. MarketingIndo Application/Infrastructure coverage: 119/119 lines and 77/78 branches. This excludes the CLI and live CRM. Draft PR: https://github.com/christyepez/CRM/pull/252. Subsequent contract inventory also identified DocumentMetadata and Tag stores; their snapshot kinds and relational round-trip tests are included separately. No live CRM service was restarted or reconfigured by this slice.

1. Capture the LIVE in-memory store state faithfully before any source restart. Public DTO responses may omit internal state; they must not be treated automatically as full store snapshots.
2. Reconcile the implemented typed SQL adapters against every LIVE CRM store and map full internal state, not just exposed DTOs. Review business invariants, reference integrity and lifecycle compatibility.
3. Integrate SQL mode and a faithful export/import boundary into a tested candidate runtime. Do not silently fall back to in-memory storage.
4. Reuse Portal permission checks and audit publication for runtime migration operations. No new public migration surface may be exposed without explicit authorization/security review.
5. Test backup recovery, SQL Server transaction behavior, reconciliation and restart persistence; meet coverage/architecture gates and review the draft PR.
6. Freeze source writers, capture final state, import/reconcile, switch ownership and validate MarketingIndo client connectivity. Preserve rollback and all original databases/volumes.

Centralization and CRM durable runtime persistence are NOT complete until these gates pass. Snapshot structural validation alone does not validate every CRM business invariant. The 21 offline-volume archives are also not evidence of application-level recovery or migration of active auxiliary stores.
