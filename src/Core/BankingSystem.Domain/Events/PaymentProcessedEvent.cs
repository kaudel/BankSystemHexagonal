using BankingSystem.Domain.Entities;
using BankingSystem.Domain.ValueObjects;

namespace BankingSystem.Domain.Events
{
    public class PaymentProcessedEvent:DomainEvent
	{
		public Guid LoanId { get; }
		public Guid PaymentId { get; }
		public Money Amount { get; }
		public DateTime PaymentDate { get; }


		public PaymentProcessedEvent(Payment payment)
		{
			LoanId = payment.LoanId;
			PaymentId = payment.Id;
			Amount = payment.PaidAmount ?? throw new InvalidOperationException("Payment amount is not set");
			PaymentDate = payment.PaymentDate ?? throw new InvalidOperationException("Payment date is not set");
		}
	}
}

