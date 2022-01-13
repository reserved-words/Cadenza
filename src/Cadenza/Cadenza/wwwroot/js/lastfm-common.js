
var isLastFmLoggedIn = function () {
    return getStoredValue('LastFmSessionKey');
}

var lastFmLogin = function () {
    getLastFmAuthUrl(function (url) {
        window.location.href = url;
    });
}