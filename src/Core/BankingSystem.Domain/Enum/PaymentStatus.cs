using System;
namespace BankingSystem.Domain.Enum
{
	public enum PaymentStatus
	{
        Pending,
        Paid,
        Overdue,
        PartiallyPaid,
        Default,
        Refinanced,
        Forgiven
    }
}

