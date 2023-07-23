using Common;
using GuardNet;

namespace Api.Domain;
public class Description : ValueObject
{
    private Description() { }

    public Description(string description)
    {
        Guard.NotNullOrEmpty(description, nameof(description), "The description can't be null or empty");
        Value = description;
    }

    public string Value { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
