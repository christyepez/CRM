using CRM.Application.Persistence;
using CRM.Application.Portal;
using CRM.Application.Ports.Persistence;

namespace CRM.Application.Foundation;

internal static class FoundationCrudSupport
{
    internal const string WarningText = "Foundation CRUD only; no productive endpoint or database configured";

    internal static string Clean(string? value, string fallback = "foundation-preview") =>
        string.IsNullOrWhiteSpace(value)
            ? fallback
            : new string(value.Trim().Where(character => !char.IsControl(character)).Take(120).ToArray());

    internal static string? CleanOptional(string? value) =>
        string.IsNullOrWhiteSpace(value)
            ? null
            : new string(value.Trim().Where(character => !char.IsControl(character)).Take(120).ToArray());

    internal static void EnsureSafeFlags(ICrmPersistenceFeatureFlagProvider flags)
    {
        var state = flags.GetFeatureFlags();
        if (!state.FoundationMode || !state.PersistenceSeamEnabled || state.ProductiveCrudEnabled || state.DurablePersistenceEnabled)
        {
            throw new InvalidOperationException("Foundation CRUD flags are not safe for NonProductionSeam.");
        }
    }

    internal static FoundationCrudOperationResult<T> Result<T>(
        string operation,
        string entity,
        T? data,
        string permission,
        CrmPortalPermissionCheckContract permissionSimulation) =>
        new(
            permissionSimulation.Allowed,
            operation,
            entity,
            permissionSimulation.Allowed ? data : default,
            permission,
            permissionSimulation,
            true,
            "NonProductionSeam",
            false,
            false,
            false,
            "FoundationSimulation",
            WarningText);

    internal static CrmFoundationPreviewItemContract ToPreview(string id, string entity, string displayName, string status, IReadOnlyDictionary<string, string> metadata) =>
        new(id, entity, displayName, status, DateTimeOffset.UtcNow, metadata);
}
