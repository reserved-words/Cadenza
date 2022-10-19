using LFM_Track = Cadenza.Common.Domain.Model.LastFm.LFM_Track;

namespace Cadenza.API.Interfaces.LastFm;

public interface IFavourites
{
    Task Favourite(LFM_Track track);
    Task<bool> IsFavourite(string artist, string title);
    Task Unfavourite(LFM_Track track);
}