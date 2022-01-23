using Cadenza.Domain;

namespace Cadenza.Database
{
    internal static class DbHelper
    {
        internal static List<BasicTrack> GetPlayTracks(this LibraryDb db, PlayTrackType type, string id)
        {
            var entry = db.GetPlayTracksEntry(type, id);

            if (entry == null)
                return new List<BasicTrack>();

            return JsonConvert.DeserializeObject<List<BasicTrack>>(entry.Tracks);
        }

        internal static DbPlayTracks GetPlayTracksEntry(this LibraryDb db, PlayTrackType type, string id)
        {
            var playTrackId = GetId(type, id);

            var tracks = db.PlayTracks
                .SingleOrDefault(a => a.Id == playTrackId);

            return tracks;
        }

        internal static string GetId(PlayTrackType type, string id)
        {
            return $"{type}|{id}";
        }

        internal static void AddTracks(this LibraryDb db, PlayTrackType type, string id, List<BasicTrack> tracks)
        {
            var playTrackId = GetId(type, id);

            DbPlayTracks entry = null;

            if (type == PlayTrackType.Artist || type == PlayTrackType.All)
            {
                entry = db.GetPlayTracksEntry(type, id);
                if (entry != null)
                {
                    var existingTracks = JsonConvert.DeserializeObject<List<BasicTrack>>(entry.Tracks);
                    var allTracks = existingTracks.Concat(tracks).ToList();
                    entry.Tracks = JsonConvert.SerializeObject(allTracks);
                }
            }

            if (entry == null)
            {
                entry = new DbPlayTracks
                {
                    Id = playTrackId,
                    Tracks = JsonConvert.SerializeObject(tracks)
                };
                db.PlayTracks.Add(entry);
            }
        }

        internal static void AddAlbum(this LibraryDb db, AlbumInfo album)
        {
            db.Albums.Add(new DbAlbum
            {
                Id = album.Id,
                ArtistId = album.ArtistId,
                ArtistName = album.ArtistName,
                Title = album.Title,
                Year = album.Year,
                ReleaseType = album.ReleaseType.ToString(),
                Artwork = album.ArtworkUrl,
                Source = album.Source.ToString()
            });
        }

        internal static void AddArtist(this LibraryDb db, ArtistInfo artist)
        {
            db.Artists.Add(new DbArtist
            {
                Id = artist.Id,
                Name = artist.Name,
                Grouping = artist.Grouping.ToString(),
                Genre = artist.Genre,
                Country = artist.Country,
                State = artist.State,
                City = artist.City
            });
        }

        internal static void AddOrUpdateArtist(this LibraryDb db, ArtistInfo artist)
        {
            var existing = db.Artists.SingleOrDefault(a => a.Id == artist.Id);

            if (existing == null)
            {
                AddArtist(db, artist);
                return;
            }

            existing.Grouping = existing.Grouping == Grouping.None.ToString() ? artist.Grouping.ToString() : existing.Grouping;
            existing.Genre = string.IsNullOrEmpty(existing.Genre) ? artist.Genre : existing.Genre;
            existing.Country = string.IsNullOrEmpty(existing.Country) ? artist.Country : existing.Country;
            existing.State = string.IsNullOrEmpty(existing.State) ? artist.State : existing.State;
            existing.City = string.IsNullOrEmpty(existing.City) ? artist.City : existing.City;
        }
    }
}
