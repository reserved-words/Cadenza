namespace Cadenza.Web.Components;

public static class Extensions
{
    public static string GetIcon(this PlayerItemType type)
    {
        return type switch
        {
            PlayerItemType.Album => "fas fa-compact-disc",
            PlayerItemType.Artist => "fas fa-users",
            PlayerItemType.Genre => "fas fa-boxes",
            PlayerItemType.Grouping => "fas fa-box",
            PlayerItemType.Tag => "fas fa-tag",
            PlayerItemType.Track => "fas fa-music",
            _ => throw new NotImplementedException(),
        };
    }

    public static string WithLineBreaks(this string text)
    {
        return text?.Replace("\n", "<br />", StringComparison.InvariantCultureIgnoreCase);
    }

    public static string DiscTrackNo(this AlbumTrack track)
    {
        return $"{track.DiscNo}:{track.TrackNo}";
    }

    public static string Duration(this int durationSeconds)
    {
        var duration = TimeSpan.FromSeconds(durationSeconds);

        return duration.TotalHours > 1
            ? duration.ToString(@"hh\:mm\:ss")
            : duration.ToString(@"mm\:ss");
    }

    public static string Duration(this AlbumTrack track)
    {
        return track.DurationSeconds.Duration();
    }

    public static string Duration(this TrackInfo track)
    {
        return track.DurationSeconds.Duration();
    }
}