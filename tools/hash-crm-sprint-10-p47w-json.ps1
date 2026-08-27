param([Parameter(Mandatory=$true)] [string] $Path)

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

    if ($null -eq $Value) { throw "Null values are not allowed in canonical P47W JSON." }
    if ($Value -is [bool]) { if ($Value) { return "true" }; return "false" }
    if ($Value -is [string]) { return ConvertTo-JsonStringLiteral -Value $Value }
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
        foreach ($key in $Value.Keys) { $properties += [pscustomobject]@{ Name = [string]$key; Value = $Value[$key] } }
    } else {
        foreach ($property in $Value.PSObject.Properties) { $properties += [pscustomobject]@{ Name = [string]$property.Name; Value = $property.Value } }
    }

    $parts = @()
    foreach ($property in ($properties | Sort-Object Name)) {
        $parts += ((ConvertTo-JsonStringLiteral -Value $property.Name) + ":" + (ConvertTo-CanonicalJson -Value $property.Value))
    }
    return "{" + ($parts -join ",") + "}"
}

$json = Get-Content -Raw -LiteralPath $Path | ConvertFrom-Json
$canonical = ConvertTo-CanonicalJson -Value $json
$bytes = [System.Text.Encoding]::UTF8.GetBytes($canonical)
$sha = [System.Security.Cryptography.SHA256]::Create()
$hash = $sha.ComputeHash($bytes)
Write-Output (([System.BitConverter]::ToString($hash) -replace "-", "").ToLowerInvariant())
