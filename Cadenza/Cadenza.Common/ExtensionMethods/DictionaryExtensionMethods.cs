namespace Cadenza.Common;

public static class DictionaryExtensionMethods
{
    public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
    {
        if (dictionary.TryAdd(key, value))
            return;

        dictionary[key] = value;
    }

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

    public static TEnum ValueOrDefault<TKey, TValue, TEnum>(this Dictionary<TKey, TValue> dictionary, TKey key) where TEnum : struct, Enum
    {
        var value = dictionary.ValueOrDefault(key);

        return value == null
            ? Default.For<TEnum>()
            : value.ToString().Parse<TEnum>();
    }
}