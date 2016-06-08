$(document).ready(function () {
    var id = 1;

    $("#addMorePhoto").live('click', function () {
        var count = parseInt($("#Count").val());
        $("#Count").val(count + 1);
  
        var newPhoto = "<div id='image_" + id + "' style='border:1px solid #808080;padding:10px;margin-top:15px;'>";
        newPhoto = newPhoto + "<b><label>Image</label></b><br /><input type='file' name='TravelImageList[" + id + "].uploadImage' id='uploadImage' /><hr />";
        newPhoto = newPhoto + "<b><label>ImageDescription</label></b><br /><input type='text' name='TravelImageList[" + id + "].ImageDescription' style='width:100%;'/><hr />";
        newPhoto = newPhoto + "<b><label>Short Description</label></b><br /><textArea name='TravelImageList[" + id + "].Description' id='Description_" + id + "'></textArea></div>";

        $("#travelImages").append(newPhoto);
        CKEDITOR.replace('Description_' + id);
        id = id + 1;
        $("#addMorePhoto").focus();
    });
});