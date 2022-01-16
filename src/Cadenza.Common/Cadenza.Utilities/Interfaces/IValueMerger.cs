namespace Cadenza.Utilities;

public interface IValueMerger
{
    string Merge(string original, string update, bool forceUpdate);
    int Merge(int original, int update, bool forceUpdate);
    int? Merge(int? original, int? update, bool forceUpdate);
    T Merge<T>(T original, T update, bool forceUpdate) where T : struct, Enum;
    ICollection<T> MergeList<T>(ICollection<T> list, ICollection<T> update, bool forceUpdate) where T : class;
    List<int> MergeTrackCounts(List<int> list, List<int> update, bool forceUpdate);
}
