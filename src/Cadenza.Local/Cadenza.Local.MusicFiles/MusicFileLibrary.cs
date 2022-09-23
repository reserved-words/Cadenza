using Cadenza.Domain;
using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Model;
using Cadenza.Local.MusicFiles.Interfaces;
using Cadenza.Local.MusicFiles.Model;

namespace Cadenza.Local.MusicFiles;

internal class MusicFileLibrary : IMusicFileLibrary
{
    private readonly IId3TagsService _id3Service;
    private readonly IId3ToLocalConverter _converter;
    private readonly ICommentProcessor _commentProcessor;

    public MusicFileLibrary(IId3ToLocalConverter converter, IId3TagsService id3Service, ICommentProcessor commentProcessor)
    {
        _converter = converter;
        _id3Service = id3Service;
        _commentProcessor = commentProcessor;
    }

    public (byte[] Bytes, string Type) GetArtwork(string filepath)
    {
        return _id3Service.GetArtwork(filepath);
    }

    public LocalFileData GetFileData(string filepath)
    {
        var id3Track = _id3Service.GetId3Data(filepath);
        var trackArtist = _converter.ConvertTrackArtist(id3Track);
        var albumArtist = _converter.ConvertAlbumArtist(id3Track);
        var track = _converter.ConvertTrack(id3Track);
        var album = _converter.ConvertAlbum(id3Track);
        var albumTrackLink = _converter.ConvertAlbumTrackLink(track.Id, id3Track);

        return new LocalFileData
        {
            Album = album,
            AlbumArtist = albumArtist,
            AlbumTrackLink = albumTrackLink,
            TrackArtist = trackArtist,
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
