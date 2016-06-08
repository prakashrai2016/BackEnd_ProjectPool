$(document).ready(function () {
    var baseUrl = $('#baseUrl').val();

    $('#saveProject').click(function () {
        var ids = '';
        var valid = true;

        $('.tagCheckBox:checked').each(function () {
            ids = ids + ',' + $(this).val();
        });
        ids = ids.substring(1);
        $('#projectTags').val(ids);

        $('.mandatory').each(function () {
            var type = $(this).attr('type');

            if ($(this).val() == '') {
                valid = false;
                
                if (type == 'text' || type == 'file')
                {
                    $(this).css('border', '1px solid red');
                }

                else
                {
                    $(this).parent('td').css('border', '1px solid red');
                }
                
                
            }

            else {

                if (type == 'text' || type == 'file') {
                    $(this).css('border', '1px solid grey');
                }

                else {
                    $(this).parent('td').css('border', '0px solid black');
                }
            }
        });

        if (valid) {
            $('#projectForm').serialize();
            $('#projectForm').submit();
        }

        else {
            alert('Please fill all mandatory fields');
        }
    });

    $("#projectName").keyup(function () {
        $('#projectName').removeAttr('class');
    });

    $('#btnSaveProject').click(function () {
        if (ValidateForm()) {
            SaveProject();
        }

    });

    $('#btnSaveNewStep').click(function () {
        SaveNewStep();
    });

    $('#btnSaveEditStep').click(function () {
        EditStep();
    });

    function SaveNewStep(){
        $("#ProjectStepForm").attr("action", baseUrl + "Project/NewStep");
        $('#ProjectStepForm').submit();
        AjaxLoading()
    }

    function EditStep() {
        $("#ProjectStepForm").attr("action", baseUrl + "Project/EditStep");
        $('#ProjectStepForm').submit();
        AjaxLoading();
    }

    $('#btnAddReference').click(function () {

        if (ValidateForm()) {
            $('#addReference').val('Y');
            SaveProject();
        }
    });

    function SaveProject() {
        var ids = '';

        $('.tagCheckBox:checked').each(function () {
            ids = ids + ',' + $(this).val();
        });
        ids = ids.substring(1);
        $('#ProjectTags').val(ids);
        $('#ProjectForm').submit();
        AjaxLoading()
    }

    function ValidateForm() {
        return true;
    }

    $('#btnList').click(function () {
        window.location = '/Project';
    });


    $('#btnProjectDetails').click(function () {
        window.location = baseUrl + 'Project/Detail/' + $('#projectGUID').val();
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

    $('#btnProjectSteps').click(function () {
        var guid = $('#GUID').val();
        window.location = baseUrl + 'Project/Steps/' + guid;
    });

    $('#AddStep').click(function () {
        window.location = baseUrl + 'Project/NewStep/' + $('#projectGUID').val();
    });

    $('.stepList').click(function () {
        var id = $(this).attr('id').split('_')[1];
        StepDetail(id);
    });

    function StepDetail(id)
    {
        AjaxLoading();
        $.ajax({
            type: 'POST',
            url: baseUrl + 'Project/StepDetail/' + id,
            async: false,
            error: function () {
                ModalMessage("divLoader", "Error occurred while calling ajax.");
            },
            success: function (data) {
                $("#divStepDetail").html('');
                $("#divStepDetail").html(data);
                $('.stepList').css('background-color', '#eee;');
                $('#Step_' + $('#ProjectStepId').val()).css('background-color', '#68A4C4');
                CKEDITOR.replace('Description', {
                    "filebrowserImageUploadUrl": "/Article/UploadTempImage",
                    extraPlugins: 'mathjax',
                    mathJaxLib: 'http://cdn.mathjax.org/mathjax/2.2-latest/MathJax.js?config=TeX-AMS_HTML',
                    height: 620
                });

                CKEDITOR.editorConfig = function (config) {
                    config.extraPlugins = 'mathjax';
                    config.toolbar_Full[8] = ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Mathjax'];
                }
                EndAjaxLoading();
            }
        });
    }

    function LoadPartial() {
        AjaxLoading();
        $.ajax({
            type: 'POST',
            data: $('#ProjectListForm').serialize(),
            url: baseUrl + '/Project/Search/',
            async: false,
            error: function () {
                ModalMessage("divLoader", "Error occurred while calling ajax.");
            },
            success: function (data) {
                $("#divProjectList").html('');
                $("#divProjectList").html(data);
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