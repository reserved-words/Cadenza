namespace Cadenza.Web.Common.Interfaces;

public interface IUpdateRepository
{
    Task RemoveTrack(int trackId);
    Task UpdateAlbum(EditableAlbum album);
    Task UpdateArtist(EditableArtist artist);
    Task UpdateTrack(EditableTrack track);
}