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

    public void UpdateTags(string filepath, List<PropertyUpdate> updates)
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
            case ItemProperty.Artwork:
                trackData.Album.Artwork = _imageConverter.GetImageFromBase64Url(value);
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
            default:
                throw new NotImplementedException();
        }
    }
}