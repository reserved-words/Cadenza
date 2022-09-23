using Cadenza.API.Common.Model.Json;
using Cadenza.Domain;

namespace Cadenza.API.Database.Interfaces;

internal interface ITrackConverter
{
    JsonTrack ToJsonModel(TrackInfo track);
    TrackInfo ToAppModel(JsonTrack track, ICollection<JsonArtist> artists);
}
