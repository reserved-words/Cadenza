namespace Cadenza.Core;

public class StoredValue<T>
{
    public StoredValue()
    {

    }

    public StoredValue(T value)
    {
        Value = value;
        Updated = DateTime.Now;
    }

    public T Value { get; set; }
    public DateTime Updated { get; set; }
}