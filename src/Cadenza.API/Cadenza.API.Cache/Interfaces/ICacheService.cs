namespace Cadenza.API.Cache.Interfaces;

internal interface ICacheService :
    IAlbumRepository,
    IArtistRepository,
    ITrackRepository,
    ITagRepository
{
    Task Populate(FullLibraryDTO library);
}
