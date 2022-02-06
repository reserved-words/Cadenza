using Cadenza.Domain;

namespace Cadenza.Utilities;

public class ValueMerger : IValueMerger
{
    public string Merge(string original, string update, MergeMode mode)
    {
        return Merged(original, update, mode, v => string.IsNullOrEmpty(v));
    }

    public int Merge(int original, int update, MergeMode mode)
    {
        return Merged(original, update, mode, v => v == 0);
    }

    public int? Merge(int? original, int? update, MergeMode mode)
    {
        return Merged(original, update, mode, v => !v.HasValue);
    }

    public T Merge<T>(T original, T update, MergeMode mode) where T : struct, Enum
    {
        return Merged(original, update, mode, v => v.Equals(default(T)));
    }

    public List<int> MergeTrackCounts(List<int> original, List<int> update, MergeMode mode)
    {
        original ??= new List<int>();
        update ??= new List<int>();

        if (mode == MergeMode.ReplaceAlways)
            return update;

        var originalCount = original.Count;
        var updateCount = update.Count;

        if (originalCount == 0)
            return update;

        if (updateCount == 0)
            return original;

        if (originalCount == 1 && updateCount == 1)
        {
            return mode == MergeMode.ReplaceIfUpdateIsNotEmpty
                ? update
                : original;
        }
        
        var max = Math.Max(originalCount, updateCount);
        var result = new List<int>();

        for (var i = 0; i < max; i++)
        {
            var originalValue = originalCount < i + 1 ? (int?)null : original[i];
            var updateValue = updateCount < i + 1 ? (int?)null : update[i];

            result.Add(Merged(originalValue, updateValue, mode, v => !v.HasValue).Value);
        }

        return result;
    }

    public ICollection<T> MergeList<T>(ICollection<T> original, ICollection<T> update, MergeMode mode) where T : class
    {
        original ??= new List<T>();
        update ??= new List<T>();

        if (mode == MergeMode.ReplaceAlways)
            return update;

        var result = new List<T>();

        foreach (var originalItem in original)
        {
            var updateItem = update.SingleOrDefault(i => i.Equals(originalItem));
            result.Add(Merged(originalItem, updateItem, mode, v => v == null));
        }

        foreach (var updateItem in update)
        {
            var resultItem = result.SingleOrDefault(i => i.Equals(updateItem));
            if (resultItem == null)
            {
                result.Add(updateItem);
            }
        }

        return original;
    }

    private static T Merged<T>(T original, T update, MergeMode mode, Predicate<T> isEmpty)
    {
        return Replace(original, update, mode, isEmpty)
            ? update
            : original;
    }

    private static bool Replace<T>(T original, T update, MergeMode mode, Predicate<T> isEmpty)
    {
        return mode == MergeMode.ReplaceAlways
            || (mode == MergeMode.ReplaceIfUpdateIsNotEmpty && !isEmpty(update))
            || (mode == MergeMode.ReplaceIfOriginalIsEmpty && isEmpty(original));
    }
}