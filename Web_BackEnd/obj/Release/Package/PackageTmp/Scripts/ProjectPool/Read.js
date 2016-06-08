$(document).ready(function () {
    var baseUrl = $('#baseUrl').val();

    $('#btnSaveRead').click(function () {
        if (ValidateForm()) {
            SaveRead();
        }

    });


    function SaveRead() {
        var ids = '';

        $('#ReadForm').submit();
        AjaxLoading()
    }

    function ValidateForm() {
        return true;
    }

    $('#btnList').click(function () {
        window.location = '/Read';
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
            data: $('#ReadListForm').serialize(),
            url: baseUrl + 'Read/Search/',
            async: false,
            error: function () {
                ModalMessage("divLoader", "Error occurred while calling ajax.");
            },
            success: function (data) {
                $("#divReadList").html('');
                $("#divReadList").html(data);
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