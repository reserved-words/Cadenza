
using Cadenza.Common.Domain.JsonConverters;
using Cadenza.Common.Domain.Model;
using Cadenza.Common.Domain.Model.Track;
using System.Text.Json;

Console.WriteLine("Starting");

var tags = new List<string> { "one", "two", "three" };

var tagList = new TagList(tags);

var track = new TrackInfo
{
    Id = "id",
    Tags = tagList
};

var json = JsonSerializer.Serialize(track, JsonSerialization.Options);

var deserializedTrack = JsonSerializer.Deserialize<TrackInfo>(json);

var serializedTrack = JsonSerializer.Serialize(track, JsonSerialization.Options);

Console.WriteLine(json);

Console.ReadKey();