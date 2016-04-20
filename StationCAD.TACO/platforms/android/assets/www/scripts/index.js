// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkID=397704
// To debug code on page load in Ripple or on Android devices/emulators: launch your app, set breakpoints, 
// and then run "window.location.reload()" in the JavaScript Console.
(function () {
    "use strict";

    document.addEventListener('deviceready', onDeviceReady.bind(this), false);

    //document.addEventListener('deviceready', oneSignalInit(), false);

    function onDeviceReady() {
        // Handle the Cordova pause and resume events
        document.addEventListener( 'pause', onPause.bind( this ), false );
        document.addEventListener( 'resume', onResume.bind( this ), false );
        
        // TODO: Cordova has been loaded. Perform any initialization that requires Cordova here.

        oneSignalInit();
    };

    function onPause() {
        // TODO: This application has been suspended. Save application state here.
    };

    function onResume() {
        // TODO: This application has been reactivated. Restore application state here.
    };

    function oneSignalInit() {
        // Enable to debug issues.
        window.plugins.OneSignal.setLogLevel({ logLevel: 4, visualLevel: 4 });

        var notificationOpenedCallback = function (jsonData) {
            console.log('didReceiveRemoteNotificationCallBack: ' + JSON.stringify(jsonData));
        };

        window.plugins.OneSignal.init("950a66d5-c457-4c9e-9a8b-cca7ef8b8c68",
                                       { googleProjectNumber: "98833261714" },
                                       notificationOpenedCallback);

        // Show an alert box if a notification comes in when the user is in your app.
        window.plugins.OneSignal.enableInAppAlertNotification(true);
    };
} )();