namespace Cadenza.Web.Common.Interfaces.Coordinators;

public interface IFavouritesConsumer
{
    Task<bool> IsFavourite(string artist, string title);
}