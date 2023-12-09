using Cadenza.Local.API.Files.Extensions;

namespace Cadenza.Local.API.Files.Services;

internal class Id3Fetcher : IId3Fetcher
{
    private readonly ICommentProcessor _commentProcessor;
    private readonly IId3TagsService _id3Service;

    public Id3Fetcher(ICommentProcessor commentProcessor, IId3TagsService id3Service)
    {
        _commentProcessor = commentProcessor;
        _id3Service = id3Service;
    }

    public SyncTrackDTO GetFileData(string id, string filepath)
    {
        var data = _id3Service.GetId3Data(filepath);
        var comment = _commentProcessor.GetData(data.Track.Comment);

        var track = ConvertTrack(id, data, comment);
        track.Artist = ConvertTrackArtist(data);
        track.Album = ConvertAlbum(data, comment);

        return track;
    }

    private SyncTrackDTO ConvertTrack(string id, Id3Data data, CommentData comment)
    {
        return new SyncTrackDTO
        {
            IdFromSource = id,
            Title = data.Track.Title,
            DurationSeconds = (int)data.Track.Duration.TotalSeconds,
            Year = comment.TrackYear.Nullify() ?? data.Album.Year,
            Lyrics = data.Track.Lyrics.Nullify(),
            TagList = comment.TrackTags,
            DiscNo = data.Disc.DiscNo <= 0 ? 1 : data.Disc.DiscNo,
            TrackNo = data.Track.TrackNo
        };
    }

    private SyncAlbumDTO ConvertAlbum(Id3Data data, CommentData comment)
    {
        var album = new SyncAlbumDTO
        {
            ArtistName = data.Album.ArtistName,
            Title = data.Album.Title,
            ReleaseType = Enum.TryParse(data.Album.ReleaseType, out ReleaseType result) ? result : ReleaseType.Album,
            DiscCount = data.Album.DiscCount,
            TrackCounts = new List<int>(),
            Year = data.Album.Year.Nullify(),
            TagList = comment.AlbumTags,
            ArtworkMimeType = data.Album.Artwork?.MimeType,
            ArtworkContent = data.Album.Artwork?.Bytes
        };

        var discNo = data.Disc.DiscNo == 0 ? 1 : data.Disc.DiscNo;

        while (album.TrackCounts.Count < discNo)
        {
            album.TrackCounts.Add(0);
        }

        album.TrackCounts[discNo - 1] = data.Disc.TrackCount;

        return album;

    }

    private SyncArtistDTO ConvertTrackArtist(Id3Data data)
    {
        var commentData = _commentProcessor.GetData(data.Track.Comment);

        return new SyncArtistDTO
        {
            Name = data.Artist.Name,
            Grouping = data.Artist.Grouping,
            Genre = data.Artist.Genre.Nullify(),
            City = commentData.City.Nullify(),
            State = commentData.State.Nullify(),
            Country = commentData.Country.Nullify(),
            TagList = commentData.ArtistTags,
            ImageMimeType = data.Artist.Image?.MimeType,
            ImageContent = data.Artist.Image?.Bytes
        };
    }
}