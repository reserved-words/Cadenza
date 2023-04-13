namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface ILibraryUpdater
{
    Task UpdateAlbum(ItemUpdateRequest request);
    Task UpdateArtist(ItemUpdateRequest request);
    Task UpdateTrack(ItemUpdateRequest request);
}
