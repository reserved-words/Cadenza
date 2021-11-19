namespace Cadenza.Player;

public class AlbumViewModel
{
    public AlbumViewModel(AlbumFull model)
    {
        Model = model;
    }

    public AlbumFull Model { get; }

    public SplitList<AlbumTrackViewModel> Tracks => Model.Discs.Split(
        d => d.Name,
        d => d.Tracks
            .Select(t => new AlbumTrackViewModel(t))
            .ToList());
}