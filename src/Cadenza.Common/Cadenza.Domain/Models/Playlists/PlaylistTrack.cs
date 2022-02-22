namespace Cadenza.Domain
{
    public class PlaylistTrack
    {
        public int Order { get; set; }
        public string TrackId { get; set; }
        public string Title { get; set; }
        public string ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string AlbumId { get; set; }
        public int DurationSeconds { get; set; }
    }
}
