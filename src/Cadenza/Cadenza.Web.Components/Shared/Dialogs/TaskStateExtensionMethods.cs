namespace Cadenza.Web.Components.Shared.Dialogs
{
    public static class TaskStateExtensionMethods
    {
        public static bool Started(this TaskState state)
        {
            return state != TaskState.None;
        }

        public static bool InProgress(this TaskState state)
        {
            return state == TaskState.Starting
                || state == TaskState.Running
                || state == TaskState.Completing;
        }

        public static bool Ended(this TaskState state)
        {
            return state == TaskState.Errored
                || state == TaskState.Completed
                || state == TaskState.CompletedWithErrors;
        }
    }
}
