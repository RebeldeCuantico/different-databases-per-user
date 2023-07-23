using Common;
using GuardNet;

namespace Api.Domain;
public class Name : ValueObject
{
    private Name() { }

    public Name(string name)
    {
        Guard.NotNullOrEmpty(name, nameof(name), "The name Can't be null or empty");
        Value = name;
    }

    public string Value { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
