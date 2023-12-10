namespace Cadenza.Web.Api.Interfaces;

public interface IUpdateApi
{
    Task<UpdateAlbumVM> GetAlbum(int id);
    Task<List<UpdateAlbumTrackVM>> GetAlbumTracks(int albumId);

    Task UpdateAlbum(int albumId, UpdateAlbumVM updatedAlbum, IReadOnlyCollection<UpdateAlbumTrackVM> updatedTracks, IReadOnlyCollection<int> removedTracks);
    Task UpdateArtist(ArtistDetailsVM updatedArtist);
    Task UpdateTrack(TrackDetailsVM updatedTrack);
}