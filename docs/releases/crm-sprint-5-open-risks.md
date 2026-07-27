# CRM Sprint 5 Open Risks

- Sprint 6 preparation could be mistaken for real activation.
- Secret Provider mock activation must avoid real secret reads.
- Common DB dry-run must not open runtime connections until explicitly approved.
- Portal Auth token propagation must not read real tokens or headers before approval.
- Locked stub runtime trial must preserve 404 defaults and avoid domain execution.
- Productive UI and DELETE remain blocked.
