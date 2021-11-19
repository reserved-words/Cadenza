namespace Cadenza.Player;

public class AlbumTrackViewModel
{
    public AlbumTrackViewModel(AlbumTrack model)
    {
        Model = model;
    }

    public AlbumTrack Model { get; }

    public bool IsCurrent { get; set; }
}
