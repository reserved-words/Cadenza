using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace TestConsoleApp;
internal class LovedTrackImporter
{
    private const int MaxItems = 200;
    private const string ConnectionString = "Data Source=localhost;Initial Catalog=cadenza-test;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";

    internal async Task Run(string apiKey, string username)
    {
        var totalPages = await GetTotalPages(apiKey, username);

        for (var i = 1; i <= totalPages; i++)
        {
            Console.WriteLine($"Starting page {i} of {totalPages}");

            var scrobbles = await GetLovedTracks(apiKey, username, i);
            await ImportLovedTracks(ConnectionString, scrobbles);


            Console.WriteLine($"Finished page {i} of {totalPages}");
        }

    }

    private async Task<int> GetTotalPages(string apiKey, string username)
    {
        var url = $"https://ws.audioscrobbler.com/2.0/?method=user.getlovedtracks&user={username}&api_key={apiKey}&format=json&page=1&limit={MaxItems}";

        using var httpClient = new HttpClient();

        var response = await httpClient.GetFromJsonAsync<LastFmResponse>(url);

        Console.WriteLine(response.LovedTracks.Attr.TotalPages);

        return response.LovedTracks.Attr.TotalPages;
    }

    private async Task<List<LovedTrack>> GetLovedTracks(string apiKey, string username, int page)
    {
        var url = $"https://ws.audioscrobbler.com/2.0/?method=user.getlovedtracks&user={username}&api_key={apiKey}&format=json&page={page}&limit={MaxItems}";

        using var httpClient = new HttpClient();

        var response = await httpClient.GetFromJsonAsync<LastFmResponse>(url);

        var list  = new List<LovedTrack>();

        foreach (var track in response.LovedTracks.Track)
        {
            list.Add(new LovedTrack
            {
                Track = track.Name,
                Artist = track.Artist.Name
            });
        }

        return list;
    }

    private async Task ImportLovedTracks(string connectionString, List<LovedTrack> lovedTracks)
    {
        using var connection = new SqlConnection(ConnectionString);

        try
        {
            foreach (var lovedTrack in lovedTracks)
            {
                await connection.ExecuteAsync("[LastFm].[ImportLovedTrack]", lovedTrack, commandType: CommandType.StoredProcedure);
            }
        }
        finally
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }

    internal class LovedTrack
    {
        public string Track { get; set; }
        public string Artist { get; set; }
    }

    internal class LastFmResponse
    {
        public LovedTracks LovedTracks { get; set; }
    }

    internal class LovedTracks
    {
        public List<Track> Track { get; set; }
        [JsonPropertyName("@attr")]
        public Attr Attr { get; set; }
    }

    internal class Track
    {
        public Artist Artist { get; set; }
        public string Name { get; set; } // title
    }

    internal class Artist
    {
        public string Name { get; set; }
    }

    internal class Attr
    {
        public int TotalPages { get; set; }
    }
}