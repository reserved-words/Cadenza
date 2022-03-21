namespace Cadenza.Library;

public abstract class MergedRepositoryBase<TSourceRepository>
{
    protected abstract int ItemFetchLimit { get; }

    protected readonly IEnumerable<TSourceRepository> Sources;

    public MergedRepositoryBase(IEnumerable<TSourceRepository> sources)
    {
        Sources = sources;
    }

    protected async Task<List<TItem>> Fetch<TItem>(Func<TSourceRepository, int, int, Task<ListResponse<TItem>>> get)
    {
        var result = new List<TItem>();

        foreach (var source in Sources)
        {
            result.AddRange(await Fetch(source, get));
        }

        return result;
    }

    protected async Task<List<TItem>> Fetch<TItem>(Func<TSourceRepository, Task<List<TItem>>> get)
    {
        var result = new List<TItem>();

        foreach (var source in Sources)
        {
            result.AddRange(await get(source));
        }

        return result;
    }

    protected async Task<List<Artist>> FetchArtists(Func<TSourceRepository, int, int, Task<ListResponse<Artist>>> get)
    {
        var result = new List<Artist>();

        foreach (var source in Sources)
        {
            var artists = await Fetch(source, get);
            var artistsToAdd = artists.Where(a => result.All(ar => ar.Id != a.Id));
            result.AddRange(artistsToAdd);
        }

        return result;
    }

    private async Task<List<TItem>> Fetch<TItem>(TSourceRepository repository, Func<TSourceRepository, int, int, Task<ListResponse<TItem>>> get)
    {
        var result = new List<TItem>();

        var response = await get(repository, 1, ItemFetchLimit);
        result.AddRange(response.Items);

        while (response.Page < response.TotalPages)
        {
            response = await get(repository, response.Page + 1, ItemFetchLimit);
            result.AddRange(response.Items);
        }

        return result;
    }
}
