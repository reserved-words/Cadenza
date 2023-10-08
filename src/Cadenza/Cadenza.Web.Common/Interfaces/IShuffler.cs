namespace Cadenza.Web.Common.Interfaces;

public interface IShuffler
{
    List<int> Shuffle(List<int> items, int? first = null);
}
