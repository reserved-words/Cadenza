using Cadenza.Web.Core.Model.Logging;
using LogLevel = Cadenza.Web.Core.Model.Logging.LogLevel;

namespace Cadenza.UI.Components.SystemInfo;

public partial class SystemErrorsBase : ComponentBase
{
    public List<ErrorLog> Items { get; set; } = new();

    public ErrorLog SelectedItem { get; set; }

    protected override void OnInitialized()
    {
        var random = new Random();

        for (var i = 0; i < 20; i++)
        {
            var loggedAt = DateTime.Now.AddMinutes(-1 * random.Next(1, 60));
            var logLevel = (LogLevel)random.Next(0, 4);
            var message = $"Message {i}";
            var stackTrace = logLevel == LogLevel.Error
                ? "askjh afo hawfkasfhlwahkahk sjfaf"
                : null;
            var application = random.Next(0, 1) == 1
                ? "Whip Sync Service"
                : "Whip Local API";

            Items.Add(new ErrorLog
            {
                LoggedAt = loggedAt,
                Application = application,
                Level = logLevel,
                Message = message,
                StackTrace = stackTrace
            });
        }


    }

    protected bool FilterFunc(ErrorLog log, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (log.Application.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (log.Level.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (log.Message.Contains(searchString))
            return true;

        return false;
    }
}