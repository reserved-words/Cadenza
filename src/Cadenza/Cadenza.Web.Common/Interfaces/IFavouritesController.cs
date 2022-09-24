using Cadenza.Domain;

namespace Cadenza.Web.Common.Interfaces;

public interface IFavouritesController
{
    Task Favourite(string artist, string title);
    Task Unfavourite(string artist, string title);
}
