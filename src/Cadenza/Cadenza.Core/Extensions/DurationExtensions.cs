namespace Cadenza.Core.Extensions;

public static class DurationExtensions
{
    public static string ToDurationDisplay(this int durationSeconds)
    {
        var timeSpan = TimeSpan.FromSeconds(durationSeconds);
        return timeSpan.Hours > 0
            ? timeSpan.ToString(@"hh:\mm:\ss")
            : timeSpan.ToString(@"mm:\ss");
    }
}