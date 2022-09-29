namespace Cadenza.Common.Utilities.Services;

internal class IdGenerator : IIdGenerator
{
    private readonly INameComparer _nameComparer;

    public IdGenerator(INameComparer nameComparer)
    {
        _nameComparer = nameComparer;
    }

    public string GenerateId(params string[] names)
    {
        var standardisedNames = names
            .Select(n => Standardise(n))
            .ToList();

        return string.Join("|", standardisedNames);
    }

    private string Standardise(string name)
    {
        return _nameComparer.GetCompareName(name)
            .Replace(" ", "");
    }
}
