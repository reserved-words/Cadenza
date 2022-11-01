using Cadenza.Common.Interfaces.Repositories;

namespace Cadenza.API.Interfaces;

public interface ILibraryCache
{
    IAlbumRepository AlbumCache { get; }
    IArtistRepository ArtistCache { get; }
    IPlayTrackRepository PlayTrackCache { get; }
    ISearchRepository SearchCache { get; }
    ITrackRepository TrackCache { get; }
    ITagRepository TagCache { get; }

    bool IsPopulated { get; }

    Task Populate(FullLibrary library);
}
