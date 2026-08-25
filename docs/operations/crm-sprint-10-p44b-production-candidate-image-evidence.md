# P44B Production Candidate Image Evidence

Production Candidate Image: crm-api:prod-candidate-8623c619
Production Candidate Image Tag: prod-candidate-8623c619
Production Candidate Image Id: sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37
Production Candidate Image Digest: crm-api@sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37
CandidateContainerId: af5a444f0635118e26072f596af4ff1586264e02d6c6d1b2d3d19bd86ebede4c
CandidatePort: 8094->8080
CandidateRestartCount: 0

CandidateImageHealthPassed: true
CandidateImageReadinessPassed: true
CandidateImageSmokePassed: true
CandidateImageSecurityPassed: true

Candidate endpoints:

- `/health`: 200
- `/health/live`: 200
- `/health/ready`: 200
- `/api/crm/readiness`: 200
- `/readiness`: 404 endpoint not available

Candidate CPU: 0.01%
Candidate Memory: 32.88MiB

RuntimePortalCallsEnabled: false
PortalRoutesActivated: false
PortalNavigationActivated: false
CommonDbRuntimeEnabled: false
ProductionDataChangesExecuted: false
