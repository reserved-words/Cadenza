
class AudioHelpers {
    static dotNetHelper;

    static setDotNetHelper(value) {
        AudioHelpers.dotNetHelper = value;
    }

    static async dotNetPlay() {
        await AudioHelpers.dotNetHelper.invokeMethodAsync('JSPlay');
    }

    static async dotNetPause() {
        await AudioHelpers.dotNetHelper.invokeMethodAsync('JSPause');
    }

    static async dotNetPrevious() {
        await AudioHelpers.dotNetHelper.invokeMethodAsync('JSPrevious');
    }

    static async dotNetNext() {
        await AudioHelpers.dotNetHelper.invokeMethodAsync('JSNext');
    }
}

window.AudioHelpers = AudioHelpers;


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
                window.AudioHelpers.dotNetPlay();
            });

            navigator.mediaSession.setActionHandler('pause', function () {
                window.AudioHelpers.dotNetPause();
            });

            navigator.mediaSession.setActionHandler('previoustrack', function () {
                window.AudioHelpers.dotNetPrevious();
            });

            navigator.mediaSession.setActionHandler('nexttrack', function () {
                window.AudioHelpers.dotNetNext();
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