namespace Cadenza.Local.API.Interfaces;

public interface IExternalSourceService
{
    Task AddLibrary(ExternalSourceLibrary library);
    Task<DateTime?> GetLastSyncDate(LibrarySource source);
}