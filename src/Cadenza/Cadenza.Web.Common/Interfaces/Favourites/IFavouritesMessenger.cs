namespace Cadenza.Web.Common.Interfaces.Favourites;

public interface IFavouritesMessenger
{
    Task<bool> IsFavourite(string artist, string title);
}