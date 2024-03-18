namespace Cadenza.Web.Api.Interfaces;

public interface IFavouritesApi
{
    Task Favourite(int trackId);
    Task Unfavourite(int trackId);
}
