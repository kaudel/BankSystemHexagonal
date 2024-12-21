using BankingSystem.Domain.ValueObjects;

public record InterestRate
{
    private readonly decimal _rateValue;
    public decimal AsPercentaje => _rateValue * 100;

    private InterestRate(decimal rateValue)
    {
        if (rateValue < 0 || rateValue > 1)
            throw new ArgumentException("Rate value must be between 0 and 1", nameof(rateValue));

        _rateValue = rateValue;
    }

    public static InterestRate FromPercentage(decimal percentage) => new(percentage / 100);

    public Money CalculateInterest(Money principal, int months)
    {
        decimal interest = principal.Amount * _rateValue * (months / 12);
        return Money.Create(interest, principal.Currency);
    }
}