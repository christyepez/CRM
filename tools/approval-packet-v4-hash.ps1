param([string] $Path = "docs/roadmap/crm-sprint-10-p47-final-approval-packet-v4.json")

$ErrorActionPreference = "Stop"
$InputPath = $Path
. "$PSScriptRoot\approval-packet-hash.ps1"

$packet = Get-Content -Raw -LiteralPath $InputPath | ConvertFrom-Json
$canonical = ConvertTo-CanonicalJson -Value $packet
$bytes = [System.Text.Encoding]::UTF8.GetBytes($canonical)
$sha = [System.Security.Cryptography.SHA256]::Create()
$hash = $sha.ComputeHash($bytes)
Write-Output (([System.BitConverter]::ToString($hash) -replace "-", "").ToLowerInvariant())
