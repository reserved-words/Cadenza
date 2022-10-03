namespace Cadenza.Web.Common.Interfaces.Favourites;

public interface IFavouritesController
{
    Task Favourite(string artist, string title);
    Task Unfavourite(string artist, string title);
}
