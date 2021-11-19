

var play = function (url){

    var a = document.getElementById("audioPlayer");

    if (url) {
        a.src = url;
    }

    a.play();
    return (a.currentTime ? Math.floor(a.currentTime) : 0) * 1000;
}

var pause = function () {
    var a = document.getElementById("audioPlayer");
    a.pause();
    return (a.currentTime ? Math.floor(a.currentTime) : 0) * 1000;
}

var stop = function () {
    var a = document.getElementById("audioPlayer");
    a.pause();
    return (a.currentTime ? Math.floor(a.currentTime) : 0) * 1000;
}
