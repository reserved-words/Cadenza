namespace Cadenza.Domain.Extensions;

public static class DictionaryExtensions
{
    public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> items, TKey key) where TValue : new()
    {
        var value = items.GetValueOrDefault(key);
        if (value == null)
        {
            value = new TValue();
            items.Add(key, value);
        }
        return value;
    }
}