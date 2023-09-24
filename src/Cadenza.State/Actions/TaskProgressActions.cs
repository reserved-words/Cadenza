using Cadenza.Web.Common.Tasks;

namespace Cadenza.Web.Common.Events;

public record StartupTaskProgressAction(Connector Connector, string Message, TaskState State);