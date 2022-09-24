namespace Cadenza.Web.Core.Extensions;

public static class DictionaryExtensionMethods
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
}