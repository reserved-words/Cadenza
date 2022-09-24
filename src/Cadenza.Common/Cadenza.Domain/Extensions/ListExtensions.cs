namespace Cadenza.Domain.Extensions;

public static class ListExtensions
{
    public static void AddIfNotPresent<T>(this List<T> items, T item)
    {
        if (items.Contains(item))
            return;

        items.Add(item);
    }

    public static T GetOrAdd<T>(this List<T> items, Predicate<T> filter) where T : new()
    {
        var value = items.SingleOrDefault(i => filter(i));

        if (value == null)
        {
            value = new T();
            items.Add(value);
        }

        return value;
    }

    public static void RemoveWhere<T>(this List<T> items, Predicate<T> filter)
    {
        var value = items.SingleOrDefault(i => filter(i));

        if (value != null)
        {
            items.Remove(value);
        }
    }

    public static List<T> AddThenSort<T>(this List<T> items, T item, Func<T, object> sortValue)
    {
        items.Add(item);
        items = items.OrderBy(i => sortValue(i)).ToList();
        return items;
    }

    public static Dictionary<TKey, List<TValue>> ToGroupedDictionary<TKey, TValue>(this List<TValue> items, Func<TValue, TKey> getKey)
    {
        return items
            .OrderBy(i => getKey(i))
            .GroupBy(i => getKey(i))
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    public static void RemoveWhere<TKey, TValue>(this Dictionary<TKey, List<TValue>> items, Predicate<TValue> filter)
    {
        foreach (var key in items.Keys)
        {
            items[key]?.RemoveWhere(filter);
        }
    }

    public static void AddThenSort<TKey, TValue>(this Dictionary<TKey, List<TValue>> items, TValue item, Func<TValue, TKey> getKey, Func<TValue, object> sortValue)
    {
        var key = getKey(item);
        var list = items[key];
        list.AddThenSort(item, sortValue);
    }

    public static TValue TryGetValue<TKey, TValue>(this Dictionary<TKey, List<TValue>> items, Predicate<TValue> filter)
    {
        foreach (var key in items.Keys)
        {
            var list = items[key];
            var item = list.SingleOrDefault(i => filter(i));
            if (item != null)
                return item;
        }

        return default;
    }
}