namespace Cadenza.API.Cache.Interfaces;

internal interface ICacheService :
    IArtistRepository
{
    Task Populate(FullLibraryDTO library);
}
