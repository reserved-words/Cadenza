namespace Cadenza.Common;

public interface ITimeSpanConverter
{
    string TimeSpanToString(TimeSpan timeSpan);
    TimeSpan StringToTimeSpan(string timeSpan);
    int TimeSpanToSeconds(TimeSpan timeSpan);
    TimeSpan SecondsToTimeSpan(int seconds);
}
