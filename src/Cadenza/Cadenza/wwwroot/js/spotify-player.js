
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

async function startSpotifyPlayer(accessToken) {

    var storeDeviceId = {
        "Value": "TEST",
        "Updated": new Date()
    };

    window.localStorage.setItem("SpotifyDeviceId", JSON.stringify(storeDeviceId));
    return true;

    //const { Player } = await waitForSpotifyWebPlaybackSDKToLoad();

    //console.log("The Web Playback SDK has loaded.");

    //const sdk = new Player({
    //    name: "Cadenza",
    //    volume: 1.0,
    //    getOAuthToken: callback => { callback(accessToken); }
    //});

    //sdk.on("authentication_error", ({ message }) => {
    //    // This happens if the access token is invalid or null
    //    console.log('authentication error: ' + message);
    //});

    //sdk.on('initialization_error', ({ message }) => {
    //    console.error('Failed to initialize', message);
    //});

    //sdk.on('account_error', ({ message }) => {
    //    console.error('Failed to validate Spotify account', message);
    //});

    //sdk.on('playback_error', ({ message }) => {
    //    console.error('Failed to perform playback', message);
    //});

    //sdk.on("ready", ({ device_id }) => {
    //    console.log("Device ID = " + device_id);
    //    window.localStorage.setItem('SpotifyDeviceId', device_id);
    //});

    //return await sdk.connect();
}