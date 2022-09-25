using Cadenza.API.Database.Model;
using Cadenza.Domain.Models.Track;

namespace Cadenza.API.Database.Interfaces.Converters;

internal interface ITrackConverter
{
    JsonTrack ToJson(TrackInfo track);
    TrackInfo ToModel(JsonTrack track, ICollection<JsonArtist> artists);
}
