﻿namespace Cadenza.Common.Interfaces.Utilities;

public interface IHttpHelper
{
    Task<HttpResponseMessage> Get(string url, string authHeader = null);
    Task<T> Get<T>(string url, string authHeader = null);
    Task<HttpResponseMessage> Post(string url, string authHeader = null, object data = null);
    Task<HttpResponseMessage> Put(string url, string authHeader = null, object data = null);
    Task<HttpResponseMessage> Delete(string url, string authHeader = null, object data = null);
    Task<HttpResponseMessage> Post(string url, string authHeader = null, Dictionary<string, string> parameters = null);
}
