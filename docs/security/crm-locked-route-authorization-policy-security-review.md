# CRM Locked Route Authorization Policy Security Review

Security decision: approved for metadata-only NonProduction locked route evaluation.

Controls:

- Fail closed by default.
- Productive routes are 404 by default.
- Locked routes are 423 only behind explicit NonProduction flag.
- Policy evaluator is pure application logic.
- No request token/header reads.
- No Portal HTTP calls by default.
- No auth middleware or `[Authorize]` productive activation.
- No DB, EF runtime, migrations, or connection strings.
- No role/permission persistence in CRM.
- DELETE remains NoGo.

Productization remains NotReady until Sprint8P6Sprint8GateDecision.
