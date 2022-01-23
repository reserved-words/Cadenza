using Cadenza.Core;
using Cadenza.Domain;

namespace Cadenza.Database;

public class MainRepository : IMainRepository
{
    private readonly IIndexedDbFactory _dbFactory;

    public MainRepository(IIndexedDbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task AddAlbums(List<AlbumInfo> albums)
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        foreach (var album in albums)
        {
            db.AddAlbum(album);
        }

        await db.SaveChanges();
    }

    public async Task AddArtists(List<ArtistInfo> artists)
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        var alreadyPopulated = db.Artists.Any();

        if (alreadyPopulated)
        {
            foreach (var artist in artists)
            {
                db.AddOrUpdateArtist(artist);
            }
        }
        else
        {
            foreach (var artist in artists)
            {
                db.AddArtist(artist);
            }
        }

        await db.SaveChanges();
    }

    public async Task AddTracks(LibrarySource source, List<BasicTrack> tracks)
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        var artists = tracks.GroupBy(a => a.ArtistId);
        foreach (var artist in artists)
        {
            db.AddTracks(PlayTrackType.Artist, artist.Key, artist.ToList());
        }

        var albums = tracks.GroupBy(a => a.AlbumId);
        foreach (var album in albums)
        {
            db.AddTracks(PlayTrackType.Album, album.Key, album.ToList());
        }

        db.AddTracks(PlayTrackType.All, source.ToString(), tracks);

        await db.SaveChanges();
    }

    public async Task Clear()
    {
        using var db = await _dbFactory.Create<LibraryDb>();
        db.Artists.ToList().ForEach(a => db.Artists.Remove(a));
        db.Albums.ToList().ForEach(a => db.Albums.Remove(a));
        db.Tracks.ToList().ForEach(a => db.Tracks.Remove(a));
        db.PlayTracks.ToList().ForEach(a => db.PlayTracks.Remove(a));
        await db.SaveChanges();
    }

    public async Task<IEnumerable<SearchableItem>> GetSearchableItems()
    {
        using var db = await _dbFactory.Create<LibraryDb>();

        var artists = db.Artists.Select(a => new SearchableArtist(a.Id, a.Name)).OfType<SearchableItem>().ToList();
        
        var albums = db.Albums.Select(a => new SearchableAlbum(a.Id, a.Title, a.ArtistName)).OfType<SearchableItem>().ToList();

        if (!artists.Any() || !albums.Any())
            return new List<SearchableItem>();

        var allTracks = Enum.GetValues<LibrarySource>()
            .SelectMany(s => db.GetPlayTracks(PlayTrackType.All, s.ToString()));

        var tracks = Merge(allTracks, artists, albums);

        return artists
            .Concat(albums)
            .Concat(tracks);
    }

    private static IEnumerable<SearchableItem> Merge(IEnumerable<BasicTrack> allTracks, List<SearchableItem> artists, List<SearchableItem> albums)
    {
        var artistsDict = artists.ToDictionary(a => a.Id, a => a);
        var albumsDict = albums.ToDictionary(a => a.Id, a => a);

        // TODO: For now setting album details to empty if null albumId, but there never should be any - figure out how there were

        return allTracks
            .Select(t => 
        {
            var artist = t.ArtistId == null
                ? new SearchableArtist("", "No Artist Found")
                : artistsDict[t.ArtistId];

            var album = t.AlbumId == null 
                ? new SearchableAlbum("", "No Album Found", artist.Name)
                : albumsDict[t.AlbumId];

            return new SearchableTrack(
                t.Id,
                t.Title,
                artist.Name,
                album.Name,
                album.Artist);
        });
    }
}