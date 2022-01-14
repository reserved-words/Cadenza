using Cadenza.Domain;

namespace Cadenza.Database;

public class ArtistRepository : IArtistRepository
{
    private readonly IIndexedDbFactory _dbFactory;

    public ArtistRepository(IIndexedDbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<LibraryArtist>> GetAlbumArtists()
    {
        using var db = await _dbFactory.Create<LibraryDb>();
        
        return db.Artists
            .Join(db.Albums, a => a.Id, a => a.ArtistId, (artist, album) => artist)
            .Distinct()
            .Select(a => new LibraryArtist
            {
                Id = a.Id,
                Name = a.Name,
                Grouping = a.Grouping
            })
            .ToList();
    }

    public async Task<LibraryArtistDetails> GetArtist(string id)
    {
        using var db = await _dbFactory.Create<LibraryDb>();
        
        var artist = db.Artists.Single(a => a.Id == id);
        var albums = db.Albums.Where(a => a.ArtistId == id);

        return new LibraryArtistDetails
        {
            Id = artist.Id,
            Name = artist.Name,
            Grouping = artist.Grouping,
            Genre = artist.Genre,
            Country = artist.Country,
            State = artist.State,
            City = artist.City,
            Releases = albums
                .GroupBy(a => a.ReleaseType.GetGroup())
                .OrderBy(g => g.Key)
                .ToDictionary(
                    grp => grp.Key,
                    grp => grp.Select(a => new LibraryAlbum
                    {
                        Id = a.Id,
                        Title = a.Title,
                        ArtistId = a.ArtistId,
                        Artist = artist.Name,
                        Artwork = a.Artwork,
                        Year = a.Year,
                        ReleaseType = a.ReleaseType,
                        Group = a.ReleaseType.GetGroup(),
                        Source = a.Source
                    })
                    .ToList())
        };
    }
}