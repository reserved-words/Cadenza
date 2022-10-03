namespace Cadenza.Local.API.Files.Services;

internal class CommentProcessor : ICommentProcessor
{
    private readonly IJsonConverter _jsonConverter;

    public CommentProcessor(IJsonConverter jsonConverter)
    {
        _jsonConverter = jsonConverter;
    }

    public CommentData GetData(string comment)
    {
        return _jsonConverter.Deserialize<CommentData>(comment);
    }

    public string CreateComment(CommentData commentData)
    {
        return _jsonConverter.Serialize(commentData);
    }
}
