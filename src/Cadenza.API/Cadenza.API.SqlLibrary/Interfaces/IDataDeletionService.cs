namespace Cadenza.API.SqlLibrary.Interfaces;

internal interface IDataDeletionService
{
    Task DeleteTrack(LibrarySource source, string id);
    Task DeleteEmptyDiscs();
    Task DeleteEmptyAlbums();
    Task DeleteEmptyArtists();
}