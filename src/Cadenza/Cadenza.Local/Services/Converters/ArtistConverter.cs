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

        AddIfNotDefault(links, LinkType.BandCamp, commentData.BandCamp, artistName);
        AddIfNotDefault(links, LinkType.BandsInTown, commentData.BandsInTown, artistName);
        AddIfNotDefault(links, LinkType.Facebook, commentData.Facebook, artistName);
        AddIfNotDefault(links, LinkType.LastFm, commentData.LastFm, artistName);
        AddIfNotDefault(links, LinkType.Twitter, commentData.Twitter, artistName);
        AddIfNotDefault(links, LinkType.Wikipedia, commentData.Wikipedia, artistName);
        AddIfNotDefault(links, LinkType.YouTube, commentData.YouTube, artistName);

        if (!links.Any())
            return null;

        return links;
    }

    private void AddIfNotDefault(List<JsonLink> links, LinkType linkType, string linkName, string artistName)
    {
        if (string.IsNullOrWhiteSpace(linkName))
            return;

        if (IsDefaultName(artistName, linkName, linkType))
            return;

        links.Add(new JsonLink { LinkType = linkType.ToString(), Name = linkName });
    }

    private List<JsonLink> GetLinks(ArtistInfo a)
    {
        var nonDefaultNames = a.Links
            .Where(lnk => !IsDefaultName(a.Name, lnk.Name, lnk.Type))
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

    private bool IsDefaultName(string artistName, string linkName, LinkType type)
    {
        return linkName == type.GetDefault(artistName);
    }
}