$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$service=Get-Content (Join-Path $root 'src\CRM.Application\DocumentMetadata\DocumentMetadataService.cs') -Raw
$contracts=Get-Content (Join-Path $root 'src\CRM.Application\DocumentMetadata\DocumentMetadataApplicationContracts.cs') -Raw
$program=Get-Content (Join-Path $root 'src\CRM.Api\Program.cs') -Raw
foreach($token in @('IDocumentMetadataFoundationStore','FoundationOnly','DocumentNotFound')){if(-not $service.Contains($token)){throw "Missing service token: $token"}}
foreach($token in @('BinaryStorageEnabled','PortalContentRuntimeEnabled','ProductiveCrudEnabled')){if(-not $contracts.Contains($token)){throw "Missing contract token: $token"}}
if(-not $program.Contains('IDocumentMetadataService, DocumentMetadataService')){throw 'Missing service DI'}
if($program.Contains('/api/crm/foundation/documents')){throw 'S22-02 must not expose document routes yet'}
Write-Host 'CRM Sprint 22 S22-02 verification passed.'
