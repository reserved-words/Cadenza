using Cadenza.Common;
using IndexedDB.Blazor;

namespace Cadenza.Database;

public class TrackRepository : ITrackRepository
{
    private readonly IIndexedDbFactory _dbFactory;

    public TrackRepository(IIndexedDbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<bool> IsPopulated()
    {
        using (var db = await _dbFactory.Create<LibraryDb>())
        {
            var info = db.Info.SingleOrDefault();
            if (info == null)
            {
                info = new DbInfo();
                db.Info.Add(info);
                await db.SaveChanges();
            }
            return info.AreTracksPopulated;
        }
    }

    public async Task AddTracks(ICollection<Track> tracks)
    {
        using (var db = await _dbFactory.Create<LibraryDb>())
        {
            foreach (var track in tracks)
            {
                //db.Tracks.Add(new DbTrack
                //{
                //    Id = track.Id,
                //    Title = track.Title,
                //    ArtistName = track.Artist.Name,
                //    ArtistId = track.ArtistId,
                //    DurationSeconds = track.DurationSeconds,
                //    Source = track.Source
                //});

            }

            await db.SaveChanges();



            //using (var db = await _dbFactory.Create<LibraryDb>())
            //{
            //    var firstPerson = db.People.First();
            //    db.People.Remove(firstPerson);
            //    await db.SaveChanges();
            //}

            //using (var db = await _dbFactory.Create<LibraryDb>())
            //{
            //    var personWithId1 = db.People.Single(x => x.Id == 1);
            //    personWithId1.FirstName = "This is 100% a first name";
            //    await db.SaveChanges();
            //}
        }
    }

    public async Task<List<Track>> GetTracks()
    {
        using (var db = await _dbFactory.Create<LibraryDb>())
        {
            return db.Tracks.Select(track => new Track
            {
                Id = track.Id,
                Title = track.Title,
//                ArtistName = track.ArtistName,
                ArtistId = track.ArtistId,
                DurationSeconds = track.DurationSeconds,
                Source = track.Source
            })
            .ToList();
        }
    }

    public Task<TrackSummary> GetSummary(string id)
    {
        throw new NotImplementedException();
    }

    public Task<TrackDetail> GetDetail(string id)
    {
        throw new NotImplementedException();
    }
}
