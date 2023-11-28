namespace Cadenza.Web.Common.Interfaces;

public interface IFavouritesService
{
    Task Favourite(int trackId);
    Task Unfavourite(int trackId);
}
