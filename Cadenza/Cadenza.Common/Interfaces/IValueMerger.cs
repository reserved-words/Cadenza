namespace Cadenza.Common;

public interface IValueMerger
{
    string Merge(string original, string update, bool forceUpdate);
    int Merge(int original, int update, bool forceUpdate);
    int? Merge(int? original, int? update, bool forceUpdate);
    Grouping Merge(Grouping original, Grouping update, bool forceUpdate);
    ReleaseType Merge(ReleaseType original, ReleaseType update, bool forceUpdate);
    LibrarySource Merge(LibrarySource original, LibrarySource update, bool forceUpdate);
    ICollection<T> MergeCollection<T>(ICollection<T> list, ICollection<T> update, bool forceUpdate) where T : IMergeable;
    List<int> MergeTrackCounts(List<int> list, List<int> update, bool forceUpdate);
}
