namespace Cadenza.Web.Common.Interfaces;

public interface IFavouritesMessenger
{
    Task<bool> IsFavourite(string artist, string title);
}