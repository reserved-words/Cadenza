using Cadenza.Domain.Enums;
using Cadenza.Domain.Models;
using Cadenza.Domain.Models.Track;
using Cadenza.Local.API.Common.Interfaces;
using Cadenza.Local.API.Files.Interfaces;
using Cadenza.Local.API.Files.Model;

namespace Cadenza.Local.API.Files;

internal class MusicFileLibrary : IMusicFileLibrary
{
    private readonly IId3TagsService _id3Service;
    private readonly IId3ToModelConverter _converter;
    private readonly ICommentProcessor _commentProcessor;

    public MusicFileLibrary(IId3ToModelConverter converter, IId3TagsService id3Service, ICommentProcessor commentProcessor)
    {
        _converter = converter;
        _id3Service = id3Service;
        _commentProcessor = commentProcessor;
    }

    public (byte[] Bytes, string Type) GetArtwork(string filepath)
    {
        return _id3Service.GetArtwork(filepath);
    }

    public TrackFull GetFileData(string filepath)
    {
        var id3Track = _id3Service.GetId3Data(filepath);
        var trackArtist = _converter.ConvertTrackArtist(id3Track);
        var albumArtist = _converter.ConvertAlbumArtist(id3Track);
        var track = _converter.ConvertTrack(id3Track);
        var album = _converter.ConvertAlbum(id3Track);
        var albumTrackLink = _converter.ConvertAlbumTrackLink(track.Id, id3Track);

        return new TrackFull
        {
            Album = album,
            AlbumArtist = albumArtist,
            AlbumTrack = albumTrackLink,
            Artist = trackArtist,
            Track = track
        };
    }

    public void UpdateFileData(string filepath, List<ItemPropertyUpdate> updates)
    {
        var data = _id3Service.GetId3Data(filepath);
        var commentData = _commentProcessor.GetData(data.Track.Comment);

        foreach (var update in updates)
        {
            Update(data, commentData, update.Property, update.UpdatedValue);
        }

        data.Track.Comment = _commentProcessor.CreateComment(commentData);

        _id3Service.SaveId3Data(filepath, data);
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
}
