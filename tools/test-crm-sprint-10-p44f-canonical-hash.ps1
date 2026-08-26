$ErrorActionPreference = "Stop"
. $PSScriptRoot\approval-packet-hash.ps1

$root = Split-Path -Parent $PSScriptRoot
$packetPath = Join-Path $root "docs/roadmap/crm-sprint-10-p44f-final-approval-packet-v3.json"
$runId = [System.Guid]::NewGuid().ToString("N")

$hash1 = (Get-ApprovalPacketHash -Path $packetPath).Hash
$hash2 = (Get-ApprovalPacketHash -Path $packetPath).Hash
$hash3 = (Get-ApprovalPacketHash -Path $packetPath).Hash
if ($hash1 -ne $hash2 -or $hash2 -ne $hash3) { throw "SameInputSameHash failed." }

$packet = Get-Content -Raw -LiteralPath $packetPath | ConvertFrom-Json
$ordered = [ordered]@{}
foreach ($property in ($packet.PSObject.Properties | Sort-Object Name -Descending)) {
    $ordered[$property.Name] = $property.Value
}
$orderHash = (ConvertTo-CanonicalJson -Value $ordered)
$originalCanonical = (Get-ApprovalPacketHash -Path $packetPath).CanonicalJson
if ($orderHash -ne $originalCanonical) { throw "PropertyOrderDoesNotChangeHash failed." }

$prettyPath = Join-Path ([System.IO.Path]::GetTempPath()) "crm-p44f-packet-pretty-$runId.json"
$packet | ConvertTo-Json -Depth 20 | Set-Content -NoNewline -Encoding UTF8 -LiteralPath $prettyPath
$prettyHash = (Get-ApprovalPacketHash -Path $prettyPath).Hash
if ($prettyHash -ne $hash1) { throw "WhitespaceDoesNotChangeHash failed." }

$crlfPath = Join-Path ([System.IO.Path]::GetTempPath()) "crm-p44f-packet-crlf-$runId.json"
((Get-Content -Raw -LiteralPath $packetPath) -replace "`n", "`r`n") | Set-Content -NoNewline -Encoding UTF8 -LiteralPath $crlfPath
$crlfHash = (Get-ApprovalPacketHash -Path $crlfPath).Hash
if ($crlfHash -ne $hash1) { throw "LineEndingsDoNotChangeHash failed." }

$semantic = Get-Content -Raw -LiteralPath $packetPath | ConvertFrom-Json
$semantic.portalIncluded = $true
$semanticPath = Join-Path ([System.IO.Path]::GetTempPath()) "crm-p44f-packet-semantic-$runId.json"
$semantic | ConvertTo-Json -Depth 20 | Set-Content -NoNewline -Encoding UTF8 -LiteralPath $semanticPath
$semanticFailed = $false
try { $null = Get-ApprovalPacketHash -Path $semanticPath } catch { $semanticFailed = $true }
if (-not $semanticFailed) { throw "SemanticChangeChangesHash failed because portalIncluded=true was accepted." }

$missing = Get-Content -Raw -LiteralPath $packetPath | ConvertFrom-Json
$missing.PSObject.Properties.Remove("candidateImageId")
$missingPath = Join-Path ([System.IO.Path]::GetTempPath()) "crm-p44f-packet-missing-$runId.json"
$missing | ConvertTo-Json -Depth 20 | Set-Content -NoNewline -Encoding UTF8 -LiteralPath $missingPath
$missingFailed = $false
try { $null = Get-ApprovalPacketHash -Path $missingPath } catch { $missingFailed = $true }
if (-not $missingFailed) { throw "RequiredFieldMissingFailsValidation failed." }

$unknown = Get-Content -Raw -LiteralPath $packetPath | ConvertFrom-Json
$unknown | Add-Member -NotePropertyName generatedAt -NotePropertyValue "2026-08-25T00:00:00Z"
$unknownPath = Join-Path ([System.IO.Path]::GetTempPath()) "crm-p44f-packet-unknown-$runId.json"
$unknown | ConvertTo-Json -Depth 20 | Set-Content -NoNewline -Encoding UTF8 -LiteralPath $unknownPath
$unknownFailed = $false
try { $null = Get-ApprovalPacketHash -Path $unknownPath } catch { $unknownFailed = $true }
if (-not $unknownFailed) { throw "UnknownFieldRejectedOrHandledDeterministically failed." }

Write-Host "PASS CRM P44F canonical hash tests passed. Hash=$hash1"
