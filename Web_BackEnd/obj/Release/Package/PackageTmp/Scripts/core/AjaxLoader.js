///Author  : Nabin Aryal
///Company : Procit (ITH Nepal)
function AjaxLoading() {
    EndAjaxLoading();

    $("#dialog_progress").dialog({
        modal: true,
        width: 150,
        height: 130,
        resizable: false,
        autosize: false
    }).siblings('.ui-dialog-titlebar').remove().css("z-index", 1005);
}

function EndAjaxLoading() {
    $("#dialog_progress").dialog("destroy");
}

function ModalMessage(divname, message) {
    var Dv = "<div id='dialog-message' style='padding:10px'>" + message + "</div>";
    $("#" + divname).append(Dv);
    $("#dialog:ui-dialog").dialog("destroy");
    $("#dialog-message").dialog({
        title: "Smartflow",
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

   // $(".ui-dialog-title").html("Smartflow");
    $(".ui-dialog-titlebar").css("font-size", "10px");
    $(".ui-dialog-titlebar").css("background", "#0F628C");
    $(".ui-dialog-titlebar").css("color", "#ffffff");
    $(".ui-dialog-buttonpane").css("background", "none");
    $(".dialog-message").css("background", "none");
}

function ErrorModalMessage(divname, message) {
    var Dv = "<div id='dialog-message' style='padding:10px'>" + message + "</div>";
    $("#" + divname).append(Dv);
    $("#dialog:ui-dialog").dialog("destroy");
    $("#dialog-message").dialog({
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

    $(".ui-dialog-title").html(Translation("label-Error"));
    $(".ui-dialog-titlebar").css("font-size", "10px");
    $(".ui-dialog-titlebar").css("background", "#FE0000");
    $(".ui-dialog-titlebar").css("color", "#ffffff");
    $(".ui-dialog-buttonpane").css("background", "none");
    $(".dialog-message").css("background", "none");
}

function ModalConfirm(divname, message) {
    var Dv = "<div id='dialog-message' style='padding:10px'>" + message + "</div>";
    $("#" + divname).append(Dv);
    $("#dialog:ui-dialog").dialog("destroy");
    $("#dialog-message").dialog({
        closeOnEscape: true,
        modal: true,
        resizable: false,
        async: false,
        buttons: {
            Ok: function () {
                $(this).dialog("close");
                $(".ui-dialog-content").dialog("close");
                callbackSuccessConformDialog();
            },
            Annuleren: function () {
                $(this).dialog("close");
                $(".ui-dialog-content").dialog("close");
                return false;
            }
        }
    });

    $(".ui-dialog-titlebar").attr("innerHTML", "Smartflow");
    $(".ui-dialog-titlebar").css("font-size", "10px");
    $(".ui-dialog-titlebar").css("background", "#0F628C");
    $(".ui-dialog-titlebar").css("color", "#ffffff");
    $(".ui-dialog-buttonpane").css("background", "none");
    $(".dialog-message").css("background", "none");
}

function UIModalConfirm(divname, message) {
    var returnValue = false;
    var Dv = "<div id='dialog-message' style='padding:10px'>" + message + "</div>";
    $("#" + divname).append(Dv);
    $("#dialog-message").dialog({
        closeOnEscape: true,
        modal: true,
        resizable: false,
        async: false,
        buttons: {
            Opslaan: function () {
                $(this).dialog("close");
                $(".ui-dialog-content").dialog("close");
                callbackSuccess();
            },
            Annuleren: function () {
                $(this).dialog("close");
            }
        }
    });

    $(".ui-dialog-title").html("SmartFlow");
    $(".ui-dialog-titlebar").css("font-size", "11px");
    $(".ui-dialog-titlebar").css("background", "#0F628C");
    $(".ui-dialog-titlebar").css("color", "#ffffff");
    $(".ui-dialog-buttonpane").css("background", "none");
}

//This function is called when we need different header name in different dialog//
function PopUpCode(heading) {
    $(".ui-dialog-titlebar").show();
    $(".ui-dialog-title").html(heading);
    $(".ui-dialog-titlebar").css("font-size", "11px");
    $(".ui-dialog-titlebar").css("background", "#0F628C");
    $(".ui-dialog-titlebar").css("color", "#ffffff");
    $(".ui-dialog-buttonpane").css("background", "none");
}

//This function is called when we need new model message over existing//
function UIModalMessage(divname, message) {
    var Dv = "<div id='dialog-message' style='padding-left:10px;padding-top:10px'>" + message + "</div>";
    $("#" + divname).append(Dv);
    $("#dialog-message").dialog({
        title:"SmartFlow",
        closeOnEscape: true,
        modal: true,
        resizable: false,
        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        }
    });

    //$(".ui-dialog-title").html("SmartFlow");
    $(".ui-dialog-titlebar").css("font-size", "11px");
    $(".ui-dialog-titlebar").css("background", "#0F628C");
    $(".ui-dialog-titlebar").css("color", "#ffffff");
    $(".ui-dialog-buttonpane").css("background", "none");
}

function MouseHover() {
    $("table > tbody tr").css("padding-top", "50px");
    $("table > tbody tr").css("padding-bottom", "50px");

    $("table > tbody tr").mouseover(function () {
        $(this).css("background-color", "#F0EDEC");
        $(this).css("cursor", "tr");
        $("table.noCSS > tbody tr").css("background-color", "#FAFAFA");
    });

    $("table >tbody tr").mouseout(function () {
        $(this).css("background-color", "");
        $("table.noCSS > tbody tr").css("background-color", "#FAFAFA");
    });
}

function ModalMessage1(divname, message) {
    var Dv = "<div id='dialog-message' style='padding:10px'>" + message + "</div>";
    $("#" + divname).append(Dv);

    $("#dialog-message").dialog({
        title: "Smartflow",
        closeOnEscape: true,
        modal: true,
        resizable: false,
        buttons: {
            Ok: function () {
                $(this).dialog("close");
                $(".ui-dialog-content").first().dialog("close");
            }
        }
    });

   // $(".ui-dialog-title").html("Smartflow");
    $(".ui-dialog-titlebar").first().css("font-size", "10px");
    $(".ui-dialog-titlebar").first().css("background", "#0F628C");
    $(".ui-dialog-titlebar").first().css("color", "#ffffff");
    $(".ui-dialog-buttonpane").css("background", "none");
    $(".dialog-message").css("background", "none");
}

function ShowOverlayView(divName, title, width, height) {
    if ($.browser.msie && parseInt($.browser.version, 10) === 7) {
        width = ($(document).width() / 2.5);
    }

    $("#" + divName).dialog({
        closeOnEscape: true,
        modal: true,
        width: width,
        height: height,
        resizable: false,
        draggable: true,
        position: "center",
        open: function (event, ui) {
            $("input").blur();
        }
    });

    $("#" + divName).prev().find(".ui-dialog-title").html(title);
    $("button .ui-dialog-titlebar-close").css("title", "sluiten");
    $(".ui-dialog-titlebar").css("font-size", "12px");
    $(".ui-dialog-titlebar").css("background", "#0F628C");
    $(".ui-dialog-titlebar").css("color", "#ffffff");
    $(".ui-dialog-titlebar").css("display", "block");
    $(".ui-dialog-buttonpane").css("background", "none");
    $(".ui-dialog-buttonpane").css("display", "none");
    $(".dialog-message").css("background", "none");
    $(".ui-widget-overlay").css("height", $(document).height());
}

//This function is called when we need new model message over existing//
function GeneralUIModalMessage(divname, message, callbackGeneralUIModalMessage) {
    var Dv = "<div id='dialog-message' style='padding-left:10px;padding-top:10px'>" + message + "</div>";
    $("#" + divname).append(Dv);
    $("#dialog-message").dialog({
        title:"SmartFlow",
        closeOnEscape: true,
        modal: true,
        resizable: false,
        buttons: {
            Ok: function () {
                $(this).dialog("close");
                callbackGeneralUIModalMessage();
            }
        }
    });

    //$(".ui-dialog-title").html("SmartFlow");
    $(".ui-dialog-titlebar").css("font-size", "11px");
    $(".ui-dialog-titlebar").css("background", "#0F628C");
    $(".ui-dialog-titlebar").css("color", "#ffffff");
    $(".ui-dialog-buttonpane").css("background", "none");
}

function GeneralUIModalConfirm(divname, message, okButtonName, cancelButtonName) {
    var returnValue = false;
    var Dv = "<div id='dialog-message' style='padding:10px'>" + message + "</div>";
    $("#" + divname).append(Dv);
    var modalButtons = {};
    modalButtons[okButtonName] = function () {
            $(this).dialog("close");
            $(".ui-dialog-content").dialog("close");
            callbackSuccess();
        }
    modalButtons[cancelButtonName] = function () {
            $(this).dialog("close");
        }
    $("#dialog-message").dialog({
        closeOnEscape: true,
        modal: true,
        resizable: false,
        async: false,
        buttons: modalButtons
    });

    $(".ui-dialog-title").html("SmartFlow");
    $(".ui-dialog-titlebar").css("font-size", "11px");
    $(".ui-dialog-titlebar").css("background", "#0F628C");
    $(".ui-dialog-titlebar").css("color", "#ffffff");
    $(".ui-dialog-buttonpane").css("background", "none");
}