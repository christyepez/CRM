namespace CRM.Infrastructure.Persistence.EfPrototype;

public sealed class CrmDbContextPrototype
{
    public const string Warning = "CrmDbContextPrototype placeholder only; does not inherit EF runtime context and is not registered for runtime";

    public CrmEfPrototypeOptions Options { get; } = CrmEfPrototypeOptions.Disabled;
}
