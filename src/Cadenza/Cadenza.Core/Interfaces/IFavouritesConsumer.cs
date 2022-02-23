using Cadenza.Domain;

namespace Cadenza.Core.Interfaces;

public interface IFavouritesConsumer
{
    Task<bool> IsFavourite(string artist, string title);
}