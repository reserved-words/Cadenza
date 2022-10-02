namespace Cadenza.Web.Common.Interfaces.Play;

public interface IPlayCoordinator
{
    Task LoadingPlaylist();
    Task Play(PlaylistDefinition playlistDefinition);
}
