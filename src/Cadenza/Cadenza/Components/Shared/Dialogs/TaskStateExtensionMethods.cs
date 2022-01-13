namespace Cadenza.Components.Shared.Dialogs
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
                || state == TaskState.Completing
                || state == TaskState.Cancelling;
        }

        public static bool Ended(this TaskState state)
        {
            return state == TaskState.Cancelled
                || state == TaskState.Errored
                || state == TaskState.Completed
                || state == TaskState.CompletedWithErrors;
        }
    }
}
