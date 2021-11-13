namespace Cadenza.Common;

public interface IListComparer
{
    List<string> GetMissingItems(List<string> source, List<string> target);
}
