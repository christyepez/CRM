$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$doc = Join-Path $root "docs\roadmap\crm-sprint-11-s11-06-lead-qualification-local-integration.md"
$runner = Join-Path $root "tools\run-crm-sprint-11-s11-06-local-integration.ps1"
$packageJson = Join-Path $root "frontend\crm-web\package.json"
$proxy = Join-Path $root "frontend\crm-web\proxy.conf.json"
$server = Join-Path $root "frontend\crm-web\tools\serve-local-integration.mjs"

foreach ($path in @($doc, $runner, $packageJson, $proxy, $server)) {
    if (-not (Test-Path $path)) {
        throw "Missing required S11-06 artifact: $path"
    }
}

$docText = Get-Content $doc -Raw
$requiredDocMarkers = @(
    'S11-06 validated the Lead Qualification foundation workflow locally',
    'Frontend-to-API proxy',
    'Productive route status: 404',
    'SimulatedProductionTouched: false',
    'PortalRuntimeObserved: false',
    'CommonDbRuntimeObserved: false',
    'Next gate: CRM Sprint 11 S11-07'
)

foreach ($marker in $requiredDocMarkers) {
    if ($docText -notlike "*$marker*") {
        throw "S11-06 documentation missing marker: $marker"
    }
}

$runnerText = Get-Content $runner -Raw
$requiredRunnerMarkers = @(
    '/foundation/leads/qualification',
    '/api/crm/foundation/leads',
    'ProductiveRouteNegative',
    '/api/crm/leads/lead-preview-001/qualification',
    'ExpectedStatus'
)

foreach ($marker in $requiredRunnerMarkers) {
    if ($runnerText -notlike "*$marker*") {
        throw "S11-06 runner missing marker: $marker"
    }
}

$packageJsonText = Get-Content $packageJson -Raw
if ($packageJsonText -notlike '*"start": "node tools/serve-local-integration.mjs"*') {
    throw "frontend/crm-web/package.json must expose the S11-06 local integration start script."
}

$proxyText = Get-Content $proxy -Raw
if ($proxyText -notlike "*http://localhost:8093*" -or $proxyText -notlike '*/api*') {
    throw "frontend/crm-web/proxy.conf.json must route /api to the local CRM API."
}

$serverText = Get-Content $server -Raw
if ($serverText -notlike "*127.0.0.1*" -or $serverText -notlike "*4200*" -or $serverText -notlike "*localhost:8093*") {
    throw "Local integration server must bind loopback 4200 and proxy to localhost:8093."
}

Write-Host "CRM Sprint 11 S11-06 verification passed."
