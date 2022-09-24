namespace Cadenza.Domain.Attributes;

[AttributeUsage(AttributeTargets.Enum)]
public class DefaultValueAttribute : Attribute
{
    public DefaultValueAttribute(object value)
    {
        Value = value;
    }

    public object Value { get; private set; }
}
