 public abstract class Entity
 {
    public Guid Id { get; protected set; }
    //protected set: only the entity is allowed to modify its state and 
    // prevents modifications outside of the entity
    public DateTime CreatedAt { get; protected set;}
    public DateTime UpdatedAt {get; protected set;}

    protected Entity()
    {
        Id= Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    protected void UpdateModifiedDate()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public override bool Equals(object? obj)
    {
        if(obj is not Entity other)
            return false;
        
        return Id.Equals(other.Id);
    }

    public override int GetHashCode() => Id.GetHashCode();
 }