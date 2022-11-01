namespace Cadenza.API.Cache.Interfaces;

internal interface ICacheService : IAlbumRepository, IArtistRepository, ITrackRepository, ISearchRepository, ITagRepository, IPlayTrackRepository
{
    Task Populate(FullLibrary library);
}
