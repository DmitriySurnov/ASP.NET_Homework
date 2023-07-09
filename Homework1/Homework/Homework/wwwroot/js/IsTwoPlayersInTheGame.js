function AjaxRecheck() {
    console.log("IsTwoPlayersInTheGame");
    $.ajax({
        type: 'POST',
        url: '.. /../../Api/IsTwoPlayersInTheGame',
        success: function (result) {
            console.log("Data receiver");
            console.log(result);
            if (!result) {
                $("#WaitingPlayers").trigger("click")
            }
        }
    })
}
var t = setInterval(AjaxRecheck, 1000);