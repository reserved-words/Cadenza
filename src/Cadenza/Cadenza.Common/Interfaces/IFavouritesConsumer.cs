using Cadenza.Domain;

namespace Cadenza.Common;

public interface IFavouritesConsumer
{
    Task<bool> IsFavourite(string artist, string title);
}