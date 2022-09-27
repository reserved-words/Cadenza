using Track = Cadenza.Domain.Model.LastFm.Track;

namespace Cadenza.API.Interfaces.LastFm;

public interface IFavourites
{
    Task Favourite(Track track);
    Task<bool> IsFavourite(string artist, string title);
    Task Unfavourite(Track track);
}