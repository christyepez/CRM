# CRM Power BI Readiness Checklist

Before real BI activation:

- Portal authorization for reports is approved.
- BI platform workspace, reports and datasets are managed outside the repository.
- Embed tokens are issued by an approved backend service, never stored in CRM.
- Dataset refresh and lineage are documented.
- NonProduction validation passes with mock or sandbox data.
- No workspace IDs, report IDs, dataset IDs, embed URLs or secrets are committed.

Current P7 result: contracts only; no analytics runtime configured.
