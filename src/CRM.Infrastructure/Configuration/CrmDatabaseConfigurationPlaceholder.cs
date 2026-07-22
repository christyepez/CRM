namespace CRM.Infrastructure.Configuration;

public sealed record CrmDatabaseConfigurationPlaceholderStatus(
    bool Configured,
    bool RuntimeConnected,
    string LogicalDatabaseName,
    bool LogicalDatabaseNameIsPlaceholder,
    string Warning);

public sealed class CrmDatabaseConfigurationPlaceholder
{
    public const string WarningText = "Database configuration placeholder only; no connection values are read";

    public CrmDatabaseConfigurationPlaceholderStatus GetStatus() =>
        new(false, false, "CrmDb", true, WarningText);
}
