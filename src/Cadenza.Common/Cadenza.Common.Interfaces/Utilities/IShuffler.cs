namespace Cadenza.Common.Interfaces.Utilities;

public interface IShuffler
{
    List<int> Shuffle(List<int> items, int? first = null);
}
