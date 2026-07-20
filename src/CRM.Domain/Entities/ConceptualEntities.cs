using CRM.Domain.Common;
using CRM.Domain.ValueObjects;

namespace CRM.Domain.Entities;

public sealed record Account(CrmId Id, CompanyName Name);

public sealed record Contact(CrmId Id, PersonName Name, EmailAddress Email, PhoneNumber? Phone);

public sealed record Pipeline(CrmId Id, string Name, IReadOnlyCollection<PipelineStage> Stages);

public sealed record PipelineStage(CrmId Id, string Name, int Order);

public sealed record Note(CrmId Id, CrmId RelatedEntityId, string Text);

public sealed record Campaign(CrmId Id, string Name, DateRange ActiveRange);

public sealed record Segment(CrmId Id, string Name, string CriteriaSummary);
