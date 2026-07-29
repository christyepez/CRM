# CRM Locked Route Authorization Policy Boundary

PortalCorporativo remains owner of Auth, AuthZ, Menu, Permissions, Audit, and runtime identity contracts.

CRM P5 only owns:

- Pure application policy evaluator.
- Locked route metadata contract.
- Foundation endpoint reporting the gate status.
- NonProduction-only locked route response metadata when explicitly enabled.

CRM P5 does not own:

- Login/logout.
- Identity provider.
- Roles or permissions storage.
- Token validation.
- Portal HTTP runtime.
- Productive CRUD authorization.
- Productive route activation.
- DELETE behavior.

Future productive activation requires Portal owner approval, real permission policy approval, Common DB runtime approval, observability approval, rollback approval, and QA/security sign-off.
