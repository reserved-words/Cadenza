namespace Cadenza.Library;

public interface IBaseTrackRepository : ITrackRepository
{
    Task Populate();
}
