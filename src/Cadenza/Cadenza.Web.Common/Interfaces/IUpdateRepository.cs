namespace Cadenza.Web.Common.Interfaces;

public interface IUpdateRepository
{
    Task RemoveTrack(int trackId);
    Task UpdateAlbum(AlbumDetailsVM originalAlbum, AlbumDetailsVM updatedAlbum);
    Task UpdateArtist(ArtistDetailsVM originalArtist, ArtistDetailsVM updatedArtist);
    Task UpdateTrack(TrackDetailsVM originalTrack, TrackDetailsVM updatedTrack);
}