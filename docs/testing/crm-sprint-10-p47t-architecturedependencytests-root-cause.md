# CRM Sprint 10 P47T - ArchitectureDependencyTests Root Cause

P47TArchitectureDependencyTestsRootCauseExists: true
ArchitectureTestsRootCause: RepeatedRepositoryWideSourceScanWithoutCachingAndTextFileFiltering

Findings:

- `ArchitectureDependencyTests` repeatedly called `ReadSourceFiles("src", "frontend", "docker-compose.yml", "docker-compose.crm.yml")`.
- The helper recursively enumerated and read source files for every call.
- Class-level execution exceeded the controlled 30 second window before stabilization.
- Individual methods passed, which indicated no assertion deadlock and no runtime dependency wait.

Classification:

FilesystemWait: true
ParallelizationIssue: possible
TestHostIssue: secondary when interrupted runs left `testhost` locking the architecture test DLL
ApplicationRuntimeIssue: false
DockerWait: false
ExternalDependencyWait: false

