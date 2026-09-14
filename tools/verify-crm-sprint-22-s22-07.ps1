$ErrorActionPreference='Stop'
$root=Split-Path -Parent $PSScriptRoot
$doc=Get-Content (Join-Path $root 'docs/releases/crm-sprint-22-crm-document-metadata-closure.md') -Raw
foreach($token in @('S2207Decision: ClosedSuccessfully','BinaryStorageEnabled: false','RecommendedNextSlice: CRM Tag Foundation')){if($doc -notmatch [regex]::Escape($token)){throw "Missing $token"}}
Write-Output 'CRM Sprint 22 S22-07 verification passed.'
