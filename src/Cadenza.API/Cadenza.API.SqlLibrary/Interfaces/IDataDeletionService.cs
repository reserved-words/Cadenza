namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface IDataDeletionService
{
    Task DeleteTrackById(int id);
    Task DeleteEmptyDiscs();
    Task DeleteEmptyAlbums();
    Task DeleteEmptyArtists();
    Task DeleteTrackByIdFromSource(string idFromSource);
}
