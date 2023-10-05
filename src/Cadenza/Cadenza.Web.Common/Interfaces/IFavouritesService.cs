namespace Cadenza.Web.Common.Interfaces;

public interface IFavouritesService
{
    Task Favourite(string artist, string title);
    Task Unfavourite(string artist, string title);
    Task<bool> IsFavourite(string artist, string title);
}
