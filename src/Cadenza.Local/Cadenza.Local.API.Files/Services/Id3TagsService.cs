using Cadenza.Common.Domain.Model;
using TagLib;

namespace Cadenza.Local.API.Files.Services;

internal class Id3TagsService : IId3TagsService
{
    private static Dictionary<string, string> _artistNameExceptions = new Dictionary<string, string>
    {
        { "", "No Artist" },
        { "AC; DC", "AC/DC" }
    };

    private readonly INameComparer _nameComparer;

    public Id3TagsService(INameComparer nameComparer)
    {
        _nameComparer = nameComparer;
    }

    public Id3Data GetId3Data(string filepath)
    {
        using TagLib.File f = GetFile(filepath);

        var track = new TrackId3Data
        {
            Filepath = filepath,
            Title = GetValue(f.Tag.Title),
            TrackNo = (int)f.Tag.Track,
            Duration = f.Properties.Duration,
            Lyrics = GetValue(f.Tag.Lyrics),
            Comment = f.Tag.Comment
        };

        var artist = new ArtistId3Data
        {
            Name = GetArtistName(f.Tag.JoinedPerformers, f.Tag.FirstPerformer),
            Genre = GetValue(f.Tag.Genres.FirstOrDefault()),
            Grouping = GetValue(f.Tag.Grouping),
            Image = GetArtwork(f, PictureType.Artist, false)
        };

        var album = new AlbumId3Data
        {
            ArtistName = GetArtistName(f.Tag.JoinedAlbumArtists, f.Tag.FirstAlbumArtist),
            Title = GetValue(f.Tag.Album),
            Year = f.Tag.Year.ToString(),
            DiscCount = (int)f.Tag.DiscCount,
            ReleaseType = GetValue(f.Tag.MusicBrainzReleaseType),
            Artwork = GetArtwork(f, PictureType.FrontCover, true)
        };

        var disc = new DiscId3Data
        {
            TrackCount = (int)f.Tag.TrackCount,
            DiscNo = (int)f.Tag.Disc
        };

        return new Id3Data
        {
            Track = track,
            Artist = artist,
            Album = album,
            Disc = disc
        };

    }

    public ArtworkImage GetAlbumArtwork(string filepath)
    {
        using TagLib.File f = GetFile(filepath);
        return GetArtwork(f, PictureType.FrontCover, true);
    }

    public ArtworkImage GetArtistImage(string filepath)
    {
        using TagLib.File f = GetFile(filepath);
        return GetArtwork(f, PictureType.Artist, false);
    }

    public void SaveId3Data(string filepath, Id3Data data)
    {
        using TagLib.File f = GetFile(filepath);

        if (data.Track != null)
        {
            f.Tag.Title = data.Track.Title;
            f.Tag.Lyrics = data.Track.Lyrics;
            f.Tag.Track = Convert.ToUInt16(data.Track.TrackNo);
        }

        if (data.Artist != null)
        {
            f.Tag.Performers = null;
            f.Tag.PerformersSort = null;
            f.Tag.Performers = new[] { data.Artist.Name };
            f.Tag.PerformersSort = new[] { _nameComparer.GetSortName(data.Artist.Name) };
            f.Tag.Genres = null;
            f.Tag.Genres = new[] { data.Artist.Genre };
            f.Tag.Grouping = data.Artist.Grouping;
        }

        if (data.Album != null)
        {
            f.Tag.Album = data.Album.Title;
            f.Tag.AlbumSort = data.Album.SortTitle;

            f.Tag.AlbumArtists = null;
            f.Tag.AlbumArtistsSort = null;
            f.Tag.AlbumArtists = new[] { data.Album.ArtistName };
            f.Tag.AlbumArtistsSort = new[] { _nameComparer.GetSortName(data.Album.ArtistName) };

            f.Tag.Year = Convert.ToUInt16(data.Album.Year);

            f.Tag.MusicBrainzReleaseType = data.Album.ReleaseType;

            f.Tag.DiscCount = Convert.ToUInt16(data.Album.DiscCount);

            f.Tag.Pictures = null;
            var pictures = new List<IPicture>();
            if (data.Album.Artwork != null)
            {
                pictures.Add(CreatePicture(data.Album.Artwork, PictureType.FrontCover));
            }
            if (data.Artist.Image != null)
            {
                pictures.Add(CreatePicture(data.Artist.Image, PictureType.Artist));
            }
            f.Tag.Pictures = pictures.ToArray();
        }

        if (data.Disc != null)
        {
            f.Tag.TrackCount = Convert.ToUInt16(data.Disc.TrackCount);
            f.Tag.Disc = Convert.ToUInt16(data.Disc.DiscNo);
        }

        if (data.Track.Comment != null)
        {
            f.Tag.Comment = data.Track.Comment;
        }

        f.Save();
    }

    private TagLib.File GetFile(string filepath)
    {
        var file = TagLib.File.Create(filepath);

        TagLib.Id3v2.Tag.DefaultVersion = 3;
        TagLib.Id3v2.Tag.ForceDefaultVersion = true;

        return file;
    }

    private Picture CreatePicture(ArtworkImage artwork, PictureType pictureType)
    {
        using var ms = new MemoryStream(artwork.Bytes);

        return new Picture()
        {
            Data = ByteVector.FromStream(ms),
            Type = pictureType,
            MimeType = artwork.MimeType,
            Description = ""
        };
    }

    public ArtworkImage GetArtwork(TagLib.File f, PictureType pictureType, bool orFirstImage)
    {
        var image = f.Tag.Pictures.FirstOrDefault(im => im.Type == pictureType);

        if (image == null && orFirstImage)
        {
            image = f.Tag.Pictures.FirstOrDefault();
        }

        if (image == null)
            return null;

        return new ArtworkImage(image.Data.Data, image.MimeType);
    }

    private static string GetValue(string str)
    {
        return str ?? string.Empty;
    }

    private static string GetArtistName(string joinedArtists, string firstArtist)
    {
        joinedArtists ??= "";

        return _artistNameExceptions.Keys.Contains(joinedArtists)
            ? _artistNameExceptions[joinedArtists]
            : GetValue(firstArtist);
    }
}