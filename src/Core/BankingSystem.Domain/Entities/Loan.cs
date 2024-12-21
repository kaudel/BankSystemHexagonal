using BankingSystem.Domain.Entities;
using BankingSystem.Domain.Enum;
using BankingSystem.Domain.Exceptions;
using BankingSystem.Domain.ValueObjects;

public class Loan:Entity
{
    public Money Amount{get; private set;}
    public InterestRate InterestRate{ get; private set;}
    public LoanTerm Term {get; private set;}
    public LoanStatus Status {get; private set;}
    public Guid CustomerId {get; private set;}
    public Customer Customer {get; private set;} = null;
    private readonly List<Payment> _payments = new();
    public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();
    public Money TotalInterest => CalculateTotalInterest();
    public Money TotalAmount => Amount + TotalInterest;
    public string RejectedReasons { get; private set; } = null!;

    private Loan(){}

    private Loan(Customer customer, Money money, InterestRate interestRate, LoanTerm term):base()
    {
        if(money.Amount <= 0)
            throw new DomainException("Loan amount must be greater than zero");
        
        CustomerId = customer.Id;
        Customer = customer;
        Amount = money;
        InterestRate = interestRate;
        Term = term;
        Status = LoanStatus.Pending;

        GeneratePaymentSchedule();
    }

    public static Loan Create(Customer customer, Money money, InterestRate interestRate, LoanTerm term)
    {
        var loan = new Loan(customer, money, interestRate, term);
        customer.AddLoan(loan);
        return loan;
    }

    public void Approve()
    {
        if(Status != LoanStatus.Pending)
            throw new DomainException("Loan is not pending approval");

        Status = LoanStatus.Active;
        UpdateModifiedDate();
    }

    public void Rejected(string reason)
    {
        if(Status != LoanStatus.Pending)
            throw new DomainException("Loan is not pending approval");

        Status = LoanStatus.Rejected;
        UpdateModifiedDate();
    }

    public void Disburse()
    {
        if (Status != LoanStatus.Approved)
            throw new DomainException("Only approved loans can be disbursed");
        
        Status = LoanStatus.Disbursed;
        UpdateModifiedDate();
    }

    private Money CalculateTotalInterest() => InterestRate.CalculateInterest(Amount, Term.Months);

    private void GeneratePaymentSchedule()
    {
        _payments.Clear();

        var monthlyPrincipal = Money.Create(Amount.Amount / Term.Months, Amount.Currency);
        var montlyInterest = Money.Create(TotalInterest.Amount / Term.Months, Amount.Currency);
        var paymentDate = DateTime.UtcNow;

        for (int x = 1; x < Term.Months; x++)
        {
            paymentDate = paymentDate.AddMonths(1);

            var payment = Payment.Create(
                this,
                x,
                paymentDate,
                monthlyPrincipal,
                montlyInterest
            );
            _payments.Add(payment);
        }

    }
}