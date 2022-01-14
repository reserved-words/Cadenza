﻿using System.Xml.Linq;

namespace Cadenza.LastFM;

public interface ILastFmClient
{
    Task<T> Get<T>(string url, Func<XElement, T> getValue);
}