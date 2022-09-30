namespace Cadenza.Web.Components.Shared.Dialogs
{
    public class SubTaskProgress
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public TaskState State { get; set; }

        public bool Started => State.Started();
        public bool InProgress => State.InProgress();
        public bool Ended => State.Ended();
        public bool Errored => State == TaskState.Errored;
    }
}
