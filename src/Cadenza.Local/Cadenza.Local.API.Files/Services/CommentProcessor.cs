using System.Text.Json;

namespace Cadenza.Local.API.Files.Services;

internal class CommentProcessor : ICommentProcessor
{
    public CommentData GetData(string comment)
    {
        if (string.IsNullOrWhiteSpace(comment))
            return new CommentData();

        comment = comment.Trim();

        try
        {
            return JsonSerializer.Deserialize<CommentData>(comment);
        }
        catch (Exception)
        {
            return new CommentData();
        }
    }

    public string CreateComment(CommentData commentData)
    {
        return JsonSerializer.Serialize(commentData);
    }
}
