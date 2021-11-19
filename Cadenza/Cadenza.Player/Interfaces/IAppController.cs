namespace Cadenza.Player;

public interface IAppController
{
    Task Initialise();
    Task Play(PlaylistDefinition playlistDefinition);
    Task Pause();
    Task Resume();
    Task SkipNext();
    Task SkipPrevious();
    Task UpdateSources(List<LibrarySource> enabledSources);
}