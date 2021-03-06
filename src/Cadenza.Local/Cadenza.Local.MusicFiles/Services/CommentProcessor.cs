using Cadenza.Local.MusicFiles.Extensions;
using Cadenza.Local.MusicFiles.Interfaces;
using Cadenza.Local.MusicFiles.Model;
using Cadenza.Utilities;
using System.Xml.Linq;

namespace Cadenza.Local.MusicFiles.Services;

internal class CommentProcessor : ICommentProcessor
{
    private readonly IJsonConverter _jsonConverter;

    public CommentProcessor(IJsonConverter jsonConverter)
    {
        _jsonConverter = jsonConverter;
    }

    private const string TrackYear = "track_year";
    private const string Country = "country";
    private const string State = "state";
    private const string City = "city";
    private const string Tags = "tags";
    private const string Website = "website";
    private const string Twitter = "twitter";
    private const string Instrumental = "instrumental";
    private const string True = "true";

    private const char TagDelimiter = '|';

    public CommentData GetData(string comment)
    {
        comment = comment?.Trim() ?? "";

        if (comment.StartsWith("{"))
        {
            return _jsonConverter.Deserialize<CommentData>(comment);
        }

        var data = new CommentData();

        XDocument xml;
        if (TryParseXml(comment, out xml))
        {
            data.Tags = xml.Root.GetValue(Tags)
                .Split(new[] { TagDelimiter }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            data.City = xml.Root.GetValue(City);
            data.State = xml.Root.GetValue(State);
            data.Country = xml.Root.GetValue(Country);

            data.Instrumental = xml.Root.GetValue(Instrumental) == True;
            data.TrackYear = xml.Root.GetValue(TrackYear);
            data.Website = xml.Root.GetValue(Website);
            data.Twitter = xml.Root.GetValue(Twitter);
        }

        return data;
    }

    private static bool TryParseXml(string xmlString, out XDocument xml)
    {
        try
        {
            xml = XDocument.Parse(xmlString);
            return true;
        }
        catch (Exception)
        {
            xml = null;
            return false;
        }
    }

    public string CreateComment(CommentData commentData)
    {
        return _jsonConverter.Serialize(commentData);
    }
}
