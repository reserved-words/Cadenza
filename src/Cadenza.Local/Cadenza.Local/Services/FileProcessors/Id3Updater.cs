using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Model;
using Cadenza.Local.Common.Model.Id3;

namespace Cadenza.Local.Services.FileProcessors;

public class Id3Updater : IId3Updater
{
    private readonly IBase64Converter _base64Converter;
    private readonly ICommentProcessor _commentProcessor;
    private readonly IId3TagsService _id3Service;
    private readonly IDataAccess _dataAccess;

    public Id3Updater(IId3TagsService id3Service, IDataAccess dataAccess, ICommentProcessor commentProcessor, IBase64Converter base64Converter)
    {
        _id3Service = id3Service;
        _dataAccess = dataAccess;
        _commentProcessor = commentProcessor;
        _base64Converter = base64Converter;
    }

    public async Task<List<ItemPropertyUpdateResult>> UpdateAlbum(string id, List<ItemPropertyUpdate> updates)
    {
        var results = GetEmptyResults(updates);
        var tracks = await GetAlbumTrackPaths(id);
        UpdateAllTracks(tracks, results);
        return results;
    }

    public async Task<List<ItemPropertyUpdateResult>> UpdateArtist(string id, List<ItemPropertyUpdate> updates)
    {
        var tracks = await GetArtistTrackPaths(id);
        var results = GetEmptyResults(updates);
        UpdateAllTracks(tracks, results);
        return results;
    }

    public Task<List<ItemPropertyUpdateResult>> UpdateTrack(string id, List<ItemPropertyUpdate> updates)
    {
        var path = _base64Converter.FromBase64(id);
        var results = GetEmptyResults(updates);
        UpdateTrack(path, results);
        return Task.FromResult(results);
    }

    private async Task<List<string>> GetAlbumTrackPaths(string id)
    {
        var tracks = await _dataAccess.GetTracks(LibrarySource.Local);
        return tracks
            .Where(t => t.AlbumId == id)
            .Select(t => t.Path)
            .ToList();
    }

    private async Task<List<string>> GetArtistTrackPaths(string id)
    {
        var tracks = await _dataAccess.GetTracks(LibrarySource.Local);
        return tracks
            .Where(t => t.ArtistId == id)
            .Select(t => t.Path)
            .ToList();
    }

    private List<ItemPropertyUpdateResult> GetEmptyResults(List<ItemPropertyUpdate> updates)
    {
        return updates.Select(u => new ItemPropertyUpdateResult { Update = u }).ToList();
    }

    private void TryUpdate(Id3Data trackData, CommentData commentData, ItemPropertyUpdateResult result)
    {
        try
        {
            Update(trackData, commentData, result.Update.Property, result.Update.UpdatedValue);
            result.Completed = true;
        }
        catch (NotImplementedException)
        {
            result.Completed = false;
        }
        catch (Exception ex)
        {
            result.Completed = false;
            result.Error = ex;
        }
    }

    private void Update(Id3Data trackData, CommentData commentData, ItemProperty ItemProperty, string value)
    {
        switch (ItemProperty)
        {
            case ItemProperty.AlbumTitle:
                trackData.Album.Title = value;
                break;
            case ItemProperty.City:
                commentData.City = value;
                break;
            case ItemProperty.Country:
                commentData.Country = value;
                break;
            case ItemProperty.Genre:
                trackData.Artist.Genre = value;
                break;
            case ItemProperty.Grouping:
                trackData.Artist.Grouping = value;
                break;
            case ItemProperty.Lyrics:
                trackData.Track.Lyrics = value;
                break;
            case ItemProperty.ReleaseType:
                trackData.Album.ReleaseType = value;
                break;
            case ItemProperty.ReleaseYear:
                trackData.Album.Year = value;
                break;
            case ItemProperty.State:
                commentData.State = value;
                break;
            case ItemProperty.TrackTitle:
                trackData.Track.Title = value;
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private void UpdateTrack(string path, List<ItemPropertyUpdateResult> results)
    {
        UpdateAllTracks(new List<string> { path }, results);
    }

    private void UpdateAllTracks(List<string> paths, List<ItemPropertyUpdateResult> results)
    {
        if (!paths.Any())
        {
            results.ForEach(r => r.Completed = true);
            return;
        }

        foreach (var path in paths)
        {
            var data = _id3Service.GetId3Data(path);
            var commentData = _commentProcessor.GetData(data.Track.Comment);

            foreach (var result in results)
            {
                TryUpdate(data, commentData, result);
            }

            data.Track.Comment = _commentProcessor.CreateComment(commentData);

            if (!TrySave(path, data, out Exception error))
            {
                foreach (var result in results.Where(r => r.Completed))
                {
                    result.Completed = false;
                    result.Error = error;
                }
            }
        }
    }

    private bool TrySave(string filepath, Id3Data data, out Exception error)
    {
        error = null;

        try
        {
            _id3Service.SaveId3Data(filepath, data);
            return true;
        }
        catch (Exception ex)
        {
            error = ex;
            return false;
        }
    }
}