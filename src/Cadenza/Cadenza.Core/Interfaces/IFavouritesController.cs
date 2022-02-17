using Cadenza.Domain;

namespace Cadenza.Common;

public interface IFavouritesController
{
    Task Favourite(string artist, string title);
    Task Unfavourite(string artist, string title);
}
