namespace Cadenza.Local.API.Files.Services;

internal class Id3Updater : IId3Updater
{
    private readonly IId3TagsService _id3Service;
    private readonly ICommentProcessor _commentProcessor;
    private readonly IImageConverter _imageConverter;

    public Id3Updater(IId3TagsService id3Service, ICommentProcessor commentProcessor, IImageConverter imageConverter)
    {
        _id3Service = id3Service;
        _commentProcessor = commentProcessor;
        _imageConverter = imageConverter;
    }

    public void UpdateTags(string filepath, List<PropertyUpdateDTO> updates)
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
            case ItemProperty.AlbumArtwork:
                trackData.Album.Artwork = _imageConverter.GetImageFromBase64Url(value);
                break;
            case ItemProperty.AlbumReleaseType:
                trackData.Album.ReleaseType = value;
                break;
            case ItemProperty.AlbumTags:
                commentData.AlbumTags = value;
                break;
            case ItemProperty.AlbumTitle:
                trackData.Album.Title = value;
                break;
            case ItemProperty.AlbumReleaseYear:
                trackData.Album.Year = value;
                break;
            case ItemProperty.ArtistCity:
                commentData.City = value;
                break;
            case ItemProperty.ArtistCountry:
                commentData.Country = value;
                break;
            case ItemProperty.ArtistGenre:
                trackData.Artist.Genre = value;
                break;
            case ItemProperty.ArtistGrouping:
                trackData.Artist.Grouping = value;
                break;
            case ItemProperty.ArtistImage:
                trackData.Artist.Image = _imageConverter.GetImageFromBase64Url(value);
                break;
            case ItemProperty.ArtistState:
                commentData.State = value;
                break;
            case ItemProperty.ArtistTags:
                commentData.ArtistTags = value;
                break;
            case ItemProperty.TrackDiscNo:
                trackData.Disc.DiscNo = int.Parse(value);
                break;
            //case ItemProperty.DiscTrackCount:
            //    trackData.Disc.TrackCount = int.Parse(value);
            //    break;
            case ItemProperty.TrackLyrics:
                trackData.Track.Lyrics = value;
                break;
            case ItemProperty.TrackNo:
                trackData.Track.TrackNo = int.Parse(value);
                break;
            case ItemProperty.TrackTags:
                commentData.TrackTags = value;
                break;
            case ItemProperty.TrackTitle:
                trackData.Track.Title = value;
                break;
            case ItemProperty.TrackYear:
                commentData.TrackYear = value;
                break;
            default:
                throw new NotImplementedException();
        }
    }
}