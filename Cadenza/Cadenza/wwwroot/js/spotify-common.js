
var isSpotifyLoggedIn = function () {
    return getStoredValue('SpotifyAccessToken');
}

var spotifyLogin = function () {
    getSpotifyAuthUrl(function (url) {
        window.location.href = url;
    });
}

var saveSpotifyTokens = function (data) {
    setStoredValue('SpotifyAccessToken', data.access_token);
    setStoredValue('SpotifyRefreshToken', data.refresh_token);
};

var spotifyRefresh = function () {

    var tokenData = {
        grant_type: 'refresh_token',
        refresh_token: getStoredValue('SpotifyRefreshToken')
    };

    getSpotifyTokenUrl(function (tokenUrl) {
        getSpotifyAuthHeader(function (authHeader) {
            $.ajax({
                url: tokenUrl,
                method: 'post',
                data: tokenData,
                dataType: 'json',
                headers: { 'Authorization': authHeader },
                success: function (token) {
                    saveSpotifyTokens(token);
                    window.location = "/?spr=true";
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    var errorMessage = XMLHttpRequest.responseJSON.error;
                    console.log(errorMessage);
                    if (errorMessage === 'invalid_grant') {
                        spotifyLogin();
                    }
                }
            });
        });
    });
};