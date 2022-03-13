namespace Cadenza.Core.Updates;

public delegate Task ArtistUpdatedEventHandler(object sender, ArtistUpdatedEventArgs e);

public class ArtistUpdatedEventArgs : EventArgs
{
    public ArtistUpdatedEventArgs(ArtistInfo artist)
    {
        Artist = artist;
    }

    public ArtistInfo Artist { get; }
}