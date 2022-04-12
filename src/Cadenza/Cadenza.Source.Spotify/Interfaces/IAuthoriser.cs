﻿using Cadenza.Source.Spotify.Api.Model.Auth;

namespace Cadenza.Source.Spotify.Interfaces;

internal interface IAuthoriser
{
    Task<CreateSessionResponse> CreateSession(string code, string redirectUri);
    Task<string> GetAuthHeader();
    Task<string> GetAuthUrl(string state, string redirectUri);
    Task<RefreshTokenResponse> RefreshSession(string refreshToken);
}
