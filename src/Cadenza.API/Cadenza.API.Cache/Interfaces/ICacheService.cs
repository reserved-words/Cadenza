namespace Cadenza.API.Cache.Interfaces;

internal interface ICacheService :
    IArtistRepository,
    ITrackRepository
{
    Task Populate(FullLibraryDTO library);
}
