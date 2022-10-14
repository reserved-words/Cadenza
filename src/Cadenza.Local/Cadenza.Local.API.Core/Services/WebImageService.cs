namespace Cadenza.Local.API.Core.Services;

internal class WebImageService : IWebImageService
{
    private readonly IHttpHelper _httpHelper;

    public WebImageService(IHttpHelper httpHelper)
    {
        _httpHelper = httpHelper;
    }

    public async Task<byte[]> GetBytes(string url)
    {
        // TODO
        // Validate to make sure it's definitely an image

        var response = await _httpHelper.Get(url);

        // TODO
        // Make sure to include all details in logged error

        if (!response.IsSuccessStatusCode)
            throw new Exception();

        return await response.Content.ReadAsByteArrayAsync();
    }
}
