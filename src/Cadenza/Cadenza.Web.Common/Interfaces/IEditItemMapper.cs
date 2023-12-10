namespace Cadenza.Web.Common.Interfaces;

public interface IEditItemMapper
{
    EditableAlbum MapEditableAlbum(UpdateAlbumVM album);
    List<EditableAlbumDisc> MapEditableAlbumTracks(IReadOnlyCollection<UpdateAlbumTrackVM> tracks);
    EditableArtist MapEditableArtist(ArtistDetailsVM artist);
    List<EditableArtistRelease> MapEditableArtistReleases(IReadOnlyCollection<AlbumVM> releases);
    EditableTrack MapEditableTrack(TrackDetailsVM track);

    UpdateAlbumVM MapEditedAlbum(EditableAlbum album);
    IReadOnlyCollection<UpdateAlbumTrackVM> MapEditedAlbumTracks(List<EditableAlbumDisc> tracks);
    ArtistDetailsVM MapEditedArtist(EditableArtist artist);
    IReadOnlyCollection<AlbumVM> MapEditedArtistReleases(List<EditableArtistRelease> releases);
    TrackDetailsVM MapEditedTrack(EditableTrack track);
}
