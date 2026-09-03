# CRM Sprint 12 S12-01 - Contact Contracts and Domain Rules

## Summary

S12-01 makes Contact Management foundation rules explicit and deterministic without rebuilding the existing Contact foundation CRUD.

ContactDomainStatus: ExplicitRulesImplemented

ContactApplicationStatus: FoundationOnly

ContactPersistenceArchitecture: Foundation/NonProduction seam

ProductiveContactRouteEnabled: false

PortalRuntimeEnabled: false

CommonDbRuntimeEnabled: false

LeadContactRuntimeImplemented: false

S1201Decision: Implemented

## Existing Contact baseline

Existing Contact entity remains `src/CRM.Domain/Entities/ConceptualEntities.cs`.

Existing fields:

- `Id`
- `Name`
- `Email`
- `Phone`
- `Role`
- `AccountId`
- `PreferredContactMethod`
- `Status`

Existing statuses:

- `Draft`
- `Active`
- `Inactive`

Existing preferred contact methods:

- `NotSpecified`
- `Email`
- `Phone`

Existing foundation API routes remain unchanged under `/api/crm/foundation/contacts`.

## Domain rules implemented

S12-01 adds `ContactManagementPolicy` and deterministic contracts under `src/CRM.Domain/ContactManagement`.

Contracts:

- `ContactManagementCommand`
- `ContactManagementSnapshot`
- `ContactManagementRuleResult`
- `ContactManagementOperation`
- `ContactManagementErrorCode`

## Create rules

- Name is required.
- Name is trimmed.
- Name maximum length is 160.
- Email is optional unless preferred method is `Email`.
- Email is trimmed and lower-cased using the existing CRM email convention.
- Email maximum length is 254.
- Phone is optional unless preferred method is `Phone`.
- Phone is trimmed and validated by the existing `PhoneNumber` value object.
- Role is optional, trimmed and bounded to 80 characters.
- AccountId is optional.
- AccountId must be a non-empty GUID when present.
- Status is not accepted as a create input in S12-01.

ContactMethodRequired: false

## Update rules

- ContactId is required for update.
- ContactId must be a non-empty GUID.
- Existing contact snapshot is required.
- ContactId cannot change.
- Editable fields are Name, Email, Phone, Role, AccountId and PreferredContactMethod.
- Same-state updates return success with `Changed=false`.
- No status transition state machine is introduced.

## Preferred contact method rules

- `NotSpecified` does not require email or phone.
- `Email` requires a valid email.
- `Phone` requires a valid phone.
- Unsupported enum values are rejected.

## Normalization

NormalizationBehavior:

- Trim Name, Email, Phone, Role and AccountId.
- Lower-case Email only.
- Do not parse names.
- Do not apply Ecuador-specific phone rules.
- Do not perform destructive normalization.

## AccountId decision

AccountRelationshipRequiredForFoundation: false

Contact may reference Account through optional `AccountId`, but S12-01 does not require Account Management to create or update a Contact.

## Lead decision

LeadContactDecision: ContractOnlyLater

S12-01 does not implement Lead conversion, LeadId persistence or Lead-to-Contact runtime behavior.

## Error codes

- `None`
- `InvalidContactId`
- `NameRequired`
- `NameTooLong`
- `InvalidEmail`
- `EmailTooLong`
- `InvalidPhone`
- `PhoneTooLong`
- `RoleTooLong`
- `InvalidPreferredContactMethod`
- `PreferredContactMethodRequiresEmail`
- `PreferredContactMethodRequiresPhone`
- `InvalidAccountReferenceFormat`
- `ContactNotFound`
- `ValidationFailed`

## Rule matrix

| Operation | Field/Condition | Valid | NormalizedValue | Changed | ErrorCode |
| --- | --- | --- | --- | --- | --- |
| Create | Name empty | No | null | false | `NameRequired` |
| Create | Email malformed | No | lower-case attempted | false | `InvalidEmail` |
| Create | Preferred Email without Email | No | null email | false | `PreferredContactMethodRequiresEmail` |
| Create | Preferred Phone without Phone | No | null phone | false | `PreferredContactMethodRequiresPhone` |
| Create | AccountId absent | Yes | null | true | `None` |
| Create | AccountId invalid | No | trimmed value | false | `InvalidAccountReferenceFormat` |
| Update | ContactId differs from snapshot | No | trimmed id | false | `InvalidContactId` |
| Update | Same values after normalization | Yes | normalized fields | false | `None` |
| Update | Valid field modifications | Yes | normalized fields | true | `None` |

## Security

ContactSecurityReview: PASS

- Contact data is PII-like.
- S12-01 does not add Contact logging.
- S12-01 does not add secrets.
- S12-01 does not add API model binding changes.
- String inputs are bounded.
- Raw entity mass assignment is avoided by explicit command/result contracts.

## Out of scope

- Productive `/api/crm/contacts`.
- DELETE behavior.
- Lead conversion.
- Portal Auth runtime.
- Common DB runtime.
- EF, migrations or schema changes.
- Simulated Production deployment, rebuild, restart or rollback.
- Dedicated Angular Contact workflow.

## S12-02 entry criteria

- Contact contracts are stable.
- Domain rules are tested.
- Existing foundation CRUD remains compatible.
- Productive Contact routes remain locked.
- Portal/Common DB runtime remains absent.
- Guardrails and foundation verifiers pass.
