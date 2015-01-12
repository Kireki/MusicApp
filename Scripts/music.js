function NextTrack(id) {
    var widgetUrlTemplate = "http://api.soundcloud.com/tracks/<trackID>";
    var newWidgetUrl = widgetUrlTemplate.replace("<trackID>", id);
    var scWidget = window.SC.Widget(document.querySelector(".SCiframe"));
    scWidget.load(newWidgetUrl, {
        auto_play: true,
        show_comments: false,
        show_playcount: false,
        visual: true,
        hide_related: true,
    });
    scWidget.bind(window.SC.Widget.Events.FINISH, function() {
        console.log("Next track");
        getNextId();
    });
    scWidget.bind(window.SC.Widget.Events.READY, function () {
        var vol = window.currentVolume;
        scWidget.setVolume(vol);
    });
    var bgImageUrl = window.currentTrack.artwork;
    if (bgImageUrl) {
        bgImageUrl = bgImageUrl.replace("-large", "-t500x500");
    } else {
        bgImageUrl = "//pbs.twimg.com/profile_background_images/378800000066794871/552136d7a05cc09b852f0597612993b7.jpeg";
    }
    document.querySelector(".background-image").style.backgroundImage = "url('" + bgImageUrl + "')";
    var isError;
    scWidget.getCurrentSound(function(current) {
        if (current == null || current == "") {
            isError = true;
        }
    });
    if (isError) {
        getNextId();
    }
}

function getNextId() {
    $.ajax({
        url: 'Home/GetNextTrack',
        type: 'GET',
        dataType: 'json',
        success: function(data) {
            window.currentTrack = data;
            NextTrack(data.id);
        }
    });
}

function blacklistTrack() {
    getNextId();
    $.ajax({
        url: 'Home/Blacklist',
        type: 'POST',
        dataType: 'json',
        data: { trackId: window.currentTrack.id },
        success: function(data) {
            console.log("track blacklisted");
        },
        error: function(data) {
            console.log("there was an error blacklisting the track");
        }
    });
}

var currentVolume = 1;
var currentTrack = {};
document.querySelector(".next-track-button").addEventListener("click", getNextId);
document.querySelector(".blacklist-button").addEventListener("click", blacklistTrack);
var scWidget = SC.Widget(document.querySelector(".SCiframe"));
scWidget.bind(SC.Widget.Events.FINISH, getNextId);
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