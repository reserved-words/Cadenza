using System.Data.SqlClient;
using System.Text;

namespace TestConsoleApp;
internal class UpdateFromBase64ToFilepaths
{
    private const string ConnectionString = "Data Source=localhost;Initial Catalog={0};Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";

    private const string GetTracksSql = "SELECT Id, IdFromSource FROM Library.Tracks";
    private const string UpdateTrackSql = "UPDATE Library.Tracks SET IdFromSource = @FilePath WHERE Id = @Id";

    private const string GetRemovedTracksSql = "SELECT Id, TrackIdFromSource FROM [Queue].[TrackRemovals]";
    private const string UpdateRemovedTrackSql = "UPDATE [Queue].[TrackRemovals] SET TrackIdFromSource = @FilePath WHERE Id = @Id";

    internal async Task Run(string databaseName)
    {
        var connectionString = string.Format(ConnectionString, databaseName);

        using var connection = new SqlConnection(ConnectionString);

        try
        {
            connection.Open();

            await RunSql(connection, GetTracksSql, UpdateTrackSql);
            await RunSql(connection, GetRemovedTracksSql, UpdateRemovedTrackSql);
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
        List<(int Id, string IdFromSource)> tracks = new();

        using (var command = new SqlCommand(getSql, connection))
        {
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var idFromSource = reader.GetString(1);

                    tracks.Add((id, idFromSource));
                }
            }
        }

        foreach (var track in tracks)
        {
            var textBytes = Convert.FromBase64String(track.IdFromSource);
            var filepath = Encoding.UTF8.GetString(textBytes);

            Console.WriteLine($"{track.Id}: {track.IdFromSource}: {filepath}");

            using (var command = new SqlCommand(updateSql, connection))
            {
                command.Parameters.Add(new SqlParameter("FilePath", filepath));
                command.Parameters.Add(new SqlParameter("Id", track.Id));

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
