namespace Cadenza.Common;

public class Shuffler : IShuffler
{
    private readonly IRandomGenerator _random;

    public Shuffler(IRandomGenerator random)
    {
        _random = random;
    }

    public IEnumerable<T> Shuffle<T>(IEnumerable<T> items, T first = null) where T : class
    {
        var shuffledItems = GetShuffledItems(items);
        SetFirst(shuffledItems, first);
        return shuffledItems;
    }

    private List<T> GetShuffledItems<T>(IEnumerable<T> items)
    {
        return items.OrderBy(t => _random.Next()).ToList();
    }

    private void SetFirst<T>(List<T> items, T first)
    {
        if (first == null)
            return;

        items.Remove(first);
        items.Insert(0, first);
    }
}
