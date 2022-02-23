using Cadenza.Domain;

namespace Cadenza.Core.Interfaces;

public interface IFavouritesController
{
    Task Favourite(string artist, string title);
    Task Unfavourite(string artist, string title);
}
