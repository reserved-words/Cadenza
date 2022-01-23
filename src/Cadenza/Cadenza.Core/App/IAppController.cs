namespace Cadenza.Core;

public interface IAppController
{
    void Initialise();
    Task Pause();
    Task Resume();
    Task SkipNext();
    Task SkipPrevious();
    Task Play(PlaylistDefinition playlistDefinition);
}
