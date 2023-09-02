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
    Task<ArtistData> GetArtist(int artistId);
    Task<TrackData> GetTrack(int trackId);

    Task<List<AlbumUpdateData>> GetAlbumUpdates(LibrarySource source);
    Task<List<ArtistUpdateData>> GetArtistUpdates(LibrarySource source);
    Task<List<TrackRemovalData>> GetTrackRemovals(LibrarySource source);
    Task<List<TrackUpdateData>> GetTrackUpdates(LibrarySource source);

    Task<AlbumArtwork> GetAlbumArtwork(int albumId);
    Task<ArtistImage> GetArtistImage(int artistId);

    Task<List<RecentAlbumData>> GetRecentAlbums(int maxItems);
    Task<List<RecentTagData>> GetRecentTags(int maxItems);

    Task<List<string>> GetAlbumTrackSourceIds(int albumId);
    Task<string> GetTrackIdFromSource(int trackId);
    Task<List<string>> GetArtistTrackSourceIds(int artistId);

    Task<List<Grouping>> GetGroupings();
}