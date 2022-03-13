namespace Cadenza.Library;

public interface IPlayTrackCache : IPlayTrackRepository
{
    Task Populate();
}
