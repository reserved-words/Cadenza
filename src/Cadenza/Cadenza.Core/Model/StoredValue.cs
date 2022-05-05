namespace Cadenza.Core;

public class StoredValue<T>
{
    public StoredValue()
    {

    }

    public StoredValue(T value, int? expiresInSeconds)
    {
        Value = value;
        Updated = DateTime.Now;
        Expires = expiresInSeconds.HasValue ? DateTime.Now.AddSeconds(expiresInSeconds.Value) : null;
    }

    public T Value { get; set; }
    public DateTime Updated { get; set; }
    public DateTime? Expires { get; set; }

    public bool IsExpired => Expires.HasValue && Expires.Value < DateTime.Now;
}