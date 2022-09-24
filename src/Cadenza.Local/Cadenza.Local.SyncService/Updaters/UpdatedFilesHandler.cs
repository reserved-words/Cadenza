using Cadenza.Domain.Enums;
using Cadenza.Domain.Models.Album;
using Cadenza.Domain.Models.Artist;
using Cadenza.Domain.Models.Track;
using Cadenza.Local.Common.Interfaces;
using Cadenza.Utilities.Interfaces;

namespace Cadenza.Local.SyncService.Updaters;

public class UpdatedFilesHandler : IUpdateService
{
    private readonly IBase64Converter _base64Converter;
    private readonly IDatabaseRepository _database;
    private readonly IMusicFileLibrary _musicFileLibrary;
    private readonly IMusicDirectory _musicDirectory;

    public UpdatedFilesHandler(IMusicFileLibrary musicFileLibrary, IBase64Converter base64Converter, IDatabaseRepository database, IMusicDirectory musicDirectory)
    {
        _musicFileLibrary = musicFileLibrary;
        _base64Converter = base64Converter;
        _database = database;
        _musicDirectory = musicDirectory;
    }

    public async Task Run()
    {
        var dateLastUpdated = await _database.GetDateLastUpdated();

        var filesUpdatedRemotely = await _database.GetUpdates();

        var newUpdateTime = DateTime.Now;

        var filesUpdatedLocally = await _musicDirectory.GetModifiedFiles(dateLastUpdated);

        var updatedTracks = new List<UpdatedTrack>();
        var updatedAlbums = new List<AlbumInfo>();
        var updatedArtists = new List<ArtistInfo>();

        foreach (var file in filesUpdatedLocally)
        {
            // Check if updated remotely
            var id = _base64Converter.ToBase64(file.Path);

            var remoteUpdates = filesUpdatedRemotely
                .Where(s => s.ItemType == LibraryItemType.Track && s.Id == id)
                .ToList();

            if (remoteUpdates.Any())
            {
                var remoteUpdatesSinceLocalUpdate = remoteUpdates.Where(u => u.TimeUpdated > dateLastUpdated).ToList();

                if (remoteUpdatesSinceLocalUpdate.Any())
                {
                    _musicFileLibrary.UpdateFileData(file.Path, remoteUpdatesSinceLocalUpdate);
                }

                await _database.MarkUpdated(LibraryItemType.Track, id);

                foreach (var remoteUpdate in remoteUpdates)
                {
                    filesUpdatedRemotely.Remove(remoteUpdate);
                }
            }

            // NOTE - need to check for artist (x2) and album updates as well, not just trackW

            // Get full tag data for file
            var data = _musicFileLibrary.GetFileData(file.Path);

            updatedTracks.Add(new UpdatedTrack(data.Track, data.AlbumTrack));

            if (!updatedArtists.Any(a => a.Id == data.Artist.Id)){
                updatedArtists.Add(data.Artist);
            }

            if (data.AlbumArtist.Id != data.Artist.Id && !updatedArtists.Any(a => a.Id == data.AlbumArtist.Id))
            {
                updatedArtists.Add(data.AlbumArtist);
            }

            if (!updatedAlbums.Any(a => a.Id == data.Album.Id))
            {
                updatedAlbums.Add(data.Album);
            }
        }

        foreach (var artist in updatedArtists)
        {
            await _database.AddOrUpdateArtist(artist);
        }

        foreach (var album in updatedAlbums)
        {
            await _database.AddOrUpdateAlbum(album);
        }

        foreach (var track in updatedTracks)
        {
            await _database.AddOrUpdateTrack(track.Track, track.AlbumTrackLink);
        }

        foreach (var remoteUpdate in filesUpdatedRemotely)
        {

        }

        // Carry out any remaining remote updates, mark each as done and remove from updates list



        await _database.SetDateLastUpdated(newUpdateTime);

        //var jsonItems = await _dataAccess.GetAll(LibrarySource.Local);

        //var updatesFetched = DateTime.Now;

        //var filepaths = await _fileFetcher.GetModifiedFiles();

        //foreach (var filepath in filepaths)
        //{
        //    Console.WriteLine($"Modifying file {filepath}");

        //    var data = _musicFileLibrary.GetFileData(filepath);

        //    _organiser.MergeArtist(jsonItems.Artists, data.TrackArtist);

        //    if (data.AlbumArtist.Id != data.TrackArtist.Id)
        //    {
        //        _organiser.MergeArtist(jsonItems.Artists, data.AlbumArtist);
        //    }

        //    _organiser.MergeTrack(jsonItems.Tracks, jsonItems.Artists, data.Track);
        //    _organiser.MergeAlbum(jsonItems.Albums, jsonItems.Artists, data.Album);
        //    _organiser.MergeAlbumTrackLink(jsonItems.AlbumTrackLinks, data.AlbumTrackLink);
        //}

        //_organiser.RemoveOrphanedItems(jsonItems);

        //await _dataAccess.SaveAll(jsonItems, LibrarySource.Local);

        //await _fileFetcher.UpdateTimeModifiedFilesUpdated(updatesFetched);
    }

    public class UpdatedTrack
    {
        public UpdatedTrack(TrackInfo track, AlbumTrackLink albumTrackLink)
        {
            Track = track;
            AlbumTrackLink = albumTrackLink;
        }

        public TrackInfo Track { get; set; }
        public AlbumTrackLink AlbumTrackLink { get; set; }
    }
}