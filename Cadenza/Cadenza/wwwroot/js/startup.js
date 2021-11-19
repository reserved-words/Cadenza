
var showOnHover = function (row, show) {
    var cell = row.find('td .show-on-hover');
    if (!cell)
        return;

    cell.css('display', show ? 'block' : 'none');
};

var connectProviders = function(){

    if (!doesUrlContain("player")) {

        var spl = getParam("spl");
        var spr = getParam("spr");
        var lfm = getParam("lfm");

        if (!spl && !isSpotifyLoggedIn()) {
            spotifyLogin();
            return;
        }
        else if (!lfm && !isLastFmLoggedIn()) {
            lastFmLogin();
            return;
        }
        else if (!spr) {
            spotifyRefresh();
            return;
        }

        window.location.href = "/player";
    }

    //// Need to make sure app can handle if permissions revoked for either account while in use
}

$(function () {

    $('body').on('mouseenter', 'tr', function () {
        showOnHover($(this), true);
    });

    $('body').on('mouseleave', 'tr', function () {
        showOnHover($(this), false);
    });

    $.getJSON("../appsettings.json", function (settings) {
        apiSettings = settings.PlayerApi;
        connectProviders();
    });

});