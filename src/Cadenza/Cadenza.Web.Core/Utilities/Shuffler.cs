using Cadenza.Common.Utilities.Interfaces;

namespace Cadenza.Web.Core.Utilities;

internal class Shuffler : IShuffler
{
    public List<int> Shuffle(List<int> items, int? first = null)
    {
        var shuffledItems = GetShuffledItems(items);
        SetFirst(shuffledItems, first);
        return shuffledItems;
    }

    private static List<T> GetShuffledItems<T>(List<T> items)
    {
        int n = items.Count;
        while (n > 1)
        {
            n--;
            int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
            T value = items[k];
            items[k] = items[n];
            items[n] = value;
        }
        return items;
    }

    private static void SetFirst(List<int> items, int? first)
    {
        if (first == null)
            return;

        items.Remove(first.Value);
        items.Insert(0, first.Value);
    }

    public static class ThreadSafeRandom
    {
        [ThreadStatic] private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }
}
