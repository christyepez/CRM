# P43 Production Configuration Manifest

ProductionConfigurationManifestVersion: crm-p43-production-configuration-manifest-v1

Environment: Production
Service: CRM API
TargetImage: P44 frozen immutable digest/tag
Ports: production ingress/gateway mapping
Gateway: conditional explicit scope
DNS: required; no private URL in repository
TLS: required at edge
HealthEndpoints: `/health`, `/health/ready`, `/api/crm/readiness`
SecretReferences: logical names only
DependencyReferences: Portal/Common DB conditional and disabled by default
Logging: structured/container logs plus approved sink
Monitoring: health, availability, error, latency, restart, resource and security alerts
RuntimeFlags: approved in P44, executed in P45 only

ProductionScopeFrozen: true
ProductionTargetPreparedForFreeze: true
