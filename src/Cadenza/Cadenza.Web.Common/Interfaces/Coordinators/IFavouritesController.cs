namespace Cadenza.Web.Common.Interfaces.Coordinators;

public interface IFavouritesController
{
    Task Favourite(string artist, string title);
    Task Unfavourite(string artist, string title);
}
