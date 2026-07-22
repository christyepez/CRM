# CRM Sprint 2 Integrated Evidence

Evidence consolidated from P1-P5:
- P1: persistence activation review kept database activation blocked.
- P2: `NonProductionSeam` enabled in-memory stores only.
- P3: Portal authorization simulation validated permissions without real Portal runtime.
- P4: foundation CRUD enabled under foundation routes only.
- P5: integration readiness returned `NotReady` and `ContinueReview`.

Expected validation evidence for P6:
- `dotnet restore CRM.sln`.
- `dotnet build CRM.sln --no-restore`.
- `DOTNET_ROLL_FORWARD=Major dotnet test CRM.sln --no-build`.
- Frontend build and verifier.
- Health checks on port 8093.
- Docker config without SQL Server.

Known environment note: Docker image build may be `BLOCKED_EXTERNAL_REGISTRY`.
