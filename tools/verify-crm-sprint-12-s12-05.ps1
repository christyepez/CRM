$ErrorActionPreference = "Stop"

$requiredFiles = @(
    "src/CRM.Domain/ContactManagement/ContactManagementPolicy.cs",
    "src/CRM.Application/ContactManagement/IContactManagementService.cs",
    "src/CRM.Application/ContactManagement/ContactManagementService.cs",
    "src/CRM.Application/Foundation/FoundationContactCrudContracts.cs",
    "src/CRM.Api/Foundation/ContactManagementApiContracts.cs",
    "src/CRM.Api/Program.cs",
    "frontend/crm-web/src/main.ts",
    "frontend/crm-web/src/styles.css",
    "frontend/crm-web/tools/verify-crm-foundation.mjs",
    "docs/roadmap/crm-sprint-12-s12-01-contact-contracts-domain-rules.md",
    "docs/roadmap/crm-sprint-12-s12-02-contact-application-service.md",
    "docs/roadmap/crm-sprint-12-s12-03-contact-foundation-api-integration.md",
    "docs/roadmap/crm-sprint-12-s12-04-contact-management-frontend-foundation.md",
    "docs/roadmap/crm-sprint-12-s12-05-contact-management-test-guardrail-hardening.md",
    "codex/prompts/sprint-12-contact-management-s12-06.md"
)

foreach ($file in $requiredFiles) {
    if (-not (Test-Path $file)) {
        throw "Missing Sprint 12 S12-05 artifact or prerequisite: $file"
    }
}

$domain = Get-Content "src/CRM.Domain/ContactManagement/ContactManagementPolicy.cs" -Raw
$application = (Get-ChildItem "src/CRM.Application/ContactManagement" -Filter "*.cs" | ForEach-Object { Get-Content $_.FullName -Raw }) -join "`n"
$apiContracts = Get-Content "src/CRM.Api/Foundation/ContactManagementApiContracts.cs" -Raw
$program = Get-Content "src/CRM.Api/Program.cs" -Raw
$frontend = Get-Content "frontend/crm-web/src/main.ts" -Raw
$frontendVerifier = Get-Content "frontend/crm-web/tools/verify-crm-foundation.mjs" -Raw
$doc = Get-Content "docs/roadmap/crm-sprint-12-s12-05-contact-management-test-guardrail-hardening.md" -Raw
$nextTask = Get-Content "codex/next-task.md" -Raw
$tasks = Get-Content "codex/TASKS.md" -Raw

foreach ($marker in @(
    "MaxNameLength = 160",
    "MaxEmailLength = 254",
    "MaxPhoneLength = 24",
    "MaxRoleLength = 80",
    "InvalidPreferredContactMethod",
    "PreferredContactMethodRequiresEmail",
    "PreferredContactMethodRequiresPhone",
    "InvalidAccountReferenceFormat")) {
    if (-not $domain.Contains($marker)) {
        throw "Contact domain hardening marker missing: $marker"
    }
}

foreach ($marker in @("IContactFoundationStore", "ContactManagementPolicy", "SavePreviewAsync", "GetPreviewByIdAsync", "cancellationToken")) {
    if (-not $application.Contains($marker)) {
        throw "Contact application guardrail marker missing: $marker"
    }
}

foreach ($marker in @("FoundationContactCreateRequest", "FoundationContactUpdateRequest", "ContactManagementCreateApplicationRequest", "ContactManagementUpdateApplicationRequest", "BuildName", "ToStatusCode")) {
    if (-not $apiContracts.Contains($marker)) {
        throw "Contact API contract marker missing: $marker"
    }
}

foreach ($marker in @(
    'MapGet("/api/crm/foundation/contacts"',
    'MapGet("/api/crm/foundation/contacts/{id}"',
    'MapPost("/api/crm/foundation/contacts"',
    'MapPut("/api/crm/foundation/contacts/{id}"',
    'MapGet("/api/crm/foundation/contacts/read-model-preview"')) {
    if (-not $program.Contains($marker)) {
        throw "Foundation Contact API route missing: $marker"
    }
}

$programWithoutFoundation = $program.Replace('"/api/crm/foundation/contacts"', '""').Replace('"/api/crm/foundation/contacts/{id}"', '""').Replace('"/api/crm/foundation/contacts/preview"', '""').Replace('"/api/crm/foundation/contacts/read-model-preview"', '""')
foreach ($forbidden in @('"/api/crm/contacts"', 'MapGet("/api/crm/contacts', 'MapPost("/api/crm/contacts', 'MapPut("/api/crm/contacts', 'MapDelete("/api/crm/contacts', "DeleteContact", "ConvertLeadToContact", "CreateContactFromLead")) {
    if ($programWithoutFoundation.Contains($forbidden)) {
        throw "Forbidden productive Contact API/runtime marker detected: $forbidden"
    }
}

$contactSourceStart = $frontend.IndexOf("type PreferredContactMethod")
$contactSourceEnd = $frontend.IndexOf("selector: 'crm-home'")
if ($contactSourceStart -lt 0 -or $contactSourceEnd -le $contactSourceStart) {
    throw "Unable to isolate Contact frontend source."
}

$contactSource = $frontend.Substring($contactSourceStart, $contactSourceEnd - $contactSourceStart)
foreach ($marker in @(
    "type PreferredContactMethod = 'NotSpecified' | 'Email' | 'Phone'",
    "/api/crm/foundation/contacts",
    "if (this.contactForm.invalid || this.isSubmitting())",
    "Validators.maxLength(160)",
    "Validators.email",
    "Validators.maxLength(254)",
    "Validators.maxLength(24)",
    "Validators.maxLength(80)",
    "No changes were necessary",
    "Validation issue",
    "Contact not found",
    "Contact workflow unavailable",
    "aria-live",
    "<label for=")) {
    if (-not $contactSource.Contains($marker)) {
        throw "Contact frontend hardening marker missing: $marker"
    }
}

foreach ($forbidden in @("/api/crm/contacts", "innerHTML", "bypassSecurityTrustHtml", "document.querySelector", "localStorage", "sessionStorage", "access_token", "refresh_token", "Bearer ", "Authorization", "DeleteContact", "deleteContact", "ConvertLeadToContact", "CreateContactFromLead")) {
    if ($contactSource.Contains($forbidden)) {
        throw "Forbidden Contact frontend/runtime marker detected: $forbidden"
    }
}

foreach ($marker in @("S1205Decision: Implemented", "ContactManagementCoverageMatrix: Complete", "CrossLayerScenarioMatrix: Complete", "ProductiveContactRouteAvailable: false", "DeleteBehaviorAdded: false", "LeadContactRuntimeImplemented: false", "MassAssignmentRisk: Controlled", "PiiLoggingDetected: false", "ScopedSecretScan: PASS", "RealDataDetected: false", "XssReview: PASS", "AccessibilityValidation: PASS", "ResponsiveValidation: PASS", "SimulatedProductionTouched: false")) {
    if (-not $doc.Contains($marker)) {
        throw "S12-05 roadmap document missing marker: $marker"
    }
}

foreach ($marker in @("CRM Sprint 12 S12-06 - Contact Management Local Integration Validation", "codex/prompts/sprint-12-contact-management-s12-06.md", "S12-05 merge commit required")) {
    if (-not $nextTask.Contains($marker)) {
        throw "codex/next-task.md must point to S12-06 and avoid invented SHA: $marker"
    }
}

if (-not $tasks.Contains("S1205Decision: Implemented")) {
    throw "codex/TASKS.md must record S12-05 implementation."
}

foreach ($marker in @("sprint12ContactHardeningMarkers", "PreferredContactMethod frontend enum must contain exactly NotSpecified, Email and Phone")) {
    if (-not $frontendVerifier.Contains($marker)) {
        throw "Frontend verifier missing S12-05 marker: $marker"
    }
}

Write-Host "CRM Sprint 12 S12-05 verification passed."
