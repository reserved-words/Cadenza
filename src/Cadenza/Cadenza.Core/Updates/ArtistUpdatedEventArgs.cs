namespace Cadenza.Core.Updates;

public delegate Task ArtistUpdatedEventHandler(object sender, ArtistUpdatedEventArgs e);

public class ArtistUpdatedEventArgs : EventArgs
{
    public ArtistUpdatedEventArgs(ArtistInfo artist, List<ItemProperty> updatedProperties)
    {
        Artist = artist;
        UpdatedProperties = updatedProperties;
    }

    public ArtistInfo Artist { get; }
    public List<ItemProperty> UpdatedProperties { get; }
}