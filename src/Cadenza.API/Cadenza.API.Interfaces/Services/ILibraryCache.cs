namespace Cadenza.API.Interfaces.Services;

public interface ILibraryCache
{
    IArtistRepository Artists { get; }

    bool IsPopulated { get; }

    Task Populate(FullLibraryDTO library);
}
