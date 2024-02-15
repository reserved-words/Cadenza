using Cadenza.Common.Enums;
using Cadenza.Web.Common.Enums;
using MudBlazor;
using static MudBlazor.Icons;

namespace Cadenza;

public static class Icon
{
    public const string BandCamp = "fab fa-bandcamp";
    public const string LastFm = "fab fa-lastfm-square";
    public const string Wikipedia = "fab fa-wikipedia-w";

    public static readonly string Add = Material.Filled.Add;
    public static readonly string Archive = Material.Filled.Archive;
    public static readonly string GridView = Material.Filled.GridView;
    public static readonly string MarkAsUnread = Material.Filled.MarkAsUnread;
    public static readonly string MarkAsRead = Material.Filled.MarkChatRead;
    public static readonly string Delete = Material.Filled.DeleteForever;
    public static readonly string DropdownMenu = Material.Filled.MoreVert;
    public static readonly string Finish = Material.Filled.Check;
    public static readonly string Hide = Material.Filled.CloudDownload;
    public static readonly string More = Material.Filled.FileDownload;
    public static readonly string Menu = Material.Filled.Menu;
    public static readonly string PutAside = Material.Filled.Pause;
    public static readonly string Queue = Material.Filled.PlaylistAdd;
    public static readonly string ReadNow = Material.Filled.PlayArrow;
    public static readonly string Remove = Material.Filled.Remove;
    public static readonly string Search = Material.Filled.Search;
    public static readonly string SkipNext = Material.Rounded.SkipNext;
    public static readonly string SkipPrevious = Material.Rounded.SkipPrevious;
    public static readonly string TableView = Material.Filled.TableView;
    public static readonly string Unarchive = Material.Filled.Unarchive;
    public static readonly string Unhide = Material.Filled.Upgrade;
    public static readonly string Unqueue = Material.Filled.RemoveFromQueue;
    public static readonly string View = Material.Filled.LibraryBooks;

    public static string GetIcon(this Tab tab)
    {
        return tab switch
        {
            Tab.CurrentTrack => Material.Filled.PlaylistPlay,
            Tab.Dashboard => Material.Filled.Dashboard,
            Tab.Home => Material.Filled.Home,
            Tab.Library => "fas fa-folder-tree",
            Tab.Search => Material.Filled.Search,
            Tab.Edit => Material.Filled.Edit,
            Tab.Settings => Material.Filled.Settings,
            _ => throw new NotImplementedException(),
        };
    }

    public static string GetIcon(this PlayerItemType type)
    {
        return type switch
        {
            PlayerItemType.Library => Tab.Library.GetIcon(),
            PlayerItemType.Album => "fas fa-compact-disc",
            PlayerItemType.Artist => "fas fa-users",
            PlayerItemType.Genre => "fas fa-boxes",
            PlayerItemType.Grouping => "fas fa-box",
            PlayerItemType.Tag => "fas fa-tag",
            PlayerItemType.Track => "fas fa-file-audio",
            _ => throw new NotImplementedException(),
        };
    }
}