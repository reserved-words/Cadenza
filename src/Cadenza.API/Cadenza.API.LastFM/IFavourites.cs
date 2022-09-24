using Cadenza.Domain.Models.LastFm;

namespace Cadenza.API.LastFM;

public interface IFavourites
{
    Task Favourite(Track track);
    Task<bool> IsFavourite(string artist, string title);
    Task Unfavourite(Track track);
}