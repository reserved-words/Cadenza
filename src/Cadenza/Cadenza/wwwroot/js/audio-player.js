

var play = function (url){

    var a = document.getElementById("audioPlayer");

    if (url) {
        a.src = url;
    }

    a.play();
    return progress(a);
}

var pause = function () {
    var a = document.getElementById("audioPlayer");
    a.pause();
    return progress(a);
}

var stop = function () {
    var a = document.getElementById("audioPlayer");
    a.pause();
    return progress(a);
}

var progress = function (player) {
    return {
        SecondsPlayed: player.currentTime ? Math.floor(player.currentTime) : 0,
        TotalSeconds: player.duration ? Math.floor(player.duration) : 0
    }
}
