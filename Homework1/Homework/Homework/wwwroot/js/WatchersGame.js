function AjaxRecheck() {
    console.log("WatchersGame");
    $.ajax({
        type: 'POST',
        url: '.. /../../Api/GetWatchersGameHtml',
        success: function (result) {
            console.log("Data receiver");
            console.log(result);
            $("#Watchers").html(result);
        }
    })
}
var t = setInterval(AjaxRecheck, 1000);