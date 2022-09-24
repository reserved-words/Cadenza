using Cadenza.Utilities.Interfaces;

namespace Cadenza.Utilities.Implementations;

public class ListComparer : IListComparer
{
    public List<string> GetMissingItems(List<string> source, List<string> target)
    {
        return source.Except(target).ToList();
    }
}
