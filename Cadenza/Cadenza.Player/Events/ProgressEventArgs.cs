namespace Cadenza.Player
{
    public delegate Task ProgressEventHandler(object sender, ProgressEventArgs e);


    public class ProgressEventArgs : EventArgs
    {
        public string Message { get; set; }
        public bool Completed { get; set; }
        public bool Cancelled { get; set; }
    }
}
