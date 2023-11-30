﻿namespace Cadenza.Web.Model;

public record TopAlbumVM(int Id, string Artist, string Title, string ImageUrl, int Plays, int Rank)
{
    public override string ToString() => $"{Title} by {Artist}";
}