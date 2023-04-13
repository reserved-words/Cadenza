using Cadenza.API.SqlLibrary.Model;

namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface IDataReadService
{
    Task<List<GetArtistData>> GetArtists();
    Task<List<GetAlbumData>> GetAlbums(LibrarySource? source);
    Task<List<GetDiscData>> GetDiscs(LibrarySource? source);
    Task<List<GetTrackData>> GetTracks(LibrarySource? source);
    Task<List<string>> GetAllTrackIds(LibrarySource source);

    Task<AlbumData> GetAlbum(int albumId);
    Task<ArtistData> GetArtist(string nameId);
    Task<TrackData> GetTrack(string idFromSource);

    Task<List<AlbumUpdateData>> GetAlbumUpdates(LibrarySource source);
    Task<List<ArtistUpdateData>> GetArtistUpdates(LibrarySource source);
    Task<List<TrackRemovalData>> GetTrackRemovals(LibrarySource source);
    Task<List<TrackUpdateData>> GetTrackUpdates(LibrarySource source);

    Task<AlbumArtwork> GetAlbumArtwork(int albumId);
    Task<ArtistImage> GetArtistImage(string nameId);

    Task<List<RecentAlbumData>> GetRecentAlbums(int maxItems);
    Task<List<RecentTagData>> GetRecentTags(int maxItems);
}