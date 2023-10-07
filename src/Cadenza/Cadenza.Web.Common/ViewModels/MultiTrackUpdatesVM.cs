namespace Cadenza.Web.Common.ViewModels;

public class MultiTrackUpdatesVM
{
    public List<string> TrackIds { get; set; }
    public List<PropertyUpdateVM> Updates { get; set; } = new();
}
