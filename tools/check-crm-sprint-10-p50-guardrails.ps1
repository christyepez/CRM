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

$closure = 'docs/roadmap/crm-sprint-10-p50-local-simulated-production-pilot-closure.md'

Assert-FileContains $closure 'P49Decision: ExecutedSuccessfully' 'P50 must preserve successful P49.'
Assert-FileContains $closure 'P48ApprovalConsumed: true' 'P50 must preserve consumed P48 approval.'
Assert-FileContains $closure 'P48ApprovalReusable: false' 'P50 must not make P48 reusable.'
Assert-FileContains $closure 'ContainerRunning: true' 'P50 target must be running.'
Assert-FileContains $closure 'DockerHealth: healthy' 'P50 target must be healthy.'
Assert-FileContains $closure 'CandidateImageIdentityMatched: true' 'P50 image identity mismatch.'
Assert-FileContains $closure 'Health: HTTP 200' 'P50 health mismatch.'
Assert-FileContains $closure 'Readiness: HTTP 200' 'P50 readiness mismatch.'
Assert-FileContains $closure 'RootStatus: HTTP 404' 'P50 root status must remain expected 404.'
Assert-FileContains $closure 'SwaggerStatus: HTTP 404' 'P50 swagger status must remain expected 404.'
Assert-FileContains $closure 'RestartLoopDetected: false' 'P50 restart loop detected.'
Assert-FileContains $closure 'CriticalLogErrorsDetected: false' 'P50 critical logs detected.'
Assert-FileContains $closure 'PortalRuntimeCallsDetected: false' 'P50 must not detect Portal runtime calls.'
Assert-FileContains $closure 'CommonDbRuntimeCallsDetected: false' 'P50 must not detect Common DB runtime calls.'
Assert-FileContains $closure 'ProductionDataWritesDetected: false' 'P50 must not detect production data writes.'
Assert-FileContains $closure 'NonProdUnaffected: true' 'P50 must keep NonProd unaffected.'
Assert-FileContains $closure 'RollbackReady: true' 'P50 rollback readiness mismatch.'
Assert-FileContains $closure 'RollbackExecutedInP50: false' 'P50 must not execute rollback on healthy target.'
Assert-FileContains $closure 'SecurityPostExecutionValidation: PASS' 'P50 security postcheck mismatch.'
Assert-FileContains $closure 'CriticalClosureBlockers: 0' 'P50 closure blockers must be zero.'
Assert-FileContains $closure 'SimulatedProductionPilotStatus: ClosedSuccessfully' 'P50 pilot status mismatch.'
Assert-FileContains $closure 'RealProductionActivated: false' 'P50 must not activate real Production.'
Assert-FileContains $closure 'RealProductionAuthorized: false' 'P50 must not authorize real Production.'
Assert-FileContains $closure 'P50Decision: ClosedSuccessfully' 'P50 decision mismatch.'

Write-Output 'PASS CRM P50 guardrails passed.'
