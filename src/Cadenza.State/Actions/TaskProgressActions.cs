using Cadenza.Web.Common.Tasks;

namespace Cadenza.Web.Common.Events;

public record SubTaskProgressedAction(Connector Connector, string Message, TaskState State);