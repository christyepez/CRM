# CRM Controlled Runtime Pilot First Slice NonProduction Activation Scaffold Risk Register

| Risk | Control | Status |
| --- | --- | --- |
| Scaffold accidentally activates Portal | Disabled service returns no-op locked result | Controlled |
| Flags drift to true | Tests and guardrails verify false flags | Controlled |
| Private URL committed | Logical placeholders only | Controlled |
| Runtime route mistaken for productive route | Foundation/status GET only | Controlled |
| Cross-domain persistence introduced | No DB runtime, tables or migrations | Controlled |
