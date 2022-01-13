namespace Cadenza.Common;

public interface IIdGenerator
{
    string GenerateArtistId(string artistName);
    string GenerateAlbumId(string artistName, string title);
}
