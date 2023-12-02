namespace Cadenza.Web.Common.ViewModel;

public record RecentTrackVM(
    int Id,
    int AlbumId,
    string Title,
    string Artist,
    string Album,
    bool IsLoved,
    string ImageUrl,
    DateTime Played,
    bool NowPlaying
);
