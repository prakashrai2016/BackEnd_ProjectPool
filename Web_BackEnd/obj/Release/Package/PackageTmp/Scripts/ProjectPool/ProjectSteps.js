$(document).ready(function () {
    var step = 1;
    $('#addMoreSteps').live('click', function () {
        var stepDiv = "<h2>Step " + (step +1) + "</h2><br/>"
        stepDiv += "<div id='step_'" + step + "'>";
        stepDiv += "<label>Step Title</label><br/>";
        stepDiv += "<input type = 'text' name='projectStepList[" + step + "].Title' style='width:100%;'/><hr/>";

        stepDiv += "<label>Image</label><br />";
        stepDiv += "<input type='file' name='projectStepList[" + step + "].uploadStepImage'/><hr/>";

        stepDiv += "<label>Description</label>";
        stepDiv += "<textarea name='projectStepList[0].Description' id='ckTextarea_" + step + "' class='ckTextarea'></textarea><hr/>";
        stepDiv += "</div>";

        $("#projectSteps").append(stepDiv);
        CKEDITOR.replace("ckTextarea_" + step);
        step += 1;
    });

    $('#complete').live('click', function () {
        $('#projectStepForm').serialize();
        $('#projectStepForm').submit();
    });
});