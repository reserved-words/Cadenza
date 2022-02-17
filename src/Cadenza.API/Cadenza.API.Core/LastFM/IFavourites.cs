namespace Cadenza.API.Core.LastFM;

public interface IFavourites
{
    Task Favourite(Track track);
    Task<bool> IsFavourite(string artist, string title);
    Task Unfavourite(Track track);
}