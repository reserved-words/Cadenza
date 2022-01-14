using Cadenza.Domain;

namespace Cadenza.Common;

public interface IFavouritesController
{
    Task Favourite(PlayingTrack track);
    Task Unfavourite(PlayingTrack track);
}