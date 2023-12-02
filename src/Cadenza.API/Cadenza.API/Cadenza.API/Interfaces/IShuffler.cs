namespace Cadenza.API.Interfaces;

public interface IShuffler
{
    List<int> Shuffle(List<int> items, int? first = null);
}
