$ErrorActionPreference = 'Stop'

function Assert-FileContains {
    param(
        [string] $Path,
        [string] $Pattern,
        [string] $Message
    )

    if (-not (Test-Path $Path)) {
        throw "Missing required file: $Path"
    }

    $content = Get-Content $Path -Raw
    if ($content -notmatch [regex]::Escape($Pattern)) {
        throw $Message
    }
}

$summary = 'docs/roadmap/crm-sprint-10-p49-controlled-local-simulated-production-execution.md'

Assert-FileContains $summary 'P49ExecutionStarted: true' 'P49 execution was not recorded as started.'
Assert-FileContains $summary 'ApprovalConsumed: true' 'P49 must consume the P48 approval.'
Assert-FileContains $summary 'DeploymentCommandExecuted: docker compose -p crm-prod-sim --env-file .env.prod-sim.example -f docker-compose.prod-sim.yml up -d --force-recreate' 'P49 deployment command mismatch.'
Assert-FileContains $summary 'CandidateImageIdentityMatched: true' 'P49 candidate image did not match.'
Assert-FileContains $summary 'ContainerRunning: true' 'P49 container is not recorded running.'
Assert-FileContains $summary 'DockerHealth: healthy' 'P49 Docker health is not healthy.'
Assert-FileContains $summary 'Health: HTTP 200' 'P49 health failed.'
Assert-FileContains $summary 'Readiness: HTTP 200' 'P49 readiness failed.'
Assert-FileContains $summary 'CRMReadiness: HTTP 200 ReadyForFoundationOnly' 'P49 CRM readiness mismatch.'
Assert-FileContains $summary 'RootStatus: HTTP 404' 'P49 root status should remain 404.'
Assert-FileContains $summary 'SwaggerStatus: HTTP 404' 'P49 swagger status should remain 404.'
Assert-FileContains $summary 'CriticalLogErrorsDetected: false' 'P49 critical log errors detected.'
Assert-FileContains $summary 'PortalRuntimeCallsDetected: false' 'P49 must not call Portal runtime.'
Assert-FileContains $summary 'CommonDbRuntimeCallsDetected: false' 'P49 must not call Common DB runtime.'
Assert-FileContains $summary 'ProductionDataWritesDetected: false' 'P49 must not write production data.'
Assert-FileContains $summary 'NonProdUnaffected: true' 'P49 must not affect NonProd.'
Assert-FileContains $summary 'RollbackExecuted: false' 'P49 must not rollback after success.'
Assert-FileContains $summary 'SimulatedProductionActivated: true' 'P49 must activate simulated Production.'
Assert-FileContains $summary 'RealProductionActivated: false' 'P49 must not activate real Production.'
Assert-FileContains $summary 'P48ApprovalReusable: false' 'P48 approval must not be reusable.'
Assert-FileContains $summary 'NewHumanApprovalRequired: true' 'Future retry must require new human approval.'
Assert-FileContains $summary 'P49Decision: ExecutedSuccessfully' 'P49 decision mismatch.'

Write-Output 'PASS CRM P49 guardrails passed.'
