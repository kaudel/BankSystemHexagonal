using BankingSystem.Domain.Enum;
using BankingSystem.Domain.Exceptions;
using BankingSystem.Domain.ValueObjects;

namespace BankingSystem.Domain.Entities
{
    public class Payment : Entity
	{
		public Guid LoanId { get; private set; }
		public Loan Loan { get; private set; } = null;
		public int PaymentNumber { get; private set; }
		public DateTime DueDate { get; private set; }
		public Money Principal { get; private set; }
		public Money Interest { get; private set; }
		public Money TotalAmount => Principal + Interest;
		public PaymentStatus Status { get; private set; }
		public DateTime? PaymentDate { get; private set; }
		public Money? PaymentAmount { get; private set; }
		public Money? PaidAmount { get; private set; }

		private Payment()
		{
		}

		private Payment(Loan loan,
			int paymentNumber,
			DateTime dueDate,
			Money principal,
			Money interest)
		{
			if (paymentNumber <= 0)
				throw new DomainException("Payment number must be greater than zero");

			if (dueDate < DateTime.UtcNow)
				throw new DomainException("Due date must be in the future");

			LoanId = loan.Id;
			Loan = loan;
			PaymentNumber = paymentNumber;
			DueDate = dueDate;
			Principal = principal;
			Interest = interest;
			Status = PaymentStatus.Pending;
		}

		public static Payment Create(
			Loan loan,
			int paymentNumber,
			DateTime dueDate,
			Money principal,
			Money interest
			)
		{
			return new Payment(loan, paymentNumber, dueDate, principal, interest);
		}

		public void ProcessPayment(Money amount, DateTime paymentDate)
		{
			if (Status != PaymentStatus.Pending)
				throw new DomainException("Payment is not pending");

			if (amount.Currency != TotalAmount.Currency)
				throw new DomainException("Payment currency must match total amount currency");

			if (amount.Amount < TotalAmount.Amount)
				throw new DomainException("Payment amount must be greater than or equals to total amount");

			Status = PaymentStatus.Paid;
			PaymentDate = paymentDate;
			PaidAmount = amount;

			UpdateModifiedDate();
			
		}

		public void MarkAsOverdue()
		{
			if (Status != PaymentStatus.Pending)
				throw new DomainException("Payment is not pending, Just mark as overdue payments");

			if (DateTime.UtcNow <= DueDate)
				throw new DomainException("Payment is not overdue");

			Status = PaymentStatus.Overdue;
			UpdateModifiedDate();
		}
	}
}

