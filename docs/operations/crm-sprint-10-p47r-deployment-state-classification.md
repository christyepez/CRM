# CRM Sprint 10 P47R - Deployment State Classification

P47RDeploymentStateClassificationExists: true
ProductionDeploymentState: Unknown
FirstDeploymentConfirmed: false
ExistingDeploymentConfirmed: false

P47R cannot classify the deployment as `FirstDeployment` or `ExistingDeployment`.

Blocking evidence required:

- For `FirstDeployment`: signed/reproducible evidence that no CRM production service, route, container, or port binding exists.
- For `ExistingDeployment`: current image tag/id/digest, configuration version, runtime identifier, and endpoint evidence.

