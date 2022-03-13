namespace Cadenza.Library;

public interface IArtistCache : IArtistRepository
{
    Task Populate();
}
