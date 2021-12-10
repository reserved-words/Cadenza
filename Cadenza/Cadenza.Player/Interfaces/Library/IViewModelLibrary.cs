using Cadenza.Database;

namespace Cadenza.Player;

public interface IViewModelLibrary
{
    event AlbumUpdatedEventHandler AlbumUpdated;
    event ArtistUpdatedEventHandler ArtistUpdated;
    event TrackUpdatedEventHandler TrackUpdated;

    //Task<List<LibrarySource>> GetEnabledSources();
    //Task<List<Artist>> GetAlbumArtists();
    Task<ArtistViewModel> GetArtist(string artistId);
   // Task<TrackSummary> GetTrackSummary(LibrarySource source, string id);
    //Task<List<PlaylistTrackViewModel>> GetAllTracks();
    Task<TrackFull> GetTrack(LibrarySource source, string id);
    //Task<List<PlayTrack>> GetPlaylistTracks(string name);
}