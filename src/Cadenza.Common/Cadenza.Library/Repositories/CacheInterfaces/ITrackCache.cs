namespace Cadenza.Library;

public interface ITrackCache : ITrackRepository
{
    Task Populate(FullLibrary library);
}
