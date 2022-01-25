using Cadenza.Core;
using Cadenza.Domain;
using Cadenza.Utilities;

namespace Cadenza.Database;

public class AlbumRepository : IAlbumRepository
{
    private readonly IIndexedDbFactory _dbFactory;

    public AlbumRepository(IIndexedDbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<AlbumInfo> GetAlbum(string id)
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        var album = db.Albums.Single(a => a.Id == id);

        return new AlbumInfo
        {
            Id = album.Id,
            ArtistId = album.ArtistId,
            ArtistName = album.ArtistName,
            Title = album.Title,
            ArtworkUrl = album.Artwork,
            Year = album.Year,
            ReleaseType = album.ReleaseType.Parse<ReleaseType>(),
            Source = album.Source.Parse<LibrarySource>()
        };
    }
}
