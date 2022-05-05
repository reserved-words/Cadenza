using Cadenza.Local.MusicFiles.Model;

namespace Cadenza.Local.MusicFiles.Interfaces;

internal interface ICommentProcessor
{
    CommentData GetData(string comment);
    string CreateComment(CommentData commentData);
}
