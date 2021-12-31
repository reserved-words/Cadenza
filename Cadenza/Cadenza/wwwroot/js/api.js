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