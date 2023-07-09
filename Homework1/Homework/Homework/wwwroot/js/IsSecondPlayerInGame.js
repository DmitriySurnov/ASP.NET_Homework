function AjaxRecheck() {
    console.log("IsSecondPlayerInGame");
    $.ajax({
        type: 'POST',
        url: '.. /../../Api/IsTwoPlayersInTheGame',
        success: function (result) {
            console.log("Data receiver");
            console.log(result);
            if (result) {
                location.reload();
            }
        }
    })
}
var t = setInterval(AjaxRecheck, 1000);