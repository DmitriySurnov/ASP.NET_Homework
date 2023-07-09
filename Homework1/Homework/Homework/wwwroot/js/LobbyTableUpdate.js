function AjaxRecheck(TableGuid, NumberTable, divId) {
    console.log("LobbyTableUpdate");
    $.ajax({
        type: 'POST',
        url: '.. /../../Api/GetTableHtml/' + TableGuid + "/" + NumberTable,
        success: function (result) {
            console.log("Data receiver");
            console.log(result);
            $(divId).html(result);
        }
    })
}
function SetUpdater(TableGuid, NumberTable,divId) {
    var t = setInterval(AjaxRecheck, 1000, TableGuid, NumberTable, divId);
}
