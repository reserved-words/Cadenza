namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface ILibraryUpdater
{
    Task UpdateAlbum(ItemUpdates updates);
    Task UpdateArtist(ItemUpdates updates);
    Task UpdateTrack(ItemUpdates updates);
}
