namespace Cadenza.API.Interfaces.Services;

public interface ILibraryCache
{
    IAlbumRepository Albums { get; }
    IArtistRepository Artists { get; }
    ITrackRepository Tracks { get; }

    bool IsPopulated { get; }

    Task Populate(FullLibraryDTO library);
}
