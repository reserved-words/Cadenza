namespace Cadenza.Domain.Model.Updates;

public class MultiTrackUpdates
{
    public List<string> TrackIds { get; set; }
    public List<PropertyUpdate> Updates { get; set; } = new();
}