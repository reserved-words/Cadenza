namespace Cadenza.Common;

public class NameComparer : INameComparer
{
    public T GetMatch<T>(Dictionary<string, T> dictionary, string name) where T : class, INamed
    {
        var key = dictionary.Keys
            .SingleOrDefault(a => IsMatch(a, name));

        return key == null
            ? null
            : dictionary[key];

    }

    public T GetMatch<T>(List<T> items, string name) where T : INamed
    {
        return items
            .SingleOrDefault(a => IsMatch(a.Name, name));
    }

    public bool IsMatch(string artist1, string artist2)
    {
        return GetCompareName(artist1) == GetCompareName(artist2);
    }

    public string GetCompareName(string name)
    {
        name ??= "";
        name = GetStandardisedName(name);
        name = GetSortName(name);
        name = name.ToLower();
        return name;
    }

    public string GetSortName(string name)
    {
        name = GetStandardisedName(name);
        name = RemovePrefix(name);
        return name;
    }

    public string GetStandardisedName(string name)
    {
        return name.Replace(" & ", " and ");
    }

    private static string RemovePrefix(string name)
    {
        return name.StartsWith("The ", StringComparison.InvariantCultureIgnoreCase)
            ? name[4..]
            : name;
    }
}