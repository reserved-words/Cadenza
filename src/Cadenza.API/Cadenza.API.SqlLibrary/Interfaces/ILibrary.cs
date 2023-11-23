namespace Cadenza.Database.SqlLibrary.Interfaces;

internal interface ILibrary
{
    Task<List<GetArtistData>> GetArtists();
    Task<List<GetAlbumData>> GetAlbums(LibrarySource? source);
    Task<List<GetDiscData>> GetDiscs(LibrarySource? source);
    Task<List<GetTrackData>> GetTracks(LibrarySource? source);
    Task<List<string>> GetTrackSourceIds(LibrarySource source);

    Task<AlbumData> GetAlbum(int albumId);
    Task<ArtistData> GetArtist(int artistId);
    Task<TrackData> GetTrack(int trackId);

    Task<AlbumArtwork> GetAlbumArtwork(int albumId);
    Task<ArtistImage> GetArtistImage(int artistId);

    Task<List<string>> GetAlbumTrackSourceIds(int albumId);
    Task<string> GetTrackIdFromSource(int trackId);
    Task<List<string>> GetArtistTrackSourceIds(int artistId);
}
