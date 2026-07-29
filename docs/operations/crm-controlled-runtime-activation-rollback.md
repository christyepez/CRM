# CRM Controlled Runtime Activation Rollback

P1 has no active runtime trial to roll back.

Future trial rollback minimum:
- Disable the explicit NonProduction flag.
- Confirm default fail-closed status.
- Re-run health and guardrail checks.
- Capture evidence in the trial PR.

Production rollback is out of scope because production activation is NoGo.
