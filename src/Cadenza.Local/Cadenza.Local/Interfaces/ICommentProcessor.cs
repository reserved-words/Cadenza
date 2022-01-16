namespace Cadenza.Local;

public interface ICommentProcessor
{
    CommentData GetData(string comment);
    string CreateComment(CommentData commentData);
}
