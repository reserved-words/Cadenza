using System.Globalization;

namespace Cadenza.Common;

public class TimeSpanConverter : ITimeSpanConverter
{
    private const string Format = @"hh\:mm\:ss";

    public string TimeSpanToString(TimeSpan timeSpan)
    {
        return timeSpan.ToString(Format);
    }

    public TimeSpan StringToTimeSpan(string timeSpan)
    {
        return TimeSpan.ParseExact(timeSpan, Format, CultureInfo.InvariantCulture);
    }

    public int StringToSeconds(string timeSpan)
    {
        return TimeSpanToSeconds(StringToTimeSpan(timeSpan));
    }

    public int TimeSpanToSeconds(TimeSpan timeSpan)
    {
        return (int)Math.Ceiling(timeSpan.TotalSeconds);
    }

    public TimeSpan SecondsToTimeSpan(int seconds)
    {
        return TimeSpan.FromSeconds(seconds);
    }

    public string SecondsToString(int seconds)
    {
        return TimeSpanToString(SecondsToTimeSpan(seconds));
    }
}