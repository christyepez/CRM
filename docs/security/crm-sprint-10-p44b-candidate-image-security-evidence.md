# P44B Candidate Image Security Evidence

ImageSecurityValidation: PassedWithConditions

- Base image: mcr.microsoft.com/dotnet/aspnet:8.0
- Expected exposed port: 8080 via ASPNETCORE_URLS
- Secrets absent from image evidence: true
- Production secrets absent: true
- Portal services absent: true
- Common DB runtime absent: true
- Source in runtime image: not detected by runtime health evidence; Dockerfile copies only published output into final stage
- Non-root runtime: not explicitly configured in Dockerfile
- SBOMAvailable: false
- VulnerabilityScanStatus: NotAvailableNoOfficialScannerConfigured

SecurityReadyForApproval: true
SecurityCondition: registry publication, SBOM, vulnerability scan and non-root hardening should be addressed or accepted before broad production rollout.
