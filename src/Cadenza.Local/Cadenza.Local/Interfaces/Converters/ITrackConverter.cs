namespace Cadenza.Local;

public interface ITrackConverter
{
    JsonTrack ToJsonModel(TrackInfo track);
    TrackInfo ToAppModel(JsonTrack track, ICollection<JsonArtist> artists);
    JsonTrack ToJsonModel(Id3Data id3Data);
}
