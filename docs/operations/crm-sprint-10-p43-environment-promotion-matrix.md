# P43 Environment Promotion Matrix

| Setting | NonProductionState | ProductionRequirement | EnvironmentSpecific | Secret | ValidationMethod | Ready |
| --- | --- | --- | --- | --- | --- | --- |
| Runtime mode | NonProduction | approved production flag | Yes | No | manifest | true |
| Port | 8093 local | approved ingress/gateway | Yes | No | smoke | true |
| Target image | local build evidence | immutable digest/tag | Yes | No | freeze model | true |
| Gateway | inactive | explicit scope | Yes | No | P44 approval | true |
| DNS/TLS | local/no repo cert | approved DNS/TLS | Yes | No | environment validation | true |
| Secrets | references only | approved references | Yes | Yes | name-only validation | true |
| Portal/Common DB | disabled | conditional approval | Yes | Yes | runtime flag check | true |
| Logging/Monitoring | local evidence | approved sink/alerts | Yes | No | monitoring gate | true |
