namespace Cadenza.Web.Common.Interfaces;

public interface IEditItemMapper
{
    EditableAlbum MapEditableAlbum(AlbumDetailsVM album);
    EditableAlbumDiscs MapEditableAlbumTracks(IReadOnlyCollection<AlbumTrackVM> tracks);
    EditableArtist MapEditableArtist(ArtistDetailsVM artist);
    List<EditableArtistRelease> MapEditableArtistReleases(IReadOnlyCollection<AlbumVM> releases);
    EditableTrack MapEditableTrack(TrackDetailsVM track);

    AlbumDetailsVM MapEditedAlbum(EditableAlbum album);
    IReadOnlyCollection<AlbumTrackVM> MapEditedAlbumTracks(EditableAlbumDiscs discs);
    ArtistDetailsVM MapEditedArtist(EditableArtist artist);
    IReadOnlyCollection<AlbumVM> MapEditedArtistReleases(List<EditableArtistRelease> releases);
    TrackDetailsVM MapEditedTrack(EditableTrack track);
}
