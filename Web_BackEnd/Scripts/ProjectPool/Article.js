$(document).ready(function () {
    var baseUrl = $('#baseUrl').val();

    $('#btnSaveArticle').click(function () {
        if (ValidateForm())
        {
            SaveArticle();
        }
        
    }); 

    $('#btnAddReference').click( function () {
        
        if (ValidateForm()) {
            $('#addReference').val('Y');
            SaveArticle();
        }
    });

    function SaveArticle()
    {
        var ids = '';

        $('.tagCheckBox:checked').each(function () {
            ids = ids + ',' + $(this).val();
        });
        ids = ids.substring(1);
        $('#ArticleTags').val(ids);
        $('#ArticleForm').submit();
        AjaxLoading()
    }

    function ValidateForm()
    {
        return true;
    }

    $('#btnList').click(function () {
        window.location = '/Article';
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

    

    function LoadPartial()
    {
        AjaxLoading();
        $.ajax({
            type: 'POST',
            data: $('#ArticleListForm').serialize(),
            url: baseUrl + 'Article/Search/',
            async: false,
            error: function () {
                ModalMessage("divLoader", "Error occurred while calling ajax.");
            },
            success: function (data) {
                $("#divArticleList").html('');
                $("#divArticleList").html(data);
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