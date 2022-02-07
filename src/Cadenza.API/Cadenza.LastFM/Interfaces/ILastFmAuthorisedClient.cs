﻿namespace Cadenza.LastFM.Interfaces;

public interface ILastFmAuthorisedClient
{
    Task Post(string sessionKey, Dictionary<string, string> parameters);
}