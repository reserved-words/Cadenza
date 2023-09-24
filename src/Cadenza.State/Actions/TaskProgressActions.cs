using Cadenza.Web.Common.Tasks;

namespace Cadenza.Web.Common.Events;

public record SubTaskProgressedAction(string Id, string Message, TaskState State);