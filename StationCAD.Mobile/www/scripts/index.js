﻿// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkID=397704
// To debug code on page load in Ripple or on Android devices/emulators: launch your app, set breakpoints, 
// and then run "window.location.reload()" in the JavaScript Console.
(function () {
    "use strict";
    var Pushbots;
    document.addEventListener( 'deviceready', onDeviceReady.bind( this ), false );
    

    function onDeviceReady() {


        // Handle the Cordova pause and resume events
        document.addEventListener( 'pause', onPause.bind( this ), false );
        document.addEventListener( 'resume', onResume.bind( this ), false );
        
        // TODO: Cordova has been loaded. Perform any initialization that requires Cordova here.
        // PushBots integration
        document.addEventListener('deviceready', function () {
            Pushbots = PushbotsPlugin.initialize("567aa8e1177959463e8b456e", { "android": { "sender_id": "98833261714" } });
        }, false);
        
    };

    function onPause() {
        // TODO: This application has been suspended. Save application state here.
    };

    function onResume() {
        // TODO: This application has been reactivated. Restore application state here.
    };

} )();