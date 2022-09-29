
namespace Cadenza.Web.Common.Interfaces;

public interface IFavouritesConsumer
{
    Task<bool> IsFavourite(string artist, string title);
}