namespace Cadenza.Common;

public interface INameComparer
{
    bool IsMatch(string name1, string name2);

    string GetCompareName(string name);

    string GetSortName(string name);

    string GetStandardisedName(string name);

    T GetMatch<T>(Dictionary<string, T> dictionary, string name) where T : class, INamed;

    T GetMatch<T>(List<T> items, string name) where T : INamed;
}