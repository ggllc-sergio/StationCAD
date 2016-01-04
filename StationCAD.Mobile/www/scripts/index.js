// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkID=397704
// To debug code on page load in Ripple or on Android devices/emulators: launch your app, set breakpoints, 
// and then run "window.location.reload()" in the JavaScript Console.
(function () {
    "use strict";

    document.addEventListener( 'deviceready', onDeviceReady.bind( this ), false );

    function onDeviceReady() {
        // Handle the Cordova pause and resume events
        document.addEventListener( 'pause', onPause.bind( this ), false );
        document.addEventListener( 'resume', onResume.bind( this ), false );
        
        // TODO: Cordova has been loaded. Perform any initialization that requires Cordova here.
        document.addEventListener('deviceready', function () {
            var Pushbots = PushbotsPlugin.initialize("567aa8e1177959463e8b456e", { "android": { "sender_id": "98833261714" } });
        }, false);

        // Should be called once the device is registered successfully with Apple or Google servers
        Pushbots.on("registration", function (token) {
            console.log(token);
        });

        // Should be called once app receive the notification
        Pushbots.on("notification:received", function (data) {
            console.log("received:" + JSON.stringify(data));
        });

        // Should be called once the notification is clicked
        // **important** Doesn't work with iOS while app is closed, will be fixed in 1.3.1
        Pushbots.on("notification:clicked", function (data) {
            console.log("clicked:" + JSON.stringify(data));
        });
    };

    function onPause() {
        // TODO: This application has been suspended. Save application state here.
    };

    function onResume() {
        // TODO: This application has been reactivated. Restore application state here.
    };


} )();