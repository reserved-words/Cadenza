namespace Cadenza.Source.Spotify;

public class SpotifyLibrary : SourceLibrary, ISourceRepository
{
    public SpotifyLibrary(ILibrary libraryConsumer)
        : base(libraryConsumer, LibrarySource.Spotify)
    {
    }

    public async Task<ICollection<ArtistInfo>> GetArtists()
    {
        return new List<ArtistInfo>();
    }

    public async Task<ICollection<AlbumInfo>> GetAlbums()
    {
        return new List<AlbumInfo>();
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

