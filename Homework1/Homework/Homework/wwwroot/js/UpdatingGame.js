function AjaxRecheck() {
    console.log("UpdatingGame");
    if ($("#IsCurrentPlayerMove").get(0).value == "False") {
        let field = $("#FieldString").get(0).value;
        $.ajax({
            type: 'POST',
            url: '.. /../../Api/GetUpdatingGameHtml/' + field,
            success: function (result) {
                console.log("Data receiver");
                console.log(result);
                $("#Game").html(result);
            }
        })
    }
}
var t = setInterval(AjaxRecheck, 1000);