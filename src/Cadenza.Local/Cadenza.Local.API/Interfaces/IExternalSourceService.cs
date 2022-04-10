namespace Cadenza.Local.API.Interfaces;

public interface IExternalSourceService
{
    Task AddLibrary(ExternalSourceLibrary library);
}