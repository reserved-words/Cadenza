namespace Cadenza.API.Interfaces.LastFm;

public interface IFavourites
{
    Task Favourite(FavouriteTrackDTO track);
    Task<bool> IsFavourite(string artist, string title);
    Task Unfavourite(FavouriteTrackDTO track);
}