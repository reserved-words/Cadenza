namespace Cadenza.Local;

public interface IArtistConverter
{
    JsonArtist ToJsonModel(ArtistInfo artist);
    ArtistInfo ToAppModel(JsonArtist artist);
    JsonArtist ToJsonModel(Id3Data data, bool albumArtist);
}
