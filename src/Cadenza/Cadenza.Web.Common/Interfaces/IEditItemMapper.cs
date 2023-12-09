namespace Cadenza.Web.Common.Interfaces;

public interface IEditItemMapper
{
    EditableAlbum MapEditableAlbum(AlbumDetailsVM album);
    List<EditableAlbumDisc> MapEditableAlbumTracks(IReadOnlyCollection<AlbumDiscVM> tracks);
    EditableArtist MapEditableArtist(ArtistDetailsVM artist);
    List<EditableArtistRelease> MapEditableArtistReleases(IReadOnlyCollection<AlbumVM> releases);
    EditableTrack MapEditableTrack(TrackDetailsVM track);

    AlbumDetailsVM MapEditedAlbum(EditableAlbum album);
    IReadOnlyCollection<AlbumDiscVM> MapEditedAlbumTracks(List<EditableAlbumDisc> tracks);
    ArtistDetailsVM MapEditedArtist(EditableArtist artist);
    IReadOnlyCollection<AlbumVM> MapEditedArtistReleases(List<EditableArtistRelease> releases);
    TrackDetailsVM MapEditedTrack(EditableTrack track);
}
