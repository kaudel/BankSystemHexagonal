using BankingSystem.Domain.Enum;
using BankingSystem.Domain.Exceptions;

public class Customer:Entity
{
    public string Name { get; private set; }
    public string DocumentNumber { get; private set; }
    public CustomerStatus Status {get; private set;}

    //Collections
    private readonly List<Loan> _loans;
    public IReadOnlyCollection<Loan> Loans => _loans.AsReadOnly();

    private Customer()
    {
        //TODO: required for EF
    }

    private Customer(string name, string documentNumber):base()
    {
        Name = name;
        DocumentNumber = documentNumber;
        Status = CustomerStatus.Active;
    }

    public static Customer Create(string name, string documentNumber)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new DomainException("Name is required to create Customer");
        
        if(string.IsNullOrWhiteSpace(documentNumber))
            throw new DomainException("Document number is required to create Customer");
        
        return new Customer(name, documentNumber);
    }

    public void Deactivate(string reason)
    {
        if(Status == CustomerStatus.Inactive )
            throw new DomainException("Customer is already inactive");
        
        if(_loans.Any( x => x.Status == LoanStatus.Active))
            throw new DomainException("Customer has active loans");
        
        Status = CustomerStatus.Inactive;
        UpdateModifiedDate();
    }

    internal void AddLoan(Loan loan)
    {
        if(Status != CustomerStatus.Active)
            throw new DomainException("Customer must be active to add a loan");
        
        _loans.Add(loan);
        UpdateModifiedDate();
    }

}