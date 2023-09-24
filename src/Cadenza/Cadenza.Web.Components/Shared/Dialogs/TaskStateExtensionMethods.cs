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
            return state == TaskState.Running;
        }

        public static bool Ended(this TaskState state)
        {
            return state == TaskState.Errored
                || state == TaskState.Completed;
        }
    }
}
