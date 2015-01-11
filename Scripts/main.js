function NextTrack() {
    var urlTemplate = "http://api.soundcloud.com/tracks/<trackID>";
    window.trackIndex++;
    var newWidgetUrl = urlTemplate.replace("<trackID>", tracks[trackIndex].Id);
    var scWidget = SC.Widget(document.querySelector(".SCiframe"));
    scWidget.load(newWidgetUrl, {
        auto_play: true,
        show_comments: false,
        show_playcount: false,
        visual: true,
        hide_related: true,
    });
    scWidget.bind(SC.Widget.Events.FINISH, function() {
        console.log("Next track");
        NextTrack();
    });
    scWidget.bind(SC.Widget.Events.READY, function () {
        var vol = window.currentVolume;
        scWidget.setVolume(vol);
    });
    window.bgImageUrl = tracks[trackIndex].ArtworkUrl || "//pbs.twimg.com/profile_background_images/378800000066794871/552136d7a05cc09b852f0597612993b7.jpeg";
    document.querySelector(".background-image").style.backgroundImage = "url('" + bgImageUrl + "')";
    var isError;
    scWidget.getCurrentSound(function(current) {
        if (current == null || current == "") {
            isError = true;
        }
    });
    if (isError) {
        NextTrack();
    }
}

function BlacklistTrack() {
    var blacklistedIndex = window.trackIndex;
    NextTrack();
    $.ajax({
        url: 'Home/Blacklist',
        type: 'POST',
        dataType: 'json',
        data: { trackId: window.tracks[blacklistedIndex].Id },
        success: function(data) {
            console.log("track blacklisted");
        },
        error: function(data) {
            console.log("there was an error blacklisting the track");
        }
    });
}

var currentVolume = 1;
var currentTrackId = tracks[0].Id;
var trackIndex = 0;
var bgImageUrl = tracks[trackIndex].ArtworkUrl || "//pbs.twimg.com/profile_background_images/378800000066794871/552136d7a05cc09b852f0597612993b7.jpeg";
document.querySelector(".background-image").style.backgroundImage = "url('" + bgImageUrl + "')";
document.querySelector(".next-track-button").addEventListener("click", NextTrack);
document.querySelector(".blacklist-button").addEventListener("click", BlacklistTrack);
var scWidget = SC.Widget(document.querySelector(".SCiframe"));
scWidget.bind(SC.Widget.Events.FINISH, NextTrack);
var mySlider = new dhtmlXSlider({
    parent: "sliderObj",
    value: 100,
    step: 1,
    min: 0,
    max: 100,
    tooltip: true
});
mySlider.attachEvent("onchange", function (value) {
    window.currentVolume = value / 100;
    scWidget.setVolume(window.currentVolume);
});