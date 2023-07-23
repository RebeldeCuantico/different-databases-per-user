using Common;

namespace Api.Domain;
 public class Category : AggregateRoot
{
    private Category() : base() { }

    public Category(Name name, Description description, EntityId id)
        : base(id)
    {
        Name = name;
        Description = description;       
    }

    public Name Name { get; private set; }

    public Description Description { get; private set; }

    public void Remove()
    {
       throw new NotImplementedException();
    }

    public void ChangeName(string name)
    {
        var oldName = Name.Value;
        Name = new Name(name);        
    }

    public void ChangeDescription(string description)
    {
        var oldDescription = Description.Value;
        Description = new Description(description);       
    }
}