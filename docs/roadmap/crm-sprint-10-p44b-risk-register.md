# P44B Risk Register

| RiskId | Severity | Blocking | Mitigation | ResidualRisk | AcceptanceRequired |
| --- | --- | --- | --- | --- | --- |
| P44B-R1 local-only artifact | Medium | No | publish to registry or explicitly accept local-only candidate | artifact retrieval depends on local Docker host | true |
| P44B-R2 no SBOM | Low | No | generate SBOM when official tooling is available | limited software bill evidence | true |
| P44B-R3 no official image vulnerability scan | Medium | No | run approved scanner before broad rollout | image security evidence is partial | true |
| P44B-R4 previous rollback artifact absent | Medium | No | P45 must capture previous artifact before switch | rollback may be abort-only if no prior production exists | true |

CriticalProductionBlockers: 0
HighBlockingRisks: 0
