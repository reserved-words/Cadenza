using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace TestConsoleApp;
internal class ScrobbleImporter
{
    private const int MaxItems = 200;
    private const string ConnectionString = "Data Source=localhost;Initial Catalog=cadenza-test;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";

    private readonly DateTime _startTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    internal async Task Run(string apiKey, string username)
    {
        var totalPages = await GetTotalPages(apiKey, username);

        for (var i = 1; i <= totalPages; i++)
        {
            Console.WriteLine($"Starting page {i} of {totalPages}");

            var scrobbles = await GetScrobbles(apiKey, username, i);
            await ImportScrobbles(ConnectionString, scrobbles);


            Console.WriteLine($"Finished page {i} of {totalPages}");
        }

    }

    private async Task<int> GetTotalPages(string apiKey, string username)
    {
        var url = $"https://ws.audioscrobbler.com/2.0/?method=user.getrecenttracks&user={username}&api_key={apiKey}&format=json&page=1&limit={MaxItems}";

        using var httpClient = new HttpClient();

        var response = await httpClient.GetFromJsonAsync<LastFmResponse>(url);

        Console.WriteLine(response.RecentTracks.Attr.TotalPages);

        return response.RecentTracks.Attr.TotalPages;
    }

    private async Task<List<Scrobble>> GetScrobbles(string apiKey, string username, int page)
    {
        var url = $"https://ws.audioscrobbler.com/2.0/?method=user.getrecenttracks&user={username}&api_key={apiKey}&format=json&page={page}&limit={MaxItems}";

        using var httpClient = new HttpClient();

        var response = await httpClient.GetFromJsonAsync<LastFmResponse>(url);

        var list  = new List<Scrobble>();

        foreach (var track in response.RecentTracks.Track)
        {
            list.Add(new Scrobble
            {
                ScrobbledAt = _startTime.AddSeconds(track.Date.UTS).ToLocalTime(), // date 26 Nov 2023, 14:52
                Track = track.Name,
                Album = track.Album.Text,
                Artist = track.Artist.Text
            });
        }

        return list;
    }

    private async Task ImportScrobbles(string connectionString, List<Scrobble> scrobbles)
    {
        using var connection = new SqlConnection(ConnectionString);

        try
        {
            foreach (var scrobble in scrobbles)
            {
                await connection.ExecuteAsync("[Admin].[ImportScrobble]", scrobble, commandType: CommandType.StoredProcedure);
            }
        }
        finally
        {
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }
}

internal class Scrobble
{
    public DateTime ScrobbledAt { get; set; }
    public string Track { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string AlbumArtist { get; set; }
}


internal class LastFmResponse
{
    public RecentTracks RecentTracks { get; set; }
}

internal class RecentTracks
{
    public List<Track> Track { get; set; }
    [JsonPropertyName("@attr")]
    public Attr Attr { get; set; }
}

internal class Track
{
    public Artist Artist { get; set; }
    public Album Album { get; set; }
    public string Name { get; set; } // title
    public ScrobbleDate Date { get; set; }
}

internal class Artist
{
    [JsonPropertyName("#text")]
    public string Text { get; set; }
}

internal class Album
{
    [JsonPropertyName("#text")]
    public string Text { get; set; }
}

internal class ScrobbleDate
{
    public long UTS { get; set; }
    [JsonPropertyName("#text")]
    public string Text { get; set; } 
}

internal class Attr
{
    public int TotalPages { get; set; }
}