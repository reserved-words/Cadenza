
var useCodeToFetchSpotifyTokens = function (code) {

    getSpotifyAuthHeader(function (authHeader) {

        var tokenData = {
            code: code,
            redirect_uri: spotifySettings.RedirectUri,
            grant_type: 'authorization_code'
        };

        getSpotifyTokenUrl(function (tokenUrl) {
            $.ajax({
                url: tokenUrl,
                method: 'post',
                data: tokenData,
                dataType: 'json',
                headers: { 'Authorization': authHeader },
                success: function (token) {
                    saveSpotifyTokens(token);
                    window.location.href = "/?spl=true";
                }
            });
        });

    });
};

$(function () {

    $.getJSON("../appsettings.json", function (settings) {
        apiSettings = settings.PlayerApi;
        spotifySettings = settings.Spotify;
        var code = getParam('code');
        useCodeToFetchSpotifyTokens(code);
    });

});