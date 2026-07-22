# CRM Portal User Tenant Context Contract

Expected mapping:

| Portal source | CRM target | P4 status |
| --- | --- | --- |
| Portal user id | `CrmUserContext.UserId` | ContractOnly |
| Portal tenant id | `CrmUserContext.TenantId` | ContractOnly |
| Portal permissions | `CrmPolicyResult.RequiredCapability` | ContractOnly |
| Portal claims | `CrmUserContext.NormalizedClaims` | ContractOnly |
| Correlation id | `CrmCorrelationContext.CorrelationId` | ContractOnly |

CRM must not read real tokens or infer tenant authority locally. The tenant and user context must arrive from Portal/Gateway runtime once approved.
