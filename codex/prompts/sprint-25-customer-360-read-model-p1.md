# Sprint 25 P1 - Customer 360 Read Model Functional Baseline

Define a read-only synthetic Customer 360 foundation capability over existing CRM concepts.

Constraints:
- Do not create a duplicate Customer CRUD aggregate; Account remains the organization/customer container.
- Read model may summarize structural references to Account, Contact, Lead, Opportunity, Case, Interaction, Note, Document, Tag and Assignment foundation data.
- No cross-entity mutations.
- No productive routes, DELETE, Common DB, Portal runtime, external connectors or real data.
- Deterministic synthetic foundation data only.

Backlog:
- S25-01 contracts/read-model rules.
- S25-02 application source/service.
- S25-03 foundation API.
- S25-04 Angular read-only page.
- S25-05 hardening.
- S25-06 local integration.
- S25-07 closure.
