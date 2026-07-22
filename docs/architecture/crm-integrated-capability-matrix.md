# CRM Integrated Capability Matrix

| Capability | Owner | Status | Runtime integration | Data persistence | Introduced | Next activation gate | Risks |
| --- | --- | --- | --- | --- | --- | --- | --- |
| CRM Domain | CRM | Foundation | None | None | P2 | Persistence review | Premature CRUD |
| CRM Application | CRM | Foundation | None | None | P1-P8 | Service boundary review | Layer coupling |
| CRM API | CRM | Foundation endpoints | None | None | P2-P8 | API productization gate | Productive endpoints too early |
| CRM Frontend Shell | CRM | Readiness only | None | None | P1-P8 | UI module shell review | Login/token duplication |
| Portal Security/Auth | PortalCorporativo | Planned | NotConnected | External | P5 | Portal adapter approval | Auth duplication |
| Portal Menu | PortalCorporativo | Planned | NotConnected | External | P5 | Menu registration approval | Menu duplication |
| Portal Permissions | PortalCorporativo | Planned | NotConnected | External | P5 | Permission adapter approval | Split ownership |
| Portal Audit | PortalCorporativo | Planned | NotConnected | External | P5 | Audit contract approval | Shadow audit |
| Portal Notification | PortalCorporativo | Planned | NotConnected | External | P5 | Notification adapter approval | Shadow notification |
| Portal Configuration | PortalCorporativo | Planned | NotConnected | External | P5 | Config contract approval | Hardcoded config |
| Portal Gateway | PortalCorporativo | Planned | NotConnected | External | P5 | Gateway routing approval | Gateway duplication |
| Financiero Customer Reference | Financiero | Planned | NotConnected | External | P6 | API/event contract approval | Direct coupling |
| Financiero Account Status | Financiero | Planned | NotConnected | External | P6 | API/event contract approval | Shared DB |
| Financiero Invoice/Payment Signals | Financiero | Planned | NotConnected | External | P6 | Event contract approval | Tax domain leakage |
| Reporting/BI KPI Catalog | CRM/BI | FoundationMock | None | None | P7 | Reporting governance | Fake data mistaken as real |
| Reporting/BI Dashboards | CRM/BI | FoundationMock | None | None | P7 | BI platform approval | Embed/token leakage |
| Reporting/BI Analytics Read Models | CRM/BI | FoundationMock | None | None | P7 | Read-model design review | Premature ETL |
