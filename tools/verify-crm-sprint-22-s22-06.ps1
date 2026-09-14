$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot
$doc=Get-Content (Join-Path $root 'docs/roadmap/crm-sprint-22-s22-06-crm-document-metadata-local-integration-validation.md') -Raw
$result=Get-Content (Join-Path $root 'docs/roadmap/crm-sprint-22-s22-06-crm-document-metadata-local-integration-result.json') -Raw
foreach($token in @('S2206Decision: Implemented','BinaryStorageObserved: false','PortalContentRuntimeObserved: false')){if($doc -notmatch [regex]::Escape($token)){throw "Missing $token"}}
foreach($token in @('"CreateScenario":  "PASS"','"DeleteRouteAvailable":  false','"ProductiveDocumentRouteAvailable":  false')){if($result -notmatch [regex]::Escape($token)){throw "Missing result $token"}}
Write-Output 'CRM Sprint 22 S22-06 verification passed.'
