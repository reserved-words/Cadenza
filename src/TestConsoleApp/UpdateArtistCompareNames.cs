using Cadenza.Common.Utilities.Services;
using System.Data.SqlClient;

namespace TestConsoleApp;
internal class UpdateArtistCompareNames
{
    private const string ConnectionString = "Data Source=localhost;Initial Catalog={0};Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";

    private const string GetArtistsSql = "SELECT Id, Name FROM Library.Artists";
    private const string UpdateArtistSql = "UPDATE Library.Artists SET CompareName = @CompareName WHERE Id = @Id";

    internal async Task Run(string databaseName)
    {
        var connectionString = string.Format(ConnectionString, databaseName);

        using var connection = new SqlConnection(connectionString);

        try
        {
            connection.Open();

            await RunSql(connection, GetArtistsSql, UpdateArtistSql);
        }
        finally
        {
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }

    private async Task RunSql(SqlConnection connection, string getSql, string updateSql)
    {
        var nameComparer = new NameComparer();

        List<(int Id, string Name)> artists = new();

        using (var command = new SqlCommand(getSql, connection))
        {
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var name = reader.GetString(1);

                    artists.Add((id, name));
                }
            }
        }

        foreach (var artist in artists)
        {
            var compareName = nameComparer.GetCompareName(artist.Name);

            Console.WriteLine($"{artist.Id}: {artist.Name}: {compareName}");

            using (var command = new SqlCommand(updateSql, connection))
            {
                command.Parameters.Add(new SqlParameter("CompareName", compareName));
                command.Parameters.Add(new SqlParameter("Id", artist.Id));

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
