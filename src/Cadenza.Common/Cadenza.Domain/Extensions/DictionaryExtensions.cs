namespace Cadenza.Domain;

public static class DictionaryExtensions
{
    public static T GetOrAdd<T>(this Dictionary<string, T> items, string key) where T : new()
    {
        var value = items.GetValueOrDefault(key);
        if (value == null)
        {
            value = new T();
            items.Add(key, value);
        }
        return value;
    }

    public static TValue ValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
    {
        return dictionary.TryGetValue(key, out TValue value)
            ? value
            : default;
    }
}