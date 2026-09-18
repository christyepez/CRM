# CRM centralization — isolated migration slice

Owner approved SQL persistence and safe export/import on 2026-09-18. HIGH data-impact change. Current source CRM remains active and unchanged; existing dirty worktrees are not modified.

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
