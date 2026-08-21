# P43 Performance Evidence and Thresholds

PerformanceProductionReadiness: ReadyForApproval

Safe NonProduction evidence against `http://localhost:8093`; 10 sequential requests per endpoint, no destructive load.

| Endpoint | Count | Errors | P50Latency | P95Latency | P99Latency |
| --- | ---: | ---: | --- | --- | --- |
| `/health` | 10 | 0 | 16 ms | 18 ms | 18 ms |
| `/health/ready` | 10 | 0 | 18 ms | 21 ms | 21 ms |
| `/api/crm/readiness` | 10 | 0 | 16 ms | 17 ms | 17 ms |

P50Latency: TBD-business-threshold
P95Latency: TBD-business-threshold
P99Latency: TBD-business-threshold
MaxErrorRate: TBD-business-threshold
MaxCpu: TBD-business-threshold
MaxMemory: TBD-business-threshold
MaxRestartCount: 0 unexpected restarts
MaxTimeoutRate: TBD-business-threshold
