﻿namespace Cadenza.Web.Api.Services;

internal class LibraryApi : ILibraryApi
{
    private readonly ApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;
    private readonly IViewModelMapper _mapper;

    public LibraryApi(IOptions<ApiSettings> settings, IApiHttpHelper apiHelper, IViewModelMapper mapper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
        _mapper = mapper;
    }

    public async Task<AlbumVM> GetAlbum(int id)
    {
        var album = await _apiHelper.Get<AlbumDTO>(_settings.Album, id);
        return _mapper.Map(album);
    }

    public async Task<ArtistVM> GetArtist(int id)
    {
        var artist = await _apiHelper.Get<ArtistDTO>(_settings.Artist, id);
        return _mapper.Map(artist);
    }

    public async Task<GenreFullVM> GetGenre(string genre, int groupingId)
    {
        var url = $"{_settings.Genre}?genre={genre}&groupingId={groupingId}";
        var dto = await _apiHelper.Get<GenreDTO>(url);
        return _mapper.Map(dto);
    }

    public async Task<List<ArtistVM>> GetArtistsByGrouping(int id)
    {
        var artists = await _apiHelper.Get<List<ArtistDTO>>(_settings.GroupingArtists, id);
        return artists.Select(a => _mapper.Map(a)).ToList();
    }

    public async Task<AlbumFullVM> GetFullAlbum(int id)
    {
        var album = await _apiHelper.Get<AlbumFullDTO>(_settings.AlbumFull, id);
        return _mapper.Map(album);
    }

    public async Task<ArtistFullVM> GetFullArtist(int id, bool includeAlbumsByOtherArtists)
    {
        var url = $"{_settings.ArtistFull}?id={id}&includeAlbumsByOtherArtists={includeAlbumsByOtherArtists}";
        var artist = await _apiHelper.Get<ArtistFullDTO>(url);
        return _mapper.Map(artist);
    }

    public async Task<TrackFullVM> GetFullTrack(int id)
    {
        var track = await _apiHelper.Get<TrackFullDTO>(_settings.TrackFull, id);
        return _mapper.Map(track);
    }

    public async Task<List<TaggedItemVM>> GetTag(string id)
    {
        var items = await _apiHelper.Get<List<TaggedItemDTO>>(_settings.Tag, id);
        return items.Select(i => _mapper.Map(i)).ToList();
    }

    public async Task<TrackDetailsVM> GetTrack(int id)
    {
        var track = await _apiHelper.Get<TrackDetailsDTO>(_settings.Track, id);
        return _mapper.Map(track);
    }
}
