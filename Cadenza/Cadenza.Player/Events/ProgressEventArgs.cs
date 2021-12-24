namespace Cadenza.Player
{
    public delegate Task ProgressEventHandler(object sender, ProgressEventArgs e);
    public delegate Task SyncProgressEventHandler(object sender, SyncProgressEventArgs e);

    public class ProgressEventArgs : EventArgs
    {
        public string Message { get; set; }
        public bool Completed { get; set; }
        public bool Cancelled { get; set; }
    }

    public class SyncProgressEventArgs : EventArgs
    {
        public LibrarySource Source { get; set; }
        public string Message { get; set; }
        public bool Completed { get; set; }
        public bool Cancelled { get; set; }
    }
}
