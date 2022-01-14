using Cadenza.Domain;

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
            ReleaseType = album.ReleaseType,
            Source = album.Source
        };
    }

    private static List<PlayTrack> GetById(LibraryDb db, string id, LibrarySource source)
    {
        var tracks = db.PlayTracks
            .SingleOrDefault(a => a.Id == id);

        if (tracks == null)
            return new List<PlayTrack>();

        return tracks.Tracks
            .Split(",")
            .Select(t => new PlayTrack
            {
                Id = t,
                Source = source
            })
            .ToList();
    }

    private static string GetId(PlayTrackType type, string id, LibrarySource source)
    {
        return $"{type}|{id}|{source}";
    }
}
