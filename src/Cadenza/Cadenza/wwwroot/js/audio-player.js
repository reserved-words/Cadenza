
class BrowserPlayerControls {
    static dotNetHelper;

    static setDotNetHelper(value) {
        BrowserPlayerControls.dotNetHelper = value;
    }

    static async resume() {
        await BrowserPlayerControls.dotNetHelper.invokeMethodAsync('Resume');
    }

    static async pause() {
        await BrowserPlayerControls.dotNetHelper.invokeMethodAsync('Pause');
    }

    static async previous() {
        await BrowserPlayerControls.dotNetHelper.invokeMethodAsync('SkipPrevious');
    }

    static async next() {
        await BrowserPlayerControls.dotNetHelper.invokeMethodAsync('SkipNext');
    }
}

window.BrowserPlayerControls = BrowserPlayerControls;


var play = function (url, track, artist, playlist, artworkUrl){

    var a = document.getElementById("audioPlayer");

    if (url) {
        a.src = url;

        if ('mediaSession' in navigator) {

            navigator.mediaSession.metadata = new MediaMetadata({
                title: track,
                artist: artist,
                album: playlist,
                artwork: [
/*                    { src: artworkUrl, sizes: '96x96', type: 'image/png' }*/
                ]
            });

            navigator.mediaSession.setActionHandler('play', function () {
                window.BrowserPlayerControls.resume();
            });

            navigator.mediaSession.setActionHandler('pause', function () {
                window.BrowserPlayerControls.pause();
            });

            navigator.mediaSession.setActionHandler('previoustrack', function () {
                window.BrowserPlayerControls.previous();
            });

            navigator.mediaSession.setActionHandler('nexttrack', function () {
                window.BrowserPlayerControls.next();
            });
        }
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