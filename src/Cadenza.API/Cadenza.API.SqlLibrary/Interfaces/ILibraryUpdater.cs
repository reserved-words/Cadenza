namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface ILibraryUpdater
{
    Task UpdateAlbum(ItemUpdateRequestDTO request);
    Task UpdateArtist(ItemUpdateRequestDTO request);
    Task UpdateTrack(ItemUpdateRequestDTO request);
}
