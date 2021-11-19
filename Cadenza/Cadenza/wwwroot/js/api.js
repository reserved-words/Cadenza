var apiSettings = null;

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
    var endpoint = settings.LastFmSessionKeyUrl + token;
    getFromApi(endpoint, onSuccess);
}

function getLastFmAuthUrl(redirectUri, onSuccess) {
    var endpoint = settings.LastFmAuthUrl + redirectUri;
    getFromApi(endpoint, onSuccess);
}

function getSpotifyAuthUrl(redirectUri, onSuccess) {
    var endpoint = settings.SpotifyAuthUrl + redirectUri;
    getFromApi(endpoint, onSuccess);
}

function getSpotifyAuthHeader (onSuccess) {
    var endpoint = settings.SpotifyAuthHeader;
    getFromApi(endpoint, onSuccess);
}

function getSpotifyTokenUrl(onSuccess) {
    var endpoint = settings.SpotifyTokenUrl;
    getFromApi(endpoint, onSuccess);
}