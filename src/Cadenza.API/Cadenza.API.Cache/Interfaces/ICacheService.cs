namespace Cadenza.API.Cache.Interfaces;

internal interface ICacheService :
    IAlbumRepository,
    IArtistRepository,
    ITrackRepository,
    ISearchRepository,
    ITagRepository
{
    Task Populate(FullLibraryDTO library);
}
