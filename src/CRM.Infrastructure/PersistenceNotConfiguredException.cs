namespace CRM.Infrastructure;

public sealed class PersistenceNotConfiguredException : InvalidOperationException
{
    public PersistenceNotConfiguredException()
        : base("CRM persistence is not configured in foundation runtime.")
    {
    }
}
