namespace Cadenza.Local.API.Files.Services;

internal class Id3Fetcher : IId3Fetcher
{
    private readonly IId3TagsService _id3Service;
    private readonly IId3ToModelConverter _converter;

    public Id3Fetcher(IId3ToModelConverter converter, IId3TagsService id3Service)
    {
        _converter = converter;
        _id3Service = id3Service;
    }

    public TrackFull GetFileData(string filepath)
    {
        var id3Track = _id3Service.GetId3Data(filepath);
        var trackArtist = _converter.ConvertTrackArtist(id3Track);
        var albumArtist = _converter.ConvertAlbumArtist(id3Track);
        var track = _converter.ConvertTrack(id3Track);
        var album = _converter.ConvertAlbum(id3Track);
        var albumTrackLink = _converter.ConvertAlbumTrackLink(track.Id, id3Track);

        track.ArtistName = trackArtist.Name;
        album.ArtistName = albumArtist.Name;
        album.DiscCount = album.TrackCounts.Count;

        return new TrackFull
        {
            Album = album,
            AlbumArtist = albumArtist,
            AlbumTrack = albumTrackLink,
            Artist = trackArtist,
            Track = track
        };
    }
}