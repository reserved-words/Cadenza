
var getStoredValue = function (key) {
    return localStorage.getItem(key);
};

var setStoredValue = function (key, value) {
    localStorage.setItem(key, value);
};

var put = function (url, data) {
    var json = JSON.stringify(data);

    $.ajax({
        url: url,
        method: 'put',
        data: json,
        headers: { 'Authorization': 'Bearer ' + getStoredValue('SpotifyAccessToken'), "Content-Type": "application/json" },
    });
};

var post = function (url, data) {
    var json = JSON.stringify(data);

    $.ajax({
        url: url,
        method: 'post',
        data: json,
        headers: { 'Authorization': 'Bearer ' + getStoredValue('SpotifyAccessToken'), "Content-Type": "application/json" },
    });
};

var get = function (url, onSuccess) {
    $.ajax({
        url: url,
        method: 'get',
        success: onSuccess,
        headers: { 'Authorization': 'Bearer ' + getStoredValue('SpotifyAccessToken') },
    });
};


var getParam = function (key) {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(key);
};

var doesUrlContain = function (value) {
    return window.location.href.indexOf(value) >= 0;
};
