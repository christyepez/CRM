$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "docs/roadmap/crm-sprint-12-s12-06-contact-management-local-integration.md",
    "tools/run-crm-sprint-12-s12-06-local-integration.ps1",
    "tools/verify-crm-sprint-12-s12-06.ps1",
    "codex/prompts/sprint-12-contact-management-s12-07.md",
    "frontend/crm-web/src/main.ts",
    "frontend/crm-web/proxy.conf.json",
    "frontend/crm-web/tools/serve-local-integration.mjs",
    "src/CRM.Api/Program.cs",
    "src/CRM.Application/Foundation/FoundationContactCrudService.cs"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing S12-06 artifact or prerequisite: $file"
    }
}

$doc = Get-Content "docs/roadmap/crm-sprint-12-s12-06-contact-management-local-integration.md" -Raw
$runner = Get-Content "tools/run-crm-sprint-12-s12-06-local-integration.ps1" -Raw
$program = Get-Content "src/CRM.Api/Program.cs" -Raw
$frontend = Get-Content "frontend/crm-web/src/main.ts" -Raw
$proxy = Get-Content "frontend/crm-web/proxy.conf.json" -Raw
$server = Get-Content "frontend/crm-web/tools/serve-local-integration.mjs" -Raw
$service = Get-Content "src/CRM.Application/Foundation/FoundationContactCrudService.cs" -Raw
$nextTask = Get-Content "codex/next-task.md" -Raw
$tasks = Get-Content "codex/TASKS.md" -Raw

foreach ($marker in @(
    "ContactManagementImplementationStatus: LocalIntegrationValidated",
    "ContactManagementLocalIntegration: Validated",
    "ProductiveContactRouteEnabled: false",
    "DeleteRouteAvailable: false",
    "LeadContactRuntimeImplemented: false",
    "PortalRuntimeEnabled: false",
    "TokenRuntimeObserved: false",
    "CommonDbRuntimeObserved: false",
    "RuntimePersistenceClassification: FoundationOnly",
    "PiiPayloadLogged: false",
    "SensitiveRuntimeLogDetected: false",
    "CriticalIntegrationLogErrors: false",
    "FrontendRuntimeErrors: false",
    "ReadAfterWriteConsistent: true",
    "RealDataDetected: false",
    "SimulatedProductionTouched: false",
    "S1206Decision: Implemented")) {
    if (-not $doc.Contains($marker)) {
        throw "S12-06 integration document missing marker: $marker"
    }
}

foreach ($marker in @(
    "GET /api/crm/foundation/contacts",
    "POST /api/crm/foundation/contacts",
    "PUT /api/crm/foundation/contacts",
    "GET/POST/PUT",
    "DELETE",
    "LatencyAverageMs",
    "IntegrationDefectsFixed")) {
    if (-not $doc.Contains($marker)) {
        throw "S12-06 integration document missing evidence marker: $marker"
    }
}

foreach ($marker in @(
    "http://localhost:8093",
    "http://127.0.0.1:4200",
    "/api/crm/foundation/contacts",
    "/foundation/contacts",
    "ProductiveContactRouteAvailable",
    "DeleteRouteAvailable",
    "FrontendToApiConnectivity",
    "ProxyOrCorsValidation")) {
    if (-not $runner.Contains($marker)) {
        throw "S12-06 runner missing marker: $marker"
    }
}

foreach ($marker in @(
    'MapGet("/api/crm/foundation/contacts"',
    'MapPost("/api/crm/foundation/contacts"',
    'MapPut("/api/crm/foundation/contacts/{id}"')) {
    if (-not $program.Contains($marker)) {
        throw "Foundation Contact API route missing: $marker"
    }
}

$programWithoutFoundation = $program.Replace('"/api/crm/foundation/contacts"', '""').Replace('"/api/crm/foundation/contacts/{id}"', '""').Replace('"/api/crm/foundation/contacts/preview"', '""').Replace('"/api/crm/foundation/contacts/read-model-preview"', '""')
foreach ($forbidden in @('"/api/crm/contacts"', 'MapDelete("/api/crm/contacts', "ConvertLeadToContact", "CreateContactFromLead")) {
    if ($programWithoutFoundation.Contains($forbidden)) {
        throw "Forbidden productive Contact/Lead runtime marker detected: $forbidden"
    }
}

if (-not $frontend.Contains("{ path: 'foundation/contacts', component: ContactManagementPageComponent }")) {
    throw "Frontend Contact foundation route is missing."
}

$contactSourceStart = $frontend.IndexOf("type PreferredContactMethod")
$contactSourceEnd = $frontend.IndexOf("selector: 'crm-home'")
$contactSource = if ($contactSourceStart -ge 0 -and $contactSourceEnd -gt $contactSourceStart) { $frontend.Substring($contactSourceStart, $contactSourceEnd - $contactSourceStart) } else { "" }
if (-not $contactSource.Contains("/api/crm/foundation/contacts")) {
    throw "Contact frontend must use foundation Contact API."
}

foreach ($forbidden in @("/api/crm/contacts", "innerHTML", "bypassSecurityTrustHtml", "localStorage", "sessionStorage", "Bearer ", "Authorization", "DeleteContact", "deleteContact", "ConvertLeadToContact", "CreateContactFromLead")) {
    if ($contactSource.Contains($forbidden)) {
        throw "Forbidden Contact frontend marker detected: $forbidden"
    }
}

if (-not ($proxy.Contains("http://localhost:8093") -and $proxy.Contains("/api"))) {
    throw "Angular proxy must route /api to local CRM API."
}

if (-not ($server.Contains("127.0.0.1") -and $server.Contains("4200") -and $server.Contains("localhost:8093"))) {
    throw "Local integration frontend server must bind loopback and proxy to CRM API."
}

if (-not $service.Contains('GetValueOrDefault("title") ?? item.Metadata.GetValueOrDefault("role")')) {
    throw "Foundation Contact read path must preserve role/title metadata compatibility found during S12-06."
}

foreach ($marker in @("CRM Sprint 12 S12-07 - Contact Management Sprint Closure", "codex/prompts/sprint-12-contact-management-s12-07.md", "S12-06 merge commit required")) {
    if (-not $nextTask.Contains($marker)) {
        throw "codex/next-task.md must point to S12-07: $marker"
    }
}

if (-not $tasks.Contains("S1206Decision: Implemented")) {
    throw "codex/TASKS.md must record S12-06 implementation."
}

Write-Host "CRM Sprint 12 S12-06 verification passed."
