namespace Cadenza.Library;

public interface IBasePlayTrackRepository : IPlayTrackRepository
{
    Task Populate();
}
