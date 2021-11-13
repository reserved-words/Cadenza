namespace Cadenza.Common;

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

    public Grouping Merge(Grouping original, Grouping update, bool forceUpdate)
    {
        return forceUpdate || original == 0 || original == Grouping.None
            ? update
            : original;
    }

    public ReleaseType Merge(ReleaseType original, ReleaseType update, bool forceUpdate)
    {
        return forceUpdate || original == 0 || original == ReleaseType.Album
            ? update
            : original;
    }

    public Source Merge(Source original, Source update, bool forceUpdate)
    {
        return forceUpdate || original == 0 || original == Source.Local
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

    public ICollection<T> MergeCollection<T>(ICollection<T> list, ICollection<T> update, bool forceUpdate) where T : IMergeable
    {
        list ??= new List<T>();
        update ??= new List<T>();

        foreach (var updateItem in update)
        {
            var item = list.SingleOrDefault(i => i.Id == updateItem.Id);
            if (item == null)
            {
                list.Add(updateItem);
            }
            else if (!item.IsPopulated || forceUpdate)
            {
                list.Remove(item);
                list.Add(updateItem);
            }
        }

        return list;
    }
}