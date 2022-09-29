namespace Cadenza.Common.Domain.Extensions;

public static class ListExtensions
{
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
}