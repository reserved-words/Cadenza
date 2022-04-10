using Cadenza.Local.Common.Model;

namespace Cadenza.Local.Common.Interfaces;

public interface ICommentProcessor
{
    CommentData GetData(string comment);
    string CreateComment(CommentData commentData);
}
