namespace BankingSystem.Domain.ValueObjects
{
    public record Money
    {
        public decimal Amount { get; init; }
        public string Currency { get; init; }
        private Money(decimal amount, string currency)
        {
            if (Amount < 0)
                throw new ArgumentException("Amount cannot be negative", nameof(amount));

            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency cannot be empty", nameof(currency));

            Amount = amount;
            Currency = currency;
        }

        public static Money Create(decimal amount, string currency) => new(amount, currency);

        public static Money operator +(Money left, Money right)
        {
            if (left.Currency != right.Currency)
                throw new InvalidOperationException("Cannotadd money with different currencies");

            return new Money(left.Amount + right.Amount, left.Currency);
        }

        public override string ToString() => $"{Amount}{Currency}";
    }
}