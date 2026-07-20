namespace CRM.Domain.ValueObjects;

public sealed record MoneyAmount
{
    public decimal Amount { get; }
    public string Currency { get; }

    private MoneyAmount(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static MoneyAmount From(decimal amount, string currency)
    {
        var normalizedCurrency = (currency ?? string.Empty).Trim().ToUpperInvariant();
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Money amount cannot be negative.");
        }

        if (normalizedCurrency.Length != 3)
        {
            throw new ArgumentException("Currency must be ISO-like 3 letters.", nameof(currency));
        }

        return new MoneyAmount(amount, normalizedCurrency);
    }
}

public sealed record Probability
{
    public int Percentage { get; }

    private Probability(int percentage) => Percentage = percentage;

    public static Probability From(int percentage) =>
        percentage is < 0 or > 100
            ? throw new ArgumentOutOfRangeException(nameof(percentage), "Probability must be between 0 and 100.")
            : new Probability(percentage);
}

public sealed record DateRange
{
    public DateOnly Start { get; }
    public DateOnly End { get; }

    private DateRange(DateOnly start, DateOnly end)
    {
        Start = start;
        End = end;
    }

    public static DateRange From(DateOnly start, DateOnly end) =>
        end < start
            ? throw new ArgumentException("Date range end cannot be before start.")
            : new DateRange(start, end);
}
