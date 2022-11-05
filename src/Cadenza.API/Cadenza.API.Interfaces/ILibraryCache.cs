using Cadenza.Common.Interfaces.Repositories;

namespace Cadenza.API.Interfaces;

public interface ILibraryCache
{
    IAlbumRepository Albums { get; }
    IArtistRepository Artists { get; }
    IPlayTrackRepository PlayTracks { get; }
    ISearchRepository Search { get; }
    ITrackRepository Tracks { get; }
    ITagRepository Tags { get; }

    bool IsPopulated { get; }

    Task Populate(FullLibrary library);
}
