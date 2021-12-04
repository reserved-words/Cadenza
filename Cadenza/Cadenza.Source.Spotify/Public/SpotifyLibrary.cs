namespace Cadenza.Source.Spotify;

public class SpotifyLibrary : SourceLibrary, ISourceRepository
{
    private readonly ILibrary _library;

    public SpotifyLibrary(ILibrary library)
        :base(library, LibrarySource.Spotify)
    {
        _library = library;
    }

    public async Task<ICollection<ArtistInfo>> GetArtists()
    {
        var artists = await _library.GetAlbumArtists();
        var result = new List<ArtistInfo>();
        foreach (var artist in artists)
        {
            var artistInfo = await _library.GetAlbumArtist(artist.Id);
            // this includes albums, don't need them at this point
            result.Add(artistInfo.Artist);
        }
        return result;
    }

    public async Task<ICollection<AlbumInfo>> GetAlbums()
    {
        var artists = await _library.GetAlbumArtists();
        var result = new List<AlbumInfo>();
        foreach (var artist in artists)
        {
            var artistInfo = await _library.GetAlbumArtist(artist.Id);
            foreach (var album in artistInfo.Albums)
            {
                result.Add(album.Album);
            }
        }
        return result;
    }

    public async Task<ICollection<TrackInfo>> GetTracks()
    {
        return new List<TrackInfo>();
    }

    public async Task<ICollection<AlbumTrackLink>> GetAlbumTrackLinks()
    {
        return new List<AlbumTrackLink>();
    }
}

