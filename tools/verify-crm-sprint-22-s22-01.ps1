$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$policy=Get-Content (Join-Path $root 'src\CRM.Domain\DocumentMetadata\DocumentMetadataPolicy.cs') -Raw
$tests=Get-Content (Join-Path $root 'tests\CRM.UnitTests\DocumentMetadataPolicyTests.cs') -Raw
foreach($token in @('MaxFileReferenceIdLength = 300','MaxFileNameLength = 255','MaxContentTypeLength = 120','MaxDescriptionLength = 1000','ArchivedDocumentCannotBeModified')){if(-not $policy.Contains($token)){throw "Missing policy token: $token"}}
foreach($token in @('Archive_Repeated_IsIdempotent','Update_Archived_IsRejected','Create_RejectsLongFileName')){if(-not $tests.Contains($token)){throw "Missing test token: $token"}}
Write-Host 'CRM Sprint 22 S22-01 verification passed.'
