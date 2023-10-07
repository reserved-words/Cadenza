using Cadenza.Common.LastFm;

namespace Cadenza.API.Interfaces.LastFm;

public interface IFavourites
{
    Task Favourite(FavouriteTrack track);
    Task<bool> IsFavourite(string artist, string title);
    Task Unfavourite(FavouriteTrack track);
}