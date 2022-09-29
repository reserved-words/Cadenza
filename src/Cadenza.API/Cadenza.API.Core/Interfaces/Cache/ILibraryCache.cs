using Cadenza.Common.Domain.Model;
using Cadenza.Common.Interfaces.Repositories;

namespace Cadenza.API.Core.Interfaces.Cache;

internal interface ILibraryCache
{
    IAlbumRepository AlbumCache { get; }
    IArtistRepository ArtistCache { get; }
    IPlayTrackRepository PlayTrackCache { get; }
    ISearchRepository SearchCache { get; }
    ITrackRepository TrackCache { get; }

    bool IsPopulated { get; }

    Task Populate(FullLibrary library);
}
