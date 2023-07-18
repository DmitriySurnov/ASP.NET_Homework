function AjaxRecheck(count) {
    console.log("NumberPlayersInGame");
    $.ajax({
        type: 'POST',
        url: '.. /../../Api/NumberPlayersInGame',
        success: function (result) {
            console.log("Data receiver");
            console.log(result);
            if (result != count) {
                location.reload();
            }
        }
    })
}
function runRequest(count) {
    var t = setInterval(AjaxRecheck, 1000, count);
}
