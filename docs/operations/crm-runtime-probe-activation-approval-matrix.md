# CRM Runtime Probe Activation Approval Matrix

| Probe | Required owner | Approval status | Evidence required |
| --- | --- | --- | --- |
| Common DB probe | Data Architect + Release Manager | Not approved | Secret provider validated, synthetic data, rollback and observability. |
| Portal Auth probe | Security + Portal Integration | Not approved | No token storage, Portal contract approval, rollback and observability. |
| Productive route locked stubs | Architecture Governance + QA Lead | Not approved | Locked stubs only, no business execution, no DELETE, negative route checks. |

All approvals are non-production only. Real activation remains `NoGo`.
