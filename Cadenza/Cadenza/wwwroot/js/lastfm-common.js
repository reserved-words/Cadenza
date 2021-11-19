
var lfm_redirect_uri = 'http://localhost:29085/lastfm-callback.html'; // move to settings

var isLastFmLoggedIn = function () {
    return getStoredValue('LastFmSessionKey');
}

var lastFmLogin = function () {
    getLastFmAuthUrl(lfm_redirect_uri, function (url) {
        window.location.href = url;
    });
}