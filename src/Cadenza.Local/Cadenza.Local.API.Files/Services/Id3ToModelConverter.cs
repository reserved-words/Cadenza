namespace Cadenza.Local.API.Files.Services;

internal class Id3ToModelConverter : IId3ToModelConverter
{
    private readonly IBase64Converter _base64Converter;
    private readonly IIdGenerator _idGenerator;
    private readonly ICommentProcessor _commentProcessor;
    private readonly IImageConverter _imageConverter;

    public Id3ToModelConverter(IIdGenerator idGenerator, ICommentProcessor commentProcessor, IBase64Converter base64Converter, IImageConverter imageConverter)
    {
        _idGenerator = idGenerator;
        _commentProcessor = commentProcessor;
        _base64Converter = base64Converter;
        _imageConverter = imageConverter;
    }

    public AlbumInfo ConvertAlbum(Id3Data data)
    {
        var commentData = _commentProcessor.GetData(data.Track.Comment);

        var albumId = _idGenerator.GenerateId(data.Album.ArtistName, data.Album.Title);
        var artistId = _idGenerator.GenerateId(data.Album.ArtistName);

        var album = new AlbumInfo
        {
            Id = albumId,
            ArtistId = artistId,
            ArtistName = data.Album.ArtistName,
            Title = data.Album.Title,
            ReleaseType = Enum.TryParse(data.Album.ReleaseType, out ReleaseType result) ? result : ReleaseType.Album,
            TrackCounts = new List<int>(),
            Year = data.Album.Year.Nullify(),
            Tags = commentData.AlbumTags,
            ArtworkUrl = _imageConverter.GetBase64UrlFromImage(data.Album.Artwork)
        };

        var discNo = data.Disc.DiscNo == 0 ? 1 : data.Disc.DiscNo;

        while (album.TrackCounts.Count < discNo)
        {
            album.TrackCounts.Add(0);
        }

        album.TrackCounts[discNo - 1] = data.Disc.TrackCount;

        return album;

    }

    public ArtistInfo ConvertAlbumArtist(Id3Data data)
    {
        return new ArtistInfo
        {
            Id = _idGenerator.GenerateId(data.Album.ArtistName),
            Name = data.Album.ArtistName
        };
    }

    public AlbumTrackLink ConvertAlbumTrackLink(string id, Id3Data id3Data)
    {
        var albumId = _idGenerator.GenerateId(id3Data.Album.ArtistName, id3Data.Album.Title);

        return new AlbumTrackLink
        {
            TrackId = id,
            AlbumId = albumId,
            DiscNo = id3Data.Disc.DiscNo <= 0 ? 1 : id3Data.Disc.DiscNo,
            TrackNo = id3Data.Track.TrackNo
        };
    }

    public TrackInfo ConvertTrack(Id3Data data)
    {
        var commentData = _commentProcessor.GetData(data.Track.Comment);

        return new TrackInfo
        {
            Id = _base64Converter.ToBase64(data.Track.Filepath),
            ArtistId = _idGenerator.GenerateId(data.Artist.Name),
            ArtistName = data.Artist.Name,
            AlbumId = _idGenerator.GenerateId(data.Album.ArtistName, data.Album.Title),
            Title = data.Track.Title,
            DurationSeconds = (int)data.Track.Duration.TotalSeconds,
            Year = commentData.TrackYear.Nullify() ?? data.Album.Year,
            Lyrics = data.Track.Lyrics.Nullify(),
            Tags = commentData.TrackTags
        };
    }

    public ArtistInfo ConvertTrackArtist(Id3Data data)
    {
        var commentData = _commentProcessor.GetData(data.Track.Comment);

        return new ArtistInfo
        {
            Id = _idGenerator.GenerateId(data.Artist.Name),
            Name = data.Artist.Name,
            Grouping = Enum.TryParse(data.Artist.Grouping, out Grouping result) ? result : Grouping.None,
            Genre = data.Artist.Genre.Nullify(),
            City = commentData.City.Nullify(),
            State = commentData.State.Nullify(),
            Country = commentData.Country.Nullify(),
            Tags = commentData.ArtistTags,
            ImageUrl = _imageConverter.GetBase64UrlFromImage(data.Artist.Image)
        };
    }
}