namespace Cadenza.Utilities.Interfaces;

public interface IShuffler
{
    List<T> Shuffle<T>(List<T> items, T first = null) where T : class;
}
