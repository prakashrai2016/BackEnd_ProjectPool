function AjaxLoading() {
    $("#dialog_progress").dialog({
        modal: true,
        width: 100,
        height: 175,
        resizable: false,
        autosize: false
    }).siblings('.ui-dialog-titlebar').remove().css("z-index", 1005);
    //   $("#dialog_progress").parent().css('top', 270)

    $('.ui-widget-overlay').css('height', $('#contentRender').css('height'));
}



function EndAjaxLoading() {
    $("#dialog_progress").dialog("destroy");
}

function ModalMessage(divname, message, title) {
    var Dv = "<div id='dialog-message' style='padding:10px'>" + message + "</div>";
    $("#" + divname).append(Dv);
    $("#dialog:ui-dialog").dialog("destroy");
    $("#dialog-message").dialog({
        title: title,
        closeOnEscape: true,
        modal: true,
        resizable: false,
        buttons: {
            Ok: function () {
                $(this).dialog("close");
                $(".ui-dialog-content").dialog("close");
            }
        }
    });

    $('.ui-widget-overlay').css('height', $('#contentRender').css('height'));
    // $(".ui-dialog-title").html("This is test");
    $(".ui-dialog-titlebar").css("font-size", "10px");
    $(".ui-dialog-titlebar").css("background", "#204562");
    $(".ui-dialog-titlebar").css("color", "#ffffff");
    $(".ui-dialog-titlebar").css("height", "20px");
    //$(".ui-dialog-buttonpane").css("background", "none");
    //$(".ui-dialog-buttonset button").addClass("toolBarButton");
    $(".ui-dialog-buttonset button").css("color", "white");
    $(".ui-dialog-buttonset button").css("background", "#204562");
    $(".ui-dialog-buttonset button").css("border-radius", "3px");
    $(".dialog-message").css("background", "none");

}