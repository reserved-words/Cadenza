using Cadenza.API.Database.Model;
using Cadenza.Domain.Models.Track;

namespace Cadenza.API.Database.Interfaces;

internal interface ITrackConverter
{
    JsonTrack ToJsonModel(TrackInfo track);
    TrackInfo ToAppModel(JsonTrack track, ICollection<JsonArtist> artists);
}
