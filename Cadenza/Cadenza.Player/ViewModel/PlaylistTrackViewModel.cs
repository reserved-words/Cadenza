namespace Cadenza.Player;

public class PlaylistTrackViewModel
{
    public PlaylistTrackViewModel(Track model)
    {
        Model = model;
    }

    public Track Model { get; }

    public bool IsCurrent { get; set; }
}
