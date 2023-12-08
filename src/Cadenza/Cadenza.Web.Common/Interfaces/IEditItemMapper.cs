namespace Cadenza.Web.Common.Interfaces;

public interface IEditItemMapper
{
    EditableAlbum MapAlbum(AlbumDetailsVM album);
    List<EditableAlbumDisc> MapAlbumTracks(IReadOnlyCollection<AlbumDiscVM> tracks);
    EditableArtist MapArtist(ArtistDetailsVM artist);
    List<EditableArtistRelease> MapArtistReleases(IReadOnlyCollection<AlbumVM> releases);
    EditableTrack MapTrack(TrackDetailsVM track);
}
