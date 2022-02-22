namespace Cadenza.Utilities;

public class Merger : IMerger
{
    private readonly IValueMerger _merger;

    public Merger(IValueMerger merger)
    {
        _merger = merger;
    }

    public void MergeArtist(ArtistInfo artist, ArtistInfo update, MergeMode mode)
    {
        artist.Id = Merge(artist.Id, update.Id, mode);
        artist.Name = Merge(artist.Name, update.Name, mode);
        artist.Grouping = Merge(artist.Grouping, update.Grouping, mode);
        artist.Genre = Merge(artist.Genre, update.Genre, mode);
        artist.City = Merge(artist.City, update.City, mode);
        artist.State = Merge(artist.State, update.State, mode);
        artist.Country = Merge(artist.Country, update.Country, mode);
        artist.Links = MergeLinks(artist.Links, update.Links, mode);
    }

    public void MergeAlbum(AlbumInfo album, AlbumInfo update, MergeMode mode)
    {
        album.Id = Merge(album.Id, update.Id, mode);
        album.ArtistId = Merge(album.ArtistId, update.ArtistId, mode);
        album.ArtistName = Merge(album.ArtistName, update.ArtistName, mode);
        album.Title = Merge(album.Title, update.Title, mode);
        album.Year = Merge(album.Year, update.Year, mode);
        album.ArtworkUrl = Merge(album.ArtworkUrl, update.ArtworkUrl, mode);
        album.DiscCount = Merge(album.DiscCount, update.DiscCount, mode);
        album.ReleaseType = Merge(album.ReleaseType, update.ReleaseType, mode);
        album.Source = Merge(album.Source, update.Source, mode);
        album.TrackCounts = MergeTrackCounts(album.TrackCounts, update.TrackCounts, mode);
    }

    public void MergeTrack(TrackInfo track, TrackInfo update, MergeMode mode)
    {
        track.Id = Merge(track.Id, update.Id, mode);
        track.Title = Merge(track.Title, update.Title, mode);
        track.ArtistId = Merge(track.ArtistId, update.ArtistId, mode);
        track.ArtistName = Merge(track.ArtistName, update.ArtistName, mode);
        track.AlbumId = Merge(track.AlbumId, update.AlbumId, mode);
        track.Lyrics = Merge(track.Lyrics, update.Lyrics, mode);
        track.Year = Merge(track.Year, update.Year, mode);
        track.Tags = MergeTags(track.Tags, update.Tags, mode);
        track.DurationSeconds = Merge(track.DurationSeconds, update.DurationSeconds, mode);
        track.Source = Merge(track.Source, update.Source, mode);
    }

    public void MergeAlbumTrackLink(AlbumTrackLink existing, AlbumTrackLink update, MergeMode mode)
    {
        existing.AlbumId = Merge(existing.AlbumId, update.AlbumId, mode);
        existing.Position.DiscNo = Merge(existing.Position.DiscNo, update.Position.DiscNo, mode);
        existing.Position.TrackNo = Merge(existing.Position.TrackNo, update.Position.TrackNo, mode);
    }

    public void MergePlaylist(Playlist existing, Playlist update, MergeMode mode)
    {
        existing.Id = Merge(existing.Id, update.Id, mode);
        existing.Name = Merge(existing.Name, update.Name, mode);
        existing.ArtworkUrl = Merge(existing.ArtworkUrl, update.ArtworkUrl, mode);
    }

    public void MergePlaylistTrackLink(PlaylistTrackLink existing, PlaylistTrackLink update, MergeMode mode)
    {
        existing.PlaylistId = Merge(existing.PlaylistId, update.PlaylistId, mode);
        existing.Position = Merge(existing.Position, update.Position, mode);
    }

    private int Merge(int original, int update, MergeMode mode)
    {
        return _merger.Merge(original, update, mode);
    }

    private string Merge(string original, string update, MergeMode mode)
    {
        return _merger.Merge(original, update, mode);
    }

    private LibrarySource Merge(LibrarySource original, LibrarySource update, MergeMode mode)
    {
        return _merger.Merge(original, update, mode);
    }

    private ReleaseType Merge(ReleaseType original, ReleaseType update, MergeMode mode)
    {
        return _merger.Merge(original, update, mode);
    }

    private Grouping Merge(Grouping original, Grouping update, MergeMode mode)
    {
        return _merger.Merge(original, update, mode);
    }

    private ICollection<Link> MergeLinks(ICollection<Link> original, ICollection<Link> update, MergeMode mode)
    {
        return _merger.MergeList(original, update, mode);
    }

    private ICollection<string> MergeTags(ICollection<string> original, ICollection<string> update, MergeMode mode)
    {
        return _merger.MergeList(original, update, mode);
    }

    private List<int> MergeTrackCounts(List<int> original, List<int> update, MergeMode mode)
    {
        return _merger.MergeTrackCounts(original, update, mode);
    }
}