namespace Cadenza.API.Core.Interfaces.Cache;

internal interface ICache : IAlbumRepository, IArtistRepository, ITrackRepository, ISearchRepository, ITagRepository
{
    Task Populate(FullLibrary library);
}
