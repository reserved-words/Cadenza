namespace Cadenza.Player;

public interface IAppController
{
    void Initialise();
    Task Play(PlaylistDefinition playlistDefinition);
    Task Pause();
    Task Resume();
    Task SkipNext();
    Task SkipPrevious();
}