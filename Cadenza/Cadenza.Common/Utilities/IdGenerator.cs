namespace Cadenza.Common;

public class IdGenerator : IIdGenerator
{
    private readonly INameComparer _nameComparer;

    public IdGenerator(INameComparer nameComparer)
    {
        _nameComparer = nameComparer;
    }

    public string GenerateArtistId(string artistName)
    {
        var name = _nameComparer.GetCompareName(artistName);
        return name.Replace(" ", "");
    }

    public string GenerateAlbumId(string artistName, string title)
    {
        var a = _nameComparer.GetCompareName(artistName);
        var t = _nameComparer.GetCompareName(title);
        return string.Join("|", a, t).Replace(" ", "");
    }
}
