namespace CRM.Infrastructure.Persistence.EfPrototype;

public sealed record CrmEfPrototypeOptions(
    bool EfPrototypeExists,
    bool EfRuntimeEnabled,
    bool DbContextRuntimeActive,
    bool MigrationsEnabled,
    bool DurablePersistenceEnabled,
    bool ProductiveCrudEnabled)
{
    public static CrmEfPrototypeOptions Disabled { get; } = new(
        true,
        false,
        false,
        false,
        false,
        false);
}
