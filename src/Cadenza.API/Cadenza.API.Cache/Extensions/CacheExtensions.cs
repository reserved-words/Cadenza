namespace Cadenza.API.Cache.Extensions;

internal static class CacheExtensions
{
    public static void Cache<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key, T2 value)
    {
        if (!dictionary.ContainsKey(key))
        {
            dictionary.Add(key, value);
        }
    }


    public static void Cache<T1, T2>(this Dictionary<T1, List<T2>> dictionary, T1 key, T2 value)
    {
        if (!dictionary.TryGetValue(key, out var list))
        {
            list = new List<T2>();
            dictionary.Add(key, list);
        }

        if (list.Contains(value))
            return;

        list.Add(value);
    }

    public static List<T3> GetAllValues<T1, T2, T3>(this Dictionary<T1, T2> dictionary) where T2 : T3
    {
        return dictionary.Values
            .OfType<T3>()
            .ToList();
    }

    public static List<T3> GetList<T1, T2, T3>(this Dictionary<T1, List<T2>> dictionary, T1 key) where T2 : T3
    {
        return dictionary.TryGetValue(key, out List<T2> list)
            ? list.OfType<T3>().ToList()
            : new List<T3>();
    }

    public static T2 GetValue<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key) where T2 : class
    {
        return dictionary.TryGetValue(key, out T2 value)
            ? value
            : null;
    }
}
