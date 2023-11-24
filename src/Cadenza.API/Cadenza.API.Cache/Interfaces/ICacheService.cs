namespace Cadenza.API.Cache.Interfaces;

internal interface ICacheService :
    IAlbumRepository,
    IArtistRepository,
    ITrackRepository
{
    Task Populate(FullLibraryDTO library);
}
