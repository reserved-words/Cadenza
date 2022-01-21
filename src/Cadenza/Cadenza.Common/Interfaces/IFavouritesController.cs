using Cadenza.Domain;

namespace Cadenza.Common;

public interface IFavouritesController
{
    Task Favourite(TrackSummary track);
    Task Unfavourite(TrackSummary track);
}
