namespace Cadenza.Common;

public interface IFavouritesConsumer
{
    Task<bool> IsFavourite(PlayingTrack track);
}