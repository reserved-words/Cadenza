
var useTokenToFetchLastFmSessionKey = function (token) {

    getLastFmSessionKeyUrl(token, function (url) {
        $.ajax({
            url: url,
            method: 'get',
            success: function (data) {
                saveLastFmSessionKey(data);
                window.location.href = "/?lfm=true";
            }
        });
    });
};

var saveLastFmSessionKey = function (data) {

    var key = data.getElementsByTagName("key")[0].childNodes[0].nodeValue;
    setStoredValue('LastFmSessionKey', key);
};

$(function () {

    $.getJSON("../appsettings.json", function (settings) {
        apiSettings = settings.PlayerApi;
        var token = getParam('token');
        useTokenToFetchLastFmSessionKey(token);
    });

});