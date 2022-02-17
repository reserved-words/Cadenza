
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


    const { Player } = await waitForSpotifyWebPlaybackSDKToLoad();

    const sdk = new Player({
        name: "Cadenza",
        volume: 1.0,
        getOAuthToken: callback => { callback(accessToken); }
    });

    sdk.on("authentication_error", ({ message }) => {
        // This happens if the access token is invalid or null
        console.log('Authentication error: ' + message);
    });

    sdk.on('initialization_error', ({ message }) => {
        console.error('Failed to initialize', message);
    });

    sdk.on('account_error', ({ message }) => {
        console.error('Failed to validate Spotify account', message);
    });

    sdk.on('playback_error', ({ message }) => {
        console.error('Failed to perform playback', message);
    });

    sdk.on("ready", ({ device_id }) => {
        var storeDeviceId = {
            "Value": device_id,
            "Updated": new Date()
        };

        window.localStorage.setItem("SpotifyDeviceId", JSON.stringify(storeDeviceId));
    });

    try {
        let connected = await sdk.connect();

        return connected;
    }
    catch (err) {
        console.log(err);
        return false;
    }
}