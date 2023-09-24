namespace Cadenza.Web.Common.Tasks;

public record StartupTask(Connector Connector, string Title, object InitialAction);
