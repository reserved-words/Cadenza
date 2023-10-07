namespace Cadenza.Web.Database.Interfaces;

internal interface IDataTransferObjectMapper
{
    ItemUpdateRequestDTO Map(AlbumUpdateVM vm);
    ItemUpdateRequestDTO Map(ArtistUpdateVM vm);
    ItemUpdateRequestDTO Map(TrackUpdateVM vm);
}
