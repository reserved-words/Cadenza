﻿
async function connectSpotifyPlayer(accessToken) {
    // for now just return true or false but change to return the device ID
    return await startSpotifyPlayer(accessToken);
}

var spotifyDeviceNotFound = function(){
    // TODO: Try to resolve by sorting out device issue
    return false;
}

var spotifyUnexpectedError = function () {
    alert("Unexpected error");
}