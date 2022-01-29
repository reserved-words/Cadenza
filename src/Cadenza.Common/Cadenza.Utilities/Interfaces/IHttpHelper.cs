﻿namespace Cadenza.Utilities;

public interface IHttpHelper
{
    Task<HttpResponseMessage> Get(string url, string authHeader = null);
    Task<HttpResponseMessage> Post(string url, string authHeader = null, object data = null);
    Task<HttpResponseMessage> Put(string url, string authHeader = null, object data = null);
    Task<HttpResponseMessage> Delete(string url, string authHeader = null, object data = null);
    Task<HttpResponseMessage> Post(string url, Dictionary<string, string> parameters, string authHeader = null);
}
