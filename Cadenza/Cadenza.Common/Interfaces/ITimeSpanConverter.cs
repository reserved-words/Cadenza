namespace Cadenza.Common;

public interface ITimeSpanConverter
{
    string TimeSpanToString(TimeSpan timeSpan);
    TimeSpan StringToTimeSpan(string timeSpan);
    int StringToSeconds(string timeSpan);
    int TimeSpanToSeconds(TimeSpan timeSpan);
    TimeSpan SecondsToTimeSpan(int seconds);
    string SecondsToString(int seconds);
}
