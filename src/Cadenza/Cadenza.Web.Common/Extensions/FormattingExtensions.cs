namespace Cadenza.Web.Common.Extensions;

public static class FormattingExtensions
{
    public static string WithLineBreaks(this string text)
    {
        return text?.Replace("\n", "<br />", StringComparison.InvariantCultureIgnoreCase);
    }

    public static string AlbumDuration(this int durationSeconds)
    {
        var duration = TimeSpan.FromSeconds(durationSeconds);

        return duration.Hours > 1
            ? $"{duration.Hours} hours, {duration.Minutes} minutes"
            : duration.Hours == 1
            ? $"{duration.Hours} hour, {duration.Minutes} minutes"
            : $"{duration.Minutes} minutes";
    }

    public static string TrackDuration(this int durationSeconds)
    {
        var duration = TimeSpan.FromSeconds(durationSeconds);

        return duration.TotalHours > 1
            ? duration.ToString(@"hh\:mm\:ss")
            : duration.ToString(@"mm\:ss");
    }

    public static string TrackDuration(this AlbumTrackVM track)
    {
        return track.DurationSeconds.TrackDuration();
    }

    public static string TrackDuration(this TrackDetailsVM track)
    {
        return track.DurationSeconds.TrackDuration();
    }
}