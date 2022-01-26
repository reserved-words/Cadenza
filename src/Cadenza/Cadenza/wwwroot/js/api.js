var apiSettings = null;
var lastFmSettings = null;
var spotifySettings = null;

function getFromApi(endpoint, onSuccess) {

    var url = apiSettings.BaseUrl + endpoint;

    $.ajax({
        url: url,
        method: 'get',
        success: function (data) {
            onSuccess(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Error");

/*            A function to be called if the request fails.
 *            The function receives three arguments: 
 *            The jqXHR(in jQuery 1.4.x, XMLHttpRequest) object, 
 *            a string describing the type of error that occurred and an optional exception object, 
 *            if one occurred.
 *            Possible values for the second argument(besides null) are 
 *            "timeout", "error", "abort", and "parsererror".
 *            When an HTTP error occurs, errorThrown receives the textual 
 *            portion of the HTTP status, such as "Not Found" or "Internal Server Error."
 *            (in HTTP / 2 it may instead be an empty string) 
 *            As of jQuery 1.5, the error setting can accept an array of functions.
 *            Each function will be called in turn.Note: 
 *            This handler is not called for cross - domain script 
 *            and cross - domain JSONP requests.This is an Ajax Event.*/
        }
    });
}

function getLastFmSessionKeyUrl(token, onSuccess) {
    var endpoint = apiSettings.Endpoints.LastFmSessionKeyUrl + token;
    getFromApi(endpoint, onSuccess);
}

function getLastFmAuthUrl(onSuccess) {
    var redirectUri = lastFmSettings.RedirectUri;
    var endpoint = apiSettings.Endpoints.LastFmAuthUrl + redirectUri;
    getFromApi(endpoint, onSuccess);
}

function getSpotifyAuthUrl(onSuccess) {
    var redirectUri = spotifySettings.RedirectUri;
    var endpoint = apiSettings.Endpoints.SpotifyAuthUrl + redirectUri;
    getFromApi(endpoint, onSuccess);
}

function getSpotifyAuthHeader (onSuccess) {
    var endpoint = apiSettings.Endpoints.SpotifyAuthHeader;
    getFromApi(endpoint, onSuccess);
}

function getSpotifyTokenUrl(onSuccess) {
    var endpoint = apiSettings.Endpoints.SpotifyTokenUrl;
    getFromApi(endpoint, onSuccess);
}