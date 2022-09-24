namespace Cadenza.Web.Core.Model.Logging;

public class ErrorLog
{
    public DateTime LoggedAt { get; set; }
    public string Application { get; set; }
    public LogLevel Level { get; set; }
    public string Message { get; set; }
    public string StackTrace { get; set; }
}
