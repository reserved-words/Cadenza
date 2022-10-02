namespace Cadenza.Web.Common.Interfaces.Play;

public interface IPlayController
{
    Task SkipNext();
    Task SkipPrevious();
    Task LoadingPlaylist();
    Task Play(PlaylistDefinition playlistDefinition);
    Task OnTrackStatusChanged(TrackStatusEventArgs args);
}
