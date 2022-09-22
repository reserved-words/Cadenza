using Cadenza.API.Common.Model.Json;
using Cadenza.Domain;

namespace Cadenza.API.Common.Interfaces.Converters;

public interface ITrackConverter
{
    JsonTrack ToJsonModel(TrackInfo track);
    TrackInfo ToAppModel(JsonTrack track, ICollection<JsonArtist> artists);
}
