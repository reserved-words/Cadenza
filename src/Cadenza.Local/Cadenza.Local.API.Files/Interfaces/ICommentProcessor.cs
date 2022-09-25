using Cadenza.Local.API.Files.Model;

namespace Cadenza.Local.API.Files.Interfaces;

internal interface ICommentProcessor
{
    CommentData GetData(string comment);
    string CreateComment(CommentData commentData);
}
