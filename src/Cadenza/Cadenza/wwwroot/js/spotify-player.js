
window.onSpotifyWebPlaybackSDKReady = () => {};

async function waitForSpotifyWebPlaybackSDKToLoad() {
    return new Promise(resolve => {
        if (window.Spotify) {
            resolve(window.Spotify);
        } else {
            window.onSpotifyWebPlaybackSDKReady = () => {
                resolve(window.Spotify);
            };
        }
    });
}

async function startSpotifyPlayer() {

    const { Player } = await waitForSpotifyWebPlaybackSDKToLoad();

    console.log("The Web Playback SDK has loaded.");

    if (window.location.href.indexOf("player") < 0) {
        return;
    }

    var accessToken = window.localStorage.getItem('SpotifyAccessToken');

    const sdk = new Player({
        name: "Cadenza",
        volume: 1.0,
        getOAuthToken: callback => { callback(accessToken); }
    });

    sdk.on("authentication_error", ({ message }) => {
        // This happens if the access token is invalid or null
        console.log('authentication error: ' + message);
    });

    sdk.on("ready", ({ device_id }) => {
        console.log("Device ID = " + device_id);
        window.localStorage.setItem('SpotifyDeviceId', device_id);
    });

    let connected = await sdk.connect();
    return connected;
}

//(async () => {

//    var result = await startSpotifyPlayer();
//    console.log("RESULT: " + result);
//    return result;
    
//})();
