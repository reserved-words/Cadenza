namespace Cadenza.Common;

public interface INameComparer
{
    bool IsMatch(string name1, string name2);

    string GetCompareName(string name);

    string GetSortName(string name);

    string GetStandardisedName(string name);
}