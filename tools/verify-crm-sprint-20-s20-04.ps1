$ErrorActionPreference='Stop'
$main=Get-Content 'frontend/crm-web/src/main.ts' -Raw
foreach($m in @("path: 'foundation/notes'","/api/crm/foundation/notes","class NoteManagementPageComponent","Archive note","Foundation only")){if(-not$main.Contains($m)){throw "Missing S20-04 marker: $m"}}
foreach($m in @("/api/crm/notes'","deleteNote","scheduleActivity")){if($main.Contains($m)){throw "Forbidden S20-04 marker: $m"}}
if(-not(Test-Path 'tests/CRM.ArchitectureTests/NoteFrontendGuardrailTests.cs')){throw 'Missing Note frontend guardrail tests.'}
Write-Host 'CRM Sprint 20 S20-04 verification passed.'