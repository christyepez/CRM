using CRM.Domain.Enums;

namespace CRM.Domain.OpportunityManagement;

public static class OpportunityPipelinePolicy
{
    public const int MaxAccountNameLength = 160;
    public const int MaxStageNameLength = 80;

    public static OpportunityPipelineRuleResult Evaluate(OpportunityPipelineCommand command)
    {
        var id = Normalize(command.OpportunityId);
        var accountName = Normalize(command.AccountName);
        var currency = Normalize(command.Currency)?.ToUpperInvariant();
        var pipelineId = Normalize(command.PipelineId);
        var stageId = Normalize(command.StageId);

        if (!Enum.IsDefined(command.Operation))
            return Reject(command, OpportunityPipelineErrorCode.InvalidOperation, "Opportunity operation is invalid.", id, accountName, currency, pipelineId, stageId);

        if (command.Operation != OpportunityPipelineOperation.Create)
        {
            if (!ValidId(id))
                return Reject(command, OpportunityPipelineErrorCode.InvalidOpportunityId, "Opportunity id is required.", id, accountName, currency, pipelineId, stageId);
            if (command.ExistingOpportunity is null)
                return Reject(command, OpportunityPipelineErrorCode.OpportunityNotFound, "Existing opportunity snapshot is required.", id, accountName, currency, pipelineId, stageId);
            if (!string.Equals(id, Normalize(command.ExistingOpportunity.OpportunityId), StringComparison.OrdinalIgnoreCase))
                return Reject(command, OpportunityPipelineErrorCode.InvalidOpportunityId, "Opportunity id cannot change.", id, accountName, currency, pipelineId, stageId);

            var terminal = EvaluateExistingTerminal(command, id);
            if (terminal is not null)
                return terminal;
        }

        if (command.Operation is OpportunityPipelineOperation.Win or OpportunityPipelineOperation.Lose or OpportunityPipelineOperation.Cancel)
            return TerminalTransition(command, id!);

        var validation = ValidateEditable(command, id, accountName, currency, pipelineId, stageId);
        if (validation is not null)
            return validation;

        if (command.Operation == OpportunityPipelineOperation.Progress)
        {
            var progression = ValidateProgression(command, stageId!);
            if (progression is not null)
                return Reject(command, progression.Value.Code, progression.Value.Message, id, accountName, currency, pipelineId, stageId);
        }

        var changed = command.Operation == OpportunityPipelineOperation.Create || HasChanged(command, accountName!, currency!, pipelineId!, stageId!);
        return Result(command, id, accountName, currency, pipelineId, stageId, command.Probability,
            OpportunityStatus.Open, true, changed, OpportunityPipelineErrorCode.None,
            changed ? "Opportunity pipeline operation is valid." : "Opportunity update has no changes.");
    }

    private static OpportunityPipelineRuleResult? ValidateEditable(OpportunityPipelineCommand c, string? id, string? name, string? currency, string? pipelineId, string? stageId)
    {
        if (string.IsNullOrWhiteSpace(name)) return Reject(c, OpportunityPipelineErrorCode.AccountNameRequired, "Account name is required.", id, name, currency, pipelineId, stageId);
        if (name.Length > MaxAccountNameLength) return Reject(c, OpportunityPipelineErrorCode.AccountNameTooLong, "Account name exceeds the allowed length.", id, name, currency, pipelineId, stageId);
        if (c.ExpectedValue < 0) return Reject(c, OpportunityPipelineErrorCode.InvalidExpectedValue, "Expected value cannot be negative.", id, name, currency, pipelineId, stageId);
        if (currency is null || currency.Length != 3 || !currency.All(char.IsLetter)) return Reject(c, OpportunityPipelineErrorCode.InvalidCurrency, "Currency must contain three letters.", id, name, currency, pipelineId, stageId);
        if (c.Probability is < 0 or > 100) return Reject(c, OpportunityPipelineErrorCode.InvalidProbability, "Probability must be between 0 and 100.", id, name, currency, pipelineId, stageId);
        if (!ValidId(pipelineId)) return Reject(c, OpportunityPipelineErrorCode.InvalidPipelineId, "Pipeline id is required.", id, name, currency, pipelineId, stageId);
        if (!ValidId(stageId)) return Reject(c, OpportunityPipelineErrorCode.InvalidStageId, "Stage id is required.", id, name, currency, pipelineId, stageId);

        var stageError = ValidateStages(c.Stages, stageId!);
        if (stageError is not null) return Reject(c, stageError.Value.Code, stageError.Value.Message, id, name, currency, pipelineId, stageId);

        foreach (var reference in new[] {
            (c.LeadId, OpportunityPipelineErrorCode.InvalidLeadId),
            (c.ContactId, OpportunityPipelineErrorCode.InvalidContactId),
            (c.AccountId, OpportunityPipelineErrorCode.InvalidAccountId),
            (c.ActivityId, OpportunityPipelineErrorCode.InvalidActivityId) })
        {
            if (Normalize(reference.Item1) is { } value && !ValidId(value))
                return Reject(c, reference.Item2, "Synthetic relationship id format is invalid.", id, name, currency, pipelineId, stageId);
        }
        return null;
    }

    private static (OpportunityPipelineErrorCode Code, string Message)? ValidateStages(IReadOnlyCollection<OpportunityPipelineStageDefinition>? stages, string stageId)
    {
        if (stages is null || stages.Count == 0) return (OpportunityPipelineErrorCode.PipelineStagesRequired, "Pipeline stages are required.");
        var normalized = stages.Select(s => new { Id = Normalize(s.StageId), Name = Normalize(s.Name), s.Order }).ToArray();
        if (normalized.Any(s => !ValidId(s.Id) || s.Name is null || s.Name.Length > MaxStageNameLength || s.Order <= 0))
            return (OpportunityPipelineErrorCode.InvalidStageOrder, "Every stage requires a valid id, bounded name and positive order.");
        if (normalized.GroupBy(s => s.Order).Any(g => g.Count() > 1))
            return (OpportunityPipelineErrorCode.DuplicateStageOrder, "Pipeline stage order values must be unique.");
        if (normalized.GroupBy(s => s.Name!, StringComparer.OrdinalIgnoreCase).Any(g => g.Count() > 1))
            return (OpportunityPipelineErrorCode.DuplicateStageName, "Pipeline stage names must be unique.");
        if (!normalized.Any(s => string.Equals(s.Id, stageId, StringComparison.OrdinalIgnoreCase)))
            return (OpportunityPipelineErrorCode.CurrentStageNotInPipeline, "Current stage must belong to the pipeline.");
        return null;
    }

    private static (OpportunityPipelineErrorCode Code, string Message)? ValidateProgression(OpportunityPipelineCommand c, string nextStageId)
    {
        var current = Normalize(c.ExistingOpportunity!.StageId);
        var ordered = c.Stages!.OrderBy(s => s.Order).ToArray();
        var currentIndex = Array.FindIndex(ordered, s => string.Equals(Normalize(s.StageId), current, StringComparison.OrdinalIgnoreCase));
        var nextIndex = Array.FindIndex(ordered, s => string.Equals(Normalize(s.StageId), nextStageId, StringComparison.OrdinalIgnoreCase));
        if (currentIndex < 0 || nextIndex != currentIndex + 1)
            return (OpportunityPipelineErrorCode.InvalidStageProgression, "Opportunity can progress only to the next ordered pipeline stage.");
        return null;
    }

    private static OpportunityPipelineRuleResult? EvaluateExistingTerminal(OpportunityPipelineCommand c, string? id)
    {
        var e = c.ExistingOpportunity!;
        if (e.Status == OpportunityStatus.Open) return null;
        var repeat = (e.Status == OpportunityStatus.Won && c.Operation == OpportunityPipelineOperation.Win)
            || (e.Status == OpportunityStatus.Lost && c.Operation == OpportunityPipelineOperation.Lose)
            || (e.Status == OpportunityStatus.Cancelled && c.Operation == OpportunityPipelineOperation.Cancel);
        if (repeat)
            return Result(c, id, Normalize(e.AccountName), e.Currency, e.PipelineId, e.StageId, e.Probability, e.Status, true, false, OpportunityPipelineErrorCode.None, "Opportunity is already in the requested terminal state.");

        var code = e.Status switch
        {
            OpportunityStatus.Won when c.Operation == OpportunityPipelineOperation.Lose => OpportunityPipelineErrorCode.WonOpportunityCannotBeLost,
            OpportunityStatus.Lost when c.Operation == OpportunityPipelineOperation.Win => OpportunityPipelineErrorCode.LostOpportunityCannotBeWon,
            OpportunityStatus.Cancelled => OpportunityPipelineErrorCode.CancelledOpportunityCannotBeChanged,
            _ => OpportunityPipelineErrorCode.TerminalOpportunityCannotBeModified
        };
        return Reject(c, code, "Terminal opportunities cannot be modified.", id, e.AccountName, e.Currency, e.PipelineId, e.StageId, e.Status);
    }

    private static OpportunityPipelineRuleResult TerminalTransition(OpportunityPipelineCommand c, string id)
    {
        var e = c.ExistingOpportunity!;
        var status = c.Operation switch { OpportunityPipelineOperation.Win => OpportunityStatus.Won, OpportunityPipelineOperation.Lose => OpportunityStatus.Lost, _ => OpportunityStatus.Cancelled };
        return Result(c, id, Normalize(e.AccountName), e.Currency, e.PipelineId, e.StageId,
            status == OpportunityStatus.Won ? 100 : e.Probability, status, true, true, OpportunityPipelineErrorCode.None, "Opportunity terminal transition is valid.");
    }

    private static bool HasChanged(OpportunityPipelineCommand c, string name, string currency, string pipelineId, string stageId)
    {
        var e = c.ExistingOpportunity!;
        return !string.Equals(name, Normalize(e.AccountName), StringComparison.Ordinal)
            || c.ExpectedValue != e.ExpectedValue || !string.Equals(currency, e.Currency, StringComparison.OrdinalIgnoreCase)
            || c.Probability != e.Probability || !string.Equals(pipelineId, e.PipelineId, StringComparison.OrdinalIgnoreCase)
            || !string.Equals(stageId, e.StageId, StringComparison.OrdinalIgnoreCase)
            || !Same(c.LeadId, e.LeadId) || !Same(c.ContactId, e.ContactId) || !Same(c.AccountId, e.AccountId) || !Same(c.ActivityId, e.ActivityId);
    }

    private static bool Same(string? a, string? b) => string.Equals(Normalize(a), Normalize(b), StringComparison.OrdinalIgnoreCase);
    private static OpportunityPipelineRuleResult Reject(OpportunityPipelineCommand c, OpportunityPipelineErrorCode code, string message, string? id, string? name, string? currency, string? pipelineId, string? stageId, OpportunityStatus status = OpportunityStatus.Open) => Result(c, id, name, currency, pipelineId, stageId, c.Probability, status, false, false, code, message);
    private static OpportunityPipelineRuleResult Result(OpportunityPipelineCommand c, string? id, string? name, string? currency, string? pipelineId, string? stageId, int probability, OpportunityStatus status, bool allowed, bool changed, OpportunityPipelineErrorCode code, string message) => new(id, c.Operation, allowed, changed, code, message, name, c.ExpectedValue, currency, probability, pipelineId, stageId, status, Normalize(c.LeadId), Normalize(c.ContactId), Normalize(c.AccountId), Normalize(c.ActivityId));
    private static string? Normalize(string? value) { var v = (value ?? string.Empty).Trim(); return v.Length == 0 ? null : v; }
    private static bool ValidId(string? value) => Guid.TryParse(value, out var id) && id != Guid.Empty;
}
