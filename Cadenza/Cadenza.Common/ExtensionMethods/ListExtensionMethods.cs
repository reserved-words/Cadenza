namespace Cadenza.Common;

public static class ListExtensionMethods
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
    {
        return items == null || items.Any();
    }

    public static void AddIfNotPresent<T>(this List<T> items, T item)
    {
        if (items.Contains(item))
            return;

        items.Add(item);
    }

    public static void Merge<T>(this ICollection<T> target, ICollection<T> source)
    {
        if (source == null)
            return;

        foreach (var item in source.Except(target))
        {
            target.Add(item);
        }
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

    public static SplitList<T1> Split<T1, T2>(this ICollection<T2> allItems, Func<T2, string> key, Func<T2, ICollection<T1>> sectionItems)
    {
        return new SplitList<T1>
        {
            Sections = allItems.Select(p => new ListSection<T1>
            {
                Name = key(p),
                Items = sectionItems(p).ToList()
            })
            .ToList()
        };
    }

    public static SplitList<T> Split<T>(this List<T> items)
    {
        return new SplitList<T>
        {
            Sections = new List<ListSection<T>>
                {
                    new ListSection<T>
                    {
                        Name = null,
                        Items = items
                    }
                }
        };
    }
}