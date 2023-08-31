using System.Data.SqlClient;
using System.Text;

namespace TestConsoleApp;
internal class StripBaseDirectoryFromFilepath
{
    private const string ConnectionString = "Data Source=localhost;Initial Catalog={0};Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";

    private const string GetTracksSql = "SELECT Id, IdFromSource FROM Library.Tracks";
    private const string UpdateTrackSql = "UPDATE Library.Tracks SET IdFromSource = @Trimmed WHERE Id = @Id";

    private const string GetRemovedTracksSql = "SELECT Id, TrackIdFromSource FROM [Queue].[TrackRemovals]";
    private const string UpdateRemovedTrackSql = "UPDATE [Queue].[TrackRemovals] SET TrackIdFromSource = @Trimmed WHERE Id = @Id";

    internal async Task Run(string databaseName, string trimString)
    {
        var trimLength = trimString.Length;

        var connectionString = string.Format(ConnectionString, databaseName);

        using var connection = new SqlConnection(connectionString);

        try
        {
            connection.Open();

            await RunSql(connection, GetTracksSql, UpdateTrackSql, trimLength);
            await RunSql(connection, GetRemovedTracksSql, UpdateRemovedTrackSql, trimLength);
        }
        finally
        {
            if (connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }

    private async Task RunSql(SqlConnection connection, string getSql, string updateSql, int trimLength)
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
            var trimmed = track.IdFromSource.Substring(trimLength, track.IdFromSource.Length - trimLength);

            Console.WriteLine($"{track.Id}: {track.IdFromSource}: {trimmed}");

            using (var command = new SqlCommand(updateSql, connection))
            {
                command.Parameters.Add(new SqlParameter("Trimmed", trimmed));
                command.Parameters.Add(new SqlParameter("Id", track.Id));

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
