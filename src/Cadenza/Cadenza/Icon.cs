namespace Cadenza;

public static class Icon
{
    public static string Album = "fas fa-compact-disc";
    public static string Artist = "fas fa-users";
    public static string Dashboard = "fa-solid fa-chart-column";
    public static string Delete = "fa-solid fa-trash-can";
    public static string Edit = "fa-solid fa-pencil";
    public static string Favourite = "fa-solid fa-heart";
    public static string Genre = "fas fa-boxes";
    public static string Grouping = "fas fa-box";
    public static string Home = "fa-solid fa-house";
    public static string Info = "fa-solid fa-circle-info";
    public static string Library = "fa-solid fa-folder-tree";
    public static string NotFavourite = "fa-regular fa-heart";
    public static string Pause = "fa-solid fa-pause";
    public static string Play = "fa-solid fa-play";
    public static string RightArrow = Icons.Material.Filled.KeyboardArrowRight;
    public static string Search = "fa-solid fa-magnifying-glass";
    public static string Settings = "fa-solid fa-gear";
    public static string Shuffle = "fa-solid fa-shuffle";
    public static string SkipNext = "fa-solid fa-forward-step";
    public static string SkipPrevious = "fa-solid fa-backward-step";
    public static string Playing = "fa-solid fa-volume-high";
    public static string RecentTracks = "fa-solid fa-history";
    public static string Tag = "fas fa-tag";
    public static string Track = "fas fa-file-audio";

    public static string GetIcon(this Tab tab)
    {
        return tab switch
        {
            Tab.CurrentTrack => Playing,
            Tab.Dashboard => Dashboard,
            Tab.Home => Home,
            Tab.Library => Library,
            Tab.RecentAlbums => Album,
            Tab.RecentTracks => RecentTracks,
            Tab.Charts => Dashboard,
            Tab.Search => Search,
            Tab.Edit => Edit,
            Tab.Settings => Settings,
            _ => throw new NotImplementedException(),
        };
    }

    public static string GetIcon(this PlayerItemType type)
    {
        return type switch
        {
            PlayerItemType.Library => Library,
            PlayerItemType.Album => Album,
            PlayerItemType.Artist => Artist,
            PlayerItemType.Genre => Genre,
            PlayerItemType.Grouping => Grouping,
            PlayerItemType.Tag => Tag,
            PlayerItemType.Track => Track,
            _ => throw new NotImplementedException(),
        };
    }
}