# P44B Rollback Revalidation

RollbackReadyForApproval: true

CandidateArtifact: crm-api:prod-candidate-8623c619
CandidateArtifactId: sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37
PreviousArtifact: not available in registry
ConfigurationVersion: crm-p43-production-configuration-manifest-v1

Rollback model:

- If P45 has no previous production artifact, abort before production switch.
- If P45 has a previous production artifact, rollback must reference that immutable artifact.
- A mutable tag must not be used as rollback anchor.

RollbackCondition: previous production artifact identity must be captured by P45 preflight.
