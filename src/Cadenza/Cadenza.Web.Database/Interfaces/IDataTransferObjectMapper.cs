namespace Cadenza.Web.Database.Interfaces;

internal interface IDataTransferObjectMapper
{
    UpdateAlbumDTO Map(EditableAlbum vm);
    UpdateArtistDTO Map(EditableArtist vm);
    UpdateTrackDTO Map(EditableTrack vm);
}
