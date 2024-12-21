namespace BankingSystem.Domain.ValueObjects
{
    public record LoanTerm
	{
		public int Months { get; }

		public LoanTerm(int months)
		{
			if (months <= 0)
				throw new ArgumentException("Loan term in months must be greater than 0", nameof(months));

			//30 years
			if (months > 360)
				throw new ArgumentException("Loan term in months must be less than 360 months", nameof(months));

			Months = months;

		}

		public static LoanTerm Create(int months) => new(months);

		public static LoanTerm OneYear => new(12);
        public static LoanTerm TwoYear => new(24);
        public static LoanTerm ThreeYear => new(36);
        public static LoanTerm FourYear => new(48);
        public static LoanTerm FiveYear => new(60);

		public (int Years, int MonthReminder) ToYearsAndMonths()
		{
			return (Months / 12, Months % 12);
		}

        public override string ToString()
        {
			var (years, months) = ToYearsAndMonths();

			if (years == 0)
				return $"{months} month ${(months > 1 ? "s" : "")}";

			if (months == 0)
				return $"{years} year ${(years > 1 ? "s" : "")}";

			return $"{years} year {(years > 1 ? "s" : "")} and month{(months>1?"s":"")}";
        }
    }
}

