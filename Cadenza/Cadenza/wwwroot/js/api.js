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
    var endpoint = apiSettings.Endpoints.LastFmSessionKeyUrl + token;
    getFromApi(endpoint, onSuccess);
}

function getLastFmAuthUrl(redirectUri, onSuccess) {
    var endpoint = apiSettings.Endpoints.LastFmAuthUrl + redirectUri;
    getFromApi(endpoint, onSuccess);
}

function getSpotifyAuthUrl(redirectUri, onSuccess) {
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