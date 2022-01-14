using Cadenza.Domain;

namespace Cadenza.Database
{
    internal static class DbHelper
    {

        internal static List<PlayTrack> GetPlayTracks(this LibraryDb db, PlayTrackType type, string id, LibrarySource source)
        {
            var playTrackId = GetId(type, id, source);

            var tracks = db.PlayTracks
                .SingleOrDefault(a => a.Id == playTrackId);

            if (tracks == null)
                return new List<PlayTrack>();

            return tracks.Tracks
                .Split(",")
                .Select(t => new PlayTrack
                {
                    Id = t,
                    Source = source
                })
                .ToList();
        }

        internal static string GetId(PlayTrackType type, string id, LibrarySource source)
        {
            return $"{type}|{id}|{source}";
        }
    }
}
