namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface IDataDeletionService
{
    Task DeleteTrack(string id);
    Task DeleteEmptyDiscs();
    Task DeleteEmptyAlbums();
    Task DeleteEmptyArtists();
}
