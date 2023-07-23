namespace Common;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot()
        : base() { }

    protected AggregateRoot(EntityId id)
        : base(id) { }
}
