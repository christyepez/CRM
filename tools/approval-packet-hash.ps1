param([string] $Path = "docs/roadmap/crm-sprint-10-p44f-final-approval-packet-v3.json")

$ErrorActionPreference = "Stop"

function ConvertTo-JsonStringLiteral {
    param([Parameter(Mandatory=$true)] [string] $Value)

    $builder = New-Object System.Text.StringBuilder
    [void]$builder.Append('"')
    foreach ($char in $Value.ToCharArray()) {
        $code = [int][char]$char
        switch ($char) {
            '"' { [void]$builder.Append('\"'); continue }
            '\' { [void]$builder.Append('\\'); continue }
            "`b" { [void]$builder.Append('\b'); continue }
            "`f" { [void]$builder.Append('\f'); continue }
            "`n" { [void]$builder.Append('\n'); continue }
            "`r" { [void]$builder.Append('\r'); continue }
            "`t" { [void]$builder.Append('\t'); continue }
        }
        if ($code -lt 32) {
            [void]$builder.Append(('\u{0:x4}' -f $code))
        } else {
            [void]$builder.Append($char)
        }
    }
    [void]$builder.Append('"')
    return $builder.ToString()
}

function ConvertTo-CanonicalJson {
    param([Parameter(Mandatory=$true)] $Value)

    if ($null -eq $Value) { throw "Null values are not allowed in canonical approval packet JSON." }

    if ($Value -is [bool]) {
        if ($Value) { return "true" }
        return "false"
    }

    if ($Value -is [string]) {
        return ConvertTo-JsonStringLiteral -Value $Value
    }

    if ($Value -is [int] -or $Value -is [long] -or $Value -is [decimal] -or $Value -is [double]) {
        return [System.Convert]::ToString($Value, [System.Globalization.CultureInfo]::InvariantCulture)
    }

    if ($Value -is [System.Collections.IEnumerable] -and -not ($Value -is [string]) -and -not ($Value -is [System.Collections.IDictionary]) -and -not ($Value -is [pscustomobject])) {
        $items = @()
        foreach ($item in $Value) { $items += (ConvertTo-CanonicalJson -Value $item) }
        return "[" + ($items -join ",") + "]"
    }

    $properties = @()
    if ($Value -is [System.Collections.IDictionary]) {
        foreach ($key in $Value.Keys) {
            $properties += [pscustomobject]@{ Name = [string]$key; Value = $Value[$key] }
        }
    } else {
        foreach ($property in $Value.PSObject.Properties) {
            $properties += [pscustomobject]@{ Name = [string]$property.Name; Value = $property.Value }
        }
    }

    $parts = @()
    foreach ($property in ($properties | Sort-Object Name)) {
        $parts += ((ConvertTo-JsonStringLiteral -Value $property.Name) + ":" + (ConvertTo-CanonicalJson -Value $property.Value))
    }
    return "{" + ($parts -join ",") + "}"
}

function Test-ApprovalPacketSchema {
    param([Parameter(Mandatory=$true)] $Packet)

    $required = @(
        "schemaVersion",
        "packetId",
        "environment",
        "runtimeTargetCommit",
        "candidateImageTag",
        "candidateImageId",
        "candidateImageDigest",
        "artifactPublished",
        "targetImageDecision",
        "executionScope",
        "executionScopeHash",
        "portalIncluded",
        "commonDbIncluded",
        "productionDataChangesApproved",
        "approvedExternalDependencies",
        "deploymentStrategy",
        "rollbackMechanism",
        "rollbackTargetImmutable",
        "rollbackArtifactId",
        "rollbackArtifactPublished",
        "monitoringReady",
        "sbomAvailable",
        "officialImageScannerAvailable",
        "residualRiskIds",
        "configurationManifestVersion",
        "runbookVersion",
        "rollbackPlanVersion",
        "monitoringPlanVersion",
        "testPlanVersion"
    )
    foreach ($field in $required) {
        if (-not ($Packet.PSObject.Properties.Name -contains $field)) { throw "Missing required canonical approval packet field: $field" }
    }

    $forbidden = @(
        "timestamp",
        "generatedAt",
        "validatedAt",
        "machineName",
        "absolutePath",
        "workingDirectory",
        "containerId",
        "currentContainerId",
        "currentProcessId",
        "dockerRuntimeTimestamp",
        "lastHealthCheckTimestamp",
        "gitBranchTemporaryName",
        "prNumber",
        "reviewerName",
        "humanApprovalTimestamp"
    )
    foreach ($field in $forbidden) {
        if ($Packet.PSObject.Properties.Name -contains $field) { throw "Forbidden dynamic field in canonical approval packet: $field" }
    }

    if ($Packet.environment -ne "Production") { throw "Canonical approval packet environment must be Production." }
    if ($Packet.packetId -ne "CRM-S10-P44F-PACKET-V3") { throw "Canonical approval packet id mismatch." }
    if ($Packet.runtimeTargetCommit -ne "8623c6191f5b59397d1243d2e0f8b30ee5caae6c") { throw "Runtime target commit mismatch." }
    if ($Packet.candidateImageId -ne "sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37") { throw "Candidate image id mismatch." }
    if ($Packet.portalIncluded -ne $false) { throw "Portal must remain excluded." }
    if ($Packet.commonDbIncluded -ne $false) { throw "Common DB must remain excluded." }
    if ($Packet.productionDataChangesApproved -ne $false) { throw "Production data changes must remain excluded." }
}

function Get-ApprovalPacketHash {
    param([Parameter(Mandatory=$true)] [string] $Path)

    $packet = Get-Content -Raw -LiteralPath $Path | ConvertFrom-Json
    Test-ApprovalPacketSchema -Packet $packet
    $canonical = ConvertTo-CanonicalJson -Value $packet
    $bytes = [System.Text.Encoding]::UTF8.GetBytes($canonical)
    $sha = [System.Security.Cryptography.SHA256]::Create()
    $hash = $sha.ComputeHash($bytes)
    [pscustomobject]@{
        Path = $Path
        CanonicalizationVersion = "crm-approval-packet-canonical-json-v1"
        CanonicalJson = $canonical
        HashAlgorithm = "SHA-256"
        Hash = (([System.BitConverter]::ToString($hash) -replace "-", "").ToLowerInvariant())
    }
}

if ($MyInvocation.InvocationName -ne ".") {
    $result = Get-ApprovalPacketHash -Path $Path
    Write-Output $result.Hash
}
