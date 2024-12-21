namespace BankingSystem.Domain.ValueObjects
{
    public record LoanElegibilityResult
	{
		public bool IsElegible { get; }
		public IReadOnlyCollection<string> Reasons { get; }

		public LoanElegibilityResult( bool isElegible, IEnumerable<string> reasons)
		{
			IsElegible = isElegible;
			Reasons = reasons.ToList().AsReadOnly();
		}

		public static LoanElegibilityResult Approved() => new(true, Array.Empty<string>());

		public static LoanElegibilityResult Rejected(IEnumerable<string> reasons) => new(false, reasons);
	}
}

