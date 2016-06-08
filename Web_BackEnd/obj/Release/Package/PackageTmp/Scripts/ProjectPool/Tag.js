$(document).ready(function () {
    var baseUrl = $('#baseUrl').val();

    function NewTagPopUp() {
        $("#dialog_newTag").dialog({
            title: "Add New Tag",
            closeOnEscape: true,
            modal: true,
            resizable: false,
            buttons: {
                Ok: function () {
                    $('#newTagForm').submit();
                    //AjaxLoading();
                    //$.ajax({
                    //    type: 'POST',
                    //    data: $('#newTagForm').serialize(),
                    //    url: 'http://localhost:1048/Tag/New/',
                    //    async: false,
                    //    error: function () {
                    //        ModalMessage("divLoader", "Error occurred while calling ajax.");
                    //    },
                    //    success: function (data) {
                    //        alert('OK');
                    //        EndAjaxLoading();
                    //        $(this).dialog("close");
                    //        $(".ui-dialog-content").dialog("close");
                    //    }
                    //});
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
        $(".ui-widget-content").css("width", "420px");

    }

    $('#btnSaveTag').click(function () {
        if (ValidateForm()) {
            SaveTag();
        }

    });

    $('#btnAddReference').click(function () {

        if (ValidateForm()) {
            $('#addReference').val('Y');
            SaveTag();
        }
    });

    function SaveTag() {
        var ids = '';

        $('.tagCheckBox:checked').each(function () {
            ids = ids + ',' + $(this).val();
        });
        ids = ids.substring(1);
        $('#TagTags').val(ids);
        $('#TagForm').submit();
        AjaxLoading()
    }

    function ValidateForm() {
        return true;
    }

    $('#btnList').click(function () {
        window.location = '/Tag';
    });

    $('#btnSearch').click(function () {
        SetHiddenFields();
        LoadPartial();
    });

    $('#nxtPage').click(function () {
        $("#txtPageNumber").val(parseInt($("#txtPageNumber").val()) + 1);
        SetHiddenFields();
        LoadPartial();
    });

    $('#bckPage').click(function () {
        $("#txtPageNumber").val(parseInt($("#txtPageNumber").val()) - 1);
        SetHiddenFields();
        LoadPartial();
    });

    function LoadPartial() {
        AjaxLoading();
        $.ajax({
            type: 'POST',
            data: $('#TagListForm').serialize(),
            url: baseUrl + 'Tag/Search/',
            async: false,
            error: function () {
                ModalMessage("divLoader", "Error occurred while calling ajax.");
            },
            success: function (data) {
                $("#divTagList").html('');
                $("#divTagList").html(data);
                EndAjaxLoading();
            }
        });
    }

    function SetHiddenFields() {
        $('#SearchString').val($("#txtSearchString").val());
        $('#PageSize').val($("#pageSizeSelect").val());
        $('#PageNumber').val($("#txtPageNumber").val());
    }

});
