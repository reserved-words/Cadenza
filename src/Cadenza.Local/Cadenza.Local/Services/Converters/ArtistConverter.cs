using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Interfaces.Converters;
using Cadenza.Local.Common.Model;
using Cadenza.Local.Common.Model.Id3;
using Cadenza.Local.Common.Model.Json;

namespace Cadenza.Local;

public class ArtistConverter : IArtistConverter
{
    private readonly IIdGenerator _idGenerator;
    private readonly ICommentProcessor _commentProcessor;

    public ArtistConverter(ICommentProcessor commentProcessor, IIdGenerator idGenerator)
    {
        _commentProcessor = commentProcessor;
        _idGenerator = idGenerator;
    }

    public ArtistInfo ToAppModel(JsonArtist artist)
    {
        return new ArtistInfo
        {
            Id = artist.Id,
            Name = artist.Name,
            Grouping = artist.Grouping.Parse<Grouping>(),
            Genre = artist.Genre,
            City = artist.City,
            State = artist.State,
            Country = artist.Country,
            Links = GetLinks(artist.Links)
        };
    }

    public JsonArtist ToJsonModel(ArtistInfo artist)
    {
        return new JsonArtist
        {
            Id = artist.Id,
            Name = artist.Name,
            Grouping = artist.Grouping.ToString(),
            Genre = Nullify(artist.Genre),
            City = Nullify(artist.City),
            State = Nullify(artist.State),
            Country = Nullify(artist.Country),
            Links = GetLinks(artist)
        };
    }

    public JsonArtist ToJsonModel(Id3Data data, bool albumArtist)
    {
        if (albumArtist)
        {
            return new JsonArtist
            {
                Id = _idGenerator.GenerateId(data.Album.ArtistName),
                Name = data.Album.ArtistName,
                Links = new List<JsonLink>()
            };
        }

        var commentData = _commentProcessor.GetData(data.Track.Comment);

        return new JsonArtist
        {
            Id = _idGenerator.GenerateId(data.Artist.Name),
            Name = data.Artist.Name,
            Grouping = data.Artist.Grouping.ToString(),
            Genre = Nullify(data.Artist.Genre),
            City = Nullify(commentData.City),
            State = Nullify(commentData.State),
            Country = Nullify(commentData.Country),
            Links = GetLinks(data.Artist.Name, commentData)
        };
    }

    private string Nullify(string text)
    {
        return string.IsNullOrWhiteSpace(text)
            ? null
            : text;
    }

    private List<JsonLink> GetLinks(string artistName, CommentData commentData)
    {
        var links = new List<JsonLink>();

        AddIfNotEmpty(links, LinkType.BandCamp, commentData.BandCamp, artistName);
        AddIfNotEmpty(links, LinkType.BandsInTown, commentData.BandsInTown, artistName);
        AddIfNotEmpty(links, LinkType.Facebook, commentData.Facebook, artistName);
        AddIfNotEmpty(links, LinkType.LastFm, commentData.LastFm, artistName);
        AddIfNotEmpty(links, LinkType.Twitter, commentData.Twitter, artistName);
        AddIfNotEmpty(links, LinkType.Wikipedia, commentData.Wikipedia, artistName);
        AddIfNotEmpty(links, LinkType.YouTube, commentData.YouTube, artistName);

        if (!links.Any())
            return null;

        return links;
    }

    private void AddIfNotEmpty(List<JsonLink> links, LinkType linkType, string linkName, string artistName)
    {
        if (string.IsNullOrWhiteSpace(linkName))
            return;

        links.Add(new JsonLink { LinkType = linkType.ToString(), Name = linkName });
    }

    private List<JsonLink> GetLinks(ArtistInfo a)
    {
        var nonDefaultNames = a.Links
            .Where(lnk => string.IsNullOrWhiteSpace(a.Name))
            .ToList();

        if (!nonDefaultNames.Any())
            return null;

        return nonDefaultNames
            .Select(lnk => new JsonLink
            {
                LinkType = lnk.Type.ToString(),
                Name = lnk.Name
            })
            .ToList();
    }

    private List<Link> GetLinks(List<JsonLink> links)
    {
        return links == null
            ? new List<Link>()
            : links
                .Select(lnk => new Link(lnk.LinkType.Parse<LinkType>(), lnk.Name))
                .ToList();
    }
}