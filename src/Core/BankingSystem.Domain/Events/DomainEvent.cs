namespace BankingSystem.Domain.Events
{
    public abstract class DomainEvent
	{
		public Guid Id { get; }
		public DateTime OcurrenOn { get; }


		protected DomainEvent()
		{
			Id = Guid.NewGuid();
			OcurrenOn = DateTime.UtcNow;
		}
	}
}

