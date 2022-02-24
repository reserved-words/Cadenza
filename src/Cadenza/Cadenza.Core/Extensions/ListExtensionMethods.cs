namespace Cadenza.Core.Extensions;

public static class ListExtensionMethods
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
}
