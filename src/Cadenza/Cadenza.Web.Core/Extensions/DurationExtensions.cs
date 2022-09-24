using System.Globalization;

namespace Cadenza.Web.Core.Extensions;

public static class DurationExtensions
{
    public static string ToDurationDisplay(this int durationSeconds)
    {
        var timeSpan = TimeSpan.FromSeconds(durationSeconds);
        return timeSpan.Hours > 0
            ? timeSpan.ToString(@"hh\:mm\:ss", CultureInfo.CurrentUICulture)
            : timeSpan.ToString(@"mm\:ss", CultureInfo.CurrentUICulture);
    }
}