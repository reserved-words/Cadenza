﻿namespace Cadenza.API.Wrapper.Spotify;

internal class TokenRequestData
{
    public string code { get; set; }
    public string redirect_uri { get; set; }
    public string grant_type { get; set; }
}