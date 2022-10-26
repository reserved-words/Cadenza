using Cadenza.Common.Domain.JsonConverters;
using System.Text.Json;

namespace Cadenza.Local.API.Files.Services;

internal class CommentProcessor : ICommentProcessor
{
    public CommentData GetData(string comment)
    {
        return JsonSerializer.Deserialize<CommentData>(comment, JsonSerialization.Options);
    }

    public string CreateComment(CommentData commentData)
    {
        return JsonSerializer.Serialize(commentData, JsonSerialization.Options);
    }
}
