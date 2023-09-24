namespace Cadenza.Web.Common.Tasks;

public class StartupTask
{
    public Connector Connector { get; set; }
    public object InitialAction { get; set; }

    public string Title { get; set; }
}
