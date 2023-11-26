namespace Cadenza.Web.Model;

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
