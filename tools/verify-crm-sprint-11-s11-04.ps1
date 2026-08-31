$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "frontend/crm-web/src/main.ts",
    "frontend/crm-web/src/styles.css",
    "frontend/crm-web/tools/verify-crm-foundation.mjs",
    "docs/roadmap/crm-sprint-11-s11-04-lead-intake-frontend-foundation.md",
    "codex/prompts/sprint-11-lead-qualification-s11-05.md"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing required S11-04 file: $file"
    }
}

$main = Get-Content "frontend/crm-web/src/main.ts" -Raw
if (-not $main.Contains("foundation/leads/qualification")) {
    throw "Frontend foundation route is missing."
}

if (-not $main.Contains("/api/crm/foundation/leads/{leadId}/qualification")) {
    throw "Foundation qualification API route marker is missing."
}

if ($main.Contains("/api/crm/leads")) {
    throw "Frontend must not call productive lead route."
}

if ($main -match "localStorage|sessionStorage|access_token|refresh_token|Request\.Headers|Headers\[|Bearer ") {
    throw "Frontend must not introduce token storage or auth header usage."
}

if ($main -match "ConnectionStrings:|Server=|User Id=|Password=|new SqlConnection|SqlConnection\(|UseSqlServer\(") {
    throw "Frontend must not introduce Common DB coupling."
}

Write-Host "CRM Sprint 11 S11-04 verification passed."
