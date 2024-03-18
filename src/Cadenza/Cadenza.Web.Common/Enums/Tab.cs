namespace Cadenza.Web.Common.Enums;

public enum Tab
{
    Default,
    Home,
    Dashboard,
    [Display(Name = "Recent Albums")]
    RecentAlbums,
    [Display(Name = "Recent Tracks")]
    RecentTracks,
    Charts,
    [Display(Name = "Playing")]
    CurrentTrack,
    Library,
    Search,
    Edit,
    Settings
}