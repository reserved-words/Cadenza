namespace Cadenza.Web.Model;

public record RecentTrackVM(
    string Title,
    string Artist,
    string Album,
    bool IsLoved,
    string ImageUrl,
    DateTime Played,
    bool NowPlaying
);
