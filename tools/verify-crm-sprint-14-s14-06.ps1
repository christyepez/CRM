$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
Set-Location $root

$requiredFiles = @(
    "docs/roadmap/crm-sprint-14-s14-06-opportunity-pipeline-local-integration.md",
    "tools/run-crm-sprint-14-s14-06-local-integration.ps1",
    "tools/verify-crm-sprint-14-s14-06.ps1",
    "codex/prompts/sprint-14-opportunity-pipeline-s14-07.md",
    "codex/next-task.md",
    "codex/TASKS.md",
    "README.md",
    "src/CRM.Api/Program.cs",
    "frontend/crm-web/src/main.ts",
    "frontend/crm-web/proxy.conf.json",
    "frontend/crm-web/tools/serve-local-integration.mjs"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing S14-06 artifact or prerequisite: $file"
    }
}

$doc = Get-Content "docs/roadmap/crm-sprint-14-s14-06-opportunity-pipeline-local-integration.md" -Raw
$runner = Get-Content "tools/run-crm-sprint-14-s14-06-local-integration.ps1" -Raw
$program = Get-Content "src/CRM.Api/Program.cs" -Raw
$frontend = Get-Content "frontend/crm-web/src/main.ts" -Raw
$proxy = Get-Content "frontend/crm-web/proxy.conf.json" -Raw
$server = Get-Content "frontend/crm-web/tools/serve-local-integration.mjs" -Raw
$nextTask = Get-Content "codex/next-task.md" -Raw
$tasks = Get-Content "codex/TASKS.md" -Raw
$readme = Get-Content "README.md" -Raw

foreach ($marker in @(
    "S1406Decision: Implemented",
    "OpportunityPipelineImplementationStatus: LocalIntegrationValidated",
    "OpportunityPipelineLocalIntegration: Validated",
    "BackendHealth: PASS",
    "FrontendOpportunityRouteStatus: 200",
    "FrontendToOpportunityApiConnectivity: PASS",
    "CreateScenario: PASS",
    "ReadAfterCreate: PASS",
    "UpdateScenario: PASS",
    "NoChangeScenario: PASS",
    "ProgressNextStage: PASS",
    "SameStageRejected: PASS",
    "SkipStageRejected: PASS",
    "WinScenario: PASS",
    "RepeatWin: PASS",
    "LoseScenario: PASS",
    "RepeatLose: PASS",
    "CancelScenario: PASS",
    "RepeatCancel: PASS",
    "TerminalUpdateRejected: PASS",
    "TerminalProgressRejected: PASS",
    "NotFoundScenario: PASS",
    "ValidationScenario: PASS",
    "ReadAfterWriteConsistent: true",
    "ProductiveOpportunityRouteAvailable: false",
    "ProductiveOpportunityRouteEnabled: false",
    "DeleteRouteAvailable: false",
    "DeleteBehaviorAdded: false",
    "PortalRuntimeEnabled: false",
    "PortalRuntimeObserved: false",
    "TokenRuntimeObserved: false",
    "CommonDbRuntimeEnabled: false",
    "CommonDbRuntimeObserved: false",
    "RuntimePersistenceClassification: FoundationOnly",
    "RealDataDetected: false",
    "SimulatedProductionTouched: false",
    "CrmProdSimTouched: false",
    "NextTaskPhase: CRM Sprint 14 S14-07 - Opportunity Pipeline Sprint Closure")) {
    if (-not $doc.Contains($marker)) {
        throw "S14-06 integration document missing marker: $marker"
    }
}

foreach ($marker in @(
    "http://localhost:8093",
    "http://127.0.0.1:4200",
    "/foundation/opportunities",
    "/api/crm/foundation/opportunities",
    "/api/crm/opportunities",
    'POST `/progress`',
    'POST `/win`',
    'POST `/lose`',
    'POST `/cancel`',
    "IntegrationLatencySamples: 23",
    "LatencyMinMs: 1",
    "LatencyAverageMs: 18.78",
    "LatencyP95Ms: 56",
    "crm-prod-sim")) {
    if (-not $doc.Contains($marker)) {
        throw "S14-06 evidence marker missing: $marker"
    }
}

foreach ($marker in @(
    "S1406Decision",
    "BackendHealth",
    "FrontendOpportunityRouteStatus",
    "FrontendToOpportunityApiConnectivity",
    "CreateScenario",
    "ReadAfterCreate",
    "UpdateScenario",
    "NoChangeScenario",
    "ProgressNextStage",
    "SameStageRejected",
    "SkipStageRejected",
    "WinScenario",
    "RepeatWin",
    "LoseScenario",
    "RepeatLose",
    "CancelScenario",
    "RepeatCancel",
    "TerminalUpdateRejected",
    "TerminalProgressRejected",
    "NotFoundScenario",
    "ValidationScenario",
    "ReadAfterWriteConsistent",
    "ProductiveOpportunityRouteAvailable",
    "DeleteRouteAvailable",
    "RuntimePersistenceClassification",
    "PortalRuntimeObserved",
    "TokenRuntimeObserved",
    "CommonDbRuntimeObserved",
    "RealDataDetected",
    "SimulatedProductionTouched",
    "IntegrationLatencySamples")) {
    if (-not $runner.Contains($marker)) {
        throw "S14-06 runner missing marker: $marker"
    }
}

if (Test-Path "artifacts/s14-06-integration-output.txt") {
    $evidence = Get-Content "artifacts/s14-06-integration-output.txt" -Raw | ConvertFrom-Json

    foreach ($field in @(
        "BackendHealth",
        "FrontendToOpportunityApiConnectivity",
        "CreateScenario",
        "ReadAfterCreate",
        "UpdateScenario",
        "NoChangeScenario",
        "ProgressNextStage",
        "SameStageRejected",
        "SkipStageRejected",
        "WinScenario",
        "RepeatWin",
        "LoseScenario",
        "RepeatLose",
        "CancelScenario",
        "RepeatCancel",
        "TerminalUpdateRejected",
        "TerminalProgressRejected",
        "NotFoundScenario",
        "ValidationScenario")) {
        if ($evidence.$field -ne "PASS") {
            throw "S14-06 evidence field $field expected PASS, got $($evidence.$field)"
        }
    }

    if ($evidence.S1406Decision -ne "Implemented") { throw "Invalid S1406Decision: $($evidence.S1406Decision)" }
    if ($evidence.LocalBackendUrl -ne "http://localhost:8093") { throw "Unexpected backend URL: $($evidence.LocalBackendUrl)" }
    if ($evidence.LocalFrontendUrl -ne "http://127.0.0.1:4200") { throw "Unexpected frontend URL: $($evidence.LocalFrontendUrl)" }
    if ([int]$evidence.FrontendOpportunityRouteStatus -ne 200) { throw "Frontend route status must be 200." }
    if (-not [bool]$evidence.ReadAfterWriteConsistent) { throw "Read-after-write consistency must be true." }
    if ([bool]$evidence.ProductiveOpportunityRouteAvailable) { throw "Productive Opportunity route must remain unavailable." }
    if ([bool]$evidence.DeleteRouteAvailable) { throw "DELETE route must remain unavailable." }
    if ($evidence.RuntimePersistenceClassification -ne "FoundationOnly") { throw "Runtime persistence must be FoundationOnly." }
    if ([bool]$evidence.PortalRuntimeObserved) { throw "Portal runtime must not be observed." }
    if ([bool]$evidence.TokenRuntimeObserved) { throw "Token runtime must not be observed." }
    if ([bool]$evidence.CommonDbRuntimeObserved) { throw "Common DB runtime must not be observed." }
    if ([bool]$evidence.RealDataDetected) { throw "Real data must not be detected." }
    if ([bool]$evidence.SimulatedProductionTouched) { throw "Simulated Production must remain untouched." }
    if ([int]$evidence.IntegrationLatencySamples -ne 23) { throw "Unexpected latency sample count: $($evidence.IntegrationLatencySamples)" }
    if ([int]$evidence.LatencyMinMs -ne 1) { throw "Unexpected min latency: $($evidence.LatencyMinMs)" }
    if ([decimal]$evidence.LatencyAverageMs -ne [decimal]18.78) { throw "Unexpected average latency: $($evidence.LatencyAverageMs)" }
    if ([int]$evidence.LatencyP95Ms -ne 56) { throw "Unexpected p95 latency: $($evidence.LatencyP95Ms)" }
}

foreach ($marker in @(
    'MapGet("/api/crm/foundation/opportunities"',
    'MapGet("/api/crm/foundation/opportunities/{id}"',
    'MapPost("/api/crm/foundation/opportunities"',
    'MapPut("/api/crm/foundation/opportunities/{id}"',
    'MapPost("/api/crm/foundation/opportunities/{id}/progress"',
    'MapPost("/api/crm/foundation/opportunities/{id}/win"',
    'MapPost("/api/crm/foundation/opportunities/{id}/lose"',
    'MapPost("/api/crm/foundation/opportunities/{id}/cancel"')) {
    if (-not $program.Contains($marker)) {
        throw "Foundation Opportunity API route missing: $marker"
    }
}

$programWithoutFoundation = $program.
    Replace('"/api/crm/foundation/opportunities"', '""').
    Replace('"/api/crm/foundation/opportunities/{id}"', '""').
    Replace('"/api/crm/foundation/opportunities/{id}/progress"', '""').
    Replace('"/api/crm/foundation/opportunities/{id}/win"', '""').
    Replace('"/api/crm/foundation/opportunities/{id}/lose"', '""').
    Replace('"/api/crm/foundation/opportunities/{id}/cancel"', '""')

foreach ($forbidden in @(
    '"/api/crm/opportunities"',
    'MapDelete("/api/crm/opportunities',
    'MapDelete("/api/crm/foundation/opportunities',
    "UseAuthentication",
    "UseAuthorization",
    "UseSqlServer",
    "SqlConnection")) {
    if ($programWithoutFoundation.Contains($forbidden)) {
        throw "Forbidden Opportunity API/runtime marker detected: $forbidden"
    }
}

if (-not $frontend.Contains("{ path: 'foundation/opportunities', component: OpportunityPipelinePageComponent }")) {
    throw "Frontend Opportunity foundation route is missing."
}

$opportunityStart = $frontend.IndexOf("class OpportunityPipelineApiService")
$opportunityEnd = $frontend.IndexOf("selector: 'crm-home'")
$opportunitySource = if ($opportunityStart -ge 0 -and $opportunityEnd -gt $opportunityStart) {
    $frontend.Substring($opportunityStart, $opportunityEnd - $opportunityStart)
} else {
    ""
}

if ([string]::IsNullOrWhiteSpace($opportunitySource)) {
    throw "Could not isolate Opportunity frontend source section."
}

foreach ($marker in @(
    "/api/crm/foundation/opportunities",
    "createOpportunity(request",
    "updateOpportunity(id: string",
    "progressOpportunity(id: string",
    "winOpportunity(id: string)",
    "loseOpportunity(id: string)",
    "cancelOpportunity(id: string)",
    "selectedOpportunityReadonly(",
    "nextStage(")) {
    if (-not $opportunitySource.Contains($marker)) {
        throw "Opportunity frontend marker missing: $marker"
    }
}

foreach ($forbidden in @(
    "/api/crm/opportunities",
    "deleteOpportunity",
    ".delete<",
    "Delete opportunity",
    "localStorage",
    "sessionStorage",
    "Authorization",
    "Bearer",
    "bypassSecurityTrustHtml",
    "innerHTML")) {
    if ($opportunitySource.Contains($forbidden)) {
        throw "Forbidden Opportunity frontend marker detected: $forbidden"
    }
}

if (-not ($proxy.Contains("http://localhost:8093") -and $proxy.Contains("/api"))) {
    throw "Angular proxy must route /api to local CRM API."
}

if (-not ($server.Contains("127.0.0.1") -and $server.Contains("4200") -and $server.Contains("localhost:8093"))) {
    throw "Local integration frontend server must bind loopback and proxy to CRM API."
}

foreach ($content in @($nextTask, $tasks, $readme)) {
    if (-not $content.Contains("CRM Sprint 14 S14-07 - Opportunity Pipeline Sprint Closure")) {
        throw "S14-07 handoff missing from expected documentation."
    }
}

if (-not ($nextTask.Contains("S14-06 merge commit required") -and
    $nextTask.Contains("crm-sprint-14-s14-07-opportunity-pipeline-sprint-closure") -and
    $nextTask.Contains("CRM Sprint 14 S14-07 - Opportunity Pipeline Sprint Closure") -and
    $nextTask.Contains("codex/prompts/sprint-14-opportunity-pipeline-s14-07.md"))) {
    throw "codex/next-task.md must point to S14-07 closure."
}

foreach ($marker in @(
    "S1406Decision: Implemented",
    "NextTaskPhase: CRM Sprint 14 S14-07 - Opportunity Pipeline Sprint Closure",
    "NextTaskPromptFile: codex/prompts/sprint-14-opportunity-pipeline-s14-07.md")) {
    if (-not $tasks.Contains($marker)) {
        throw "codex/TASKS.md missing S14-06 marker: $marker"
    }
}

Write-Host "CRM Sprint 14 S14-06 verification passed."
