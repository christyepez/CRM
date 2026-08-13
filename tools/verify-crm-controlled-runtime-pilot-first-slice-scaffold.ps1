$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$service = Get-Content (Join-Path $root "src/CRM.Application/Foundation/CrmControlledRuntimePilotFirstSliceScaffoldStatusService.cs") -Raw
$client = Get-Content (Join-Path $root "src/CRM.Infrastructure/Portal/ControlledRuntimePilot/DisabledPortalRuntimeClient.cs") -Raw
$program = Get-Content (Join-Path $root "src/CRM.Api/Program.cs") -Raw
$compose = Get-Content (Join-Path $root "docker-compose.yml") -Raw

if ($program -notlike "*/api/crm/foundation/sprint-10/controlled-runtime-pilot-first-slice-scaffold*") {
    throw "P14 foundation endpoint is missing."
}

foreach ($expected in @(
    "CrmSprint10P14ControlledRuntimePilotFirstImplementationSliceScaffoldExists: true",
    "FirstImplementationSliceScaffoldPrepared: true",
    "FirstImplementationSliceScaffoldOnly: true",
    "ConditionalFutureGoExecuted: false",
    "RuntimePortalCallsEnabled: false",
    "CommonDbRuntimeEnabled: false",
    "CrmProductionReady: false"
)) {
    if ($service -notlike "*$expected*") {
        throw "P14 service is missing expected marker: $expected"
    }
}

foreach ($expected in @(
    "Attempted: false",
    "ExternalCallAttempted: false",
    "PortalCouplingEnabled: false",
    "PortalRoutesEnabled: false",
    "PortalNavigationEnabled: false"
)) {
    if ($client -notlike "*$expected*") {
        throw "Disabled Portal runtime client is missing expected no-op marker: $expected"
    }
}

$httpPattern = "HttpClient|SendAsync|GetAsync|PostAsync|http" + "://|https" + "://"
if ($client -match $httpPattern) {
    throw "Disabled Portal runtime client must not contain HTTP calls or URLs."
}

if ($compose -match "mcr\.microsoft\.com/mssql|1433:1433|PortalCorporativo|portal.*image:|portal.*build:") {
    throw "CRM compose appears to define SQL Server or Portal services."
}

Write-Host "PASS CRM controlled runtime pilot first slice scaffold verification passed."
