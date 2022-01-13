
window.onSpotifyWebPlaybackSDKReady = () => {

    var accessToken = getStoredValue('SpotifyAccessToken');

    var player = new Spotify.Player({
        name: 'Whip',
        getOAuthToken: cb => { cb(accessToken); }
    });

    // Error handling
    player.addListener('initialization_error', ({ message }) => { console.log('initialization error: ' + message); });
    player.addListener('authentication_error', ({ message }) => { console.log('authentication error: ' + message); });
    player.addListener('account_error', ({ message }) => { console.log('account error: ' + message); });
    player.addListener('playback_error', ({ message }) => { console.log('playback error: ' + message); });

    // Playback status updates
    player.addListener('player_state_changed', state => { console.log(state); });

    // Ready
    player.addListener('ready', ({ device_id }) => {
        console.log('Device ID = ' + device_id);
        setStoredValue('SpotifyDeviceId', device_id);
    });

    // Not Ready
    player.addListener('not_ready', ({ device_id }) => {
        console.log('Device ID has gone offline', device_id);
    });

    player.connect();
};

