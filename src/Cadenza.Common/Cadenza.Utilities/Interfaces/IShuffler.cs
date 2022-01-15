namespace Cadenza.Utilities;

public interface IShuffler
{
    IEnumerable<T> Shuffle<T>(IEnumerable<T> items, T first = null) where T : class;
}
