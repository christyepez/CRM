# CRM Runtime Probe Activation Checklist

Before any future runtime probe activation:

- [ ] Formal release approval exists.
- [ ] Secret provider runtime contract is validated.
- [ ] Synthetic data is approved.
- [ ] Rollback plan is tested.
- [ ] Observability is ready.
- [ ] Logs redact secrets, tokens, connection strings and personal data.
- [ ] Health checks are green.
- [ ] Negative route checks prove `/api/crm/leads`, `/api/crm/accounts` and `/api/crm/contacts` are not active.
- [ ] DELETE remains `NoGo`.
- [ ] No real activation is approved.

Sprint 5 P1 satisfies none of the approval flags; it only defines this checklist.
