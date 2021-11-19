namespace Cadenza.Player;

public delegate Task ArtistUpdatedEventHandler(object sender, ArtistUpdatedEventArgs e);

public class ArtistUpdatedEventArgs : EventArgs
{
    public ArtistUpdatedEventArgs(ArtistUpdate update)
    {
        Update = update;
    }

    public ArtistUpdate Update { get; }
}