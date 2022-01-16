namespace Cadenza.Utilities;

public class ValueMerger : IValueMerger
{
    public string Merge(string original, string update, bool forceUpdate)
    {
        return forceUpdate || string.IsNullOrWhiteSpace(original)
            ? update
            : original;
    }

    public int Merge(int original, int update, bool forceUpdate)
    {
        return forceUpdate || original == 0
            ? update
            : original;
    }

    public int? Merge(int? original, int? update, bool forceUpdate)
    {
        return forceUpdate || !original.HasValue
            ? update
            : original;
    }

    public T Merge<T>(T original, T update, bool forceUpdate) where T : struct, Enum
    {
        return forceUpdate || original.Equals(default(T))
            ? update
            : original;
    }

    public List<int> MergeTrackCounts(List<int> list, List<int> update, bool forceUpdate)
    {
        list ??= new List<int>();
        update ??= new List<int>();

        if (forceUpdate)
            return update;

        var listCount = list.Count;
        var updateCount = update.Count;

        if (listCount == 0)
            return update;

        if (updateCount == 0)
            return list;

        if (listCount == 1 && updateCount == 1)
            return update;

        var max = Math.Max(listCount, updateCount);

        for (var i = 0; i < max; i++)
        {
            if (listCount < i + 1)
            {
                list.Add(update[i]);
                continue;
            }

            if (updateCount < i + 1)
                continue;

            if (list[i] == 0)
                list[i] = update[i];
        }

        return list;
    }

    public ICollection<T> MergeList<T>(ICollection<T> list, ICollection<T> update, bool forceUpdate) where T : class
    {
        list ??= new List<T>();
        update ??= new List<T>();

        foreach (var updateItem in update)
        {
            var item = list.SingleOrDefault(i => i.Equals(updateItem));
            if (item == null)
            {
                list.Add(updateItem);
            }
            else if (forceUpdate)
            {
                list.Remove(item);
                list.Add(updateItem);
            }
        }

        return list;
    }
}