﻿//$(function () {
//    $('[name="searchTerm"]').keyup(function () {
//        $('#ajaxForm').submit()
//    })
//})

$(function () {
    let delayTimer;
    $('[name="searchTerm"]').keyup(function () {
        clearTimeout(delayTimer);
        delayTimer = setTimeout(function () {
            $('#ajaxForm').submit();
        }, 2000);
    });
})

function OnDeleteClick(elem) {
    $(elem).closest("div").remove()
}

$(document).ready(function () {
    $("#fileUpload").on("change", function () {
        //Get count of selected files
        var countFiles = $(this)[0].files.length;
        var entireUrl = $(this)[0].value;
        var extn = entireUrl.substring(entireUrl.lastIndexOf('.') + 1).toLowerCase();
        var image_holder = $("#image-holder");

        if (extn == "gif" || extn == "png" || extn == "jpg" || extn == "jpeg") {
            if (typeof (FileReader) != "undefined") {
                //loop for each file selected for uploaded.
                for (var i = 0; i < countFiles; i++) {


                    //START CHECK SIZE
                    var size = $(this)[0].files[i].size;
                    var name = $(this)[0].files[i].name;
                    console.log("file size of" + name + " is " + size);
                    //END CHECK SIZE


                    var reader = new FileReader();
                    reader.onload = function (e) {
                        var imgDiv = $("<div>", {
                            "class": "col-md-3 text-center"
                        });

                        $("<input />", {
                            "type": "hidden",
                            "value": e.target.fileName,
                            "name": "ImageFileNames"
                        }).appendTo(imgDiv);

                        $("<input />", {
                            "type": "hidden",
                            "value": e.target.result,
                            "name": "ImageFilesData"
                        }).appendTo(imgDiv);

                        $("<img />", {
                            "src": e.target.result,
                            "class": "thumb-image",
                            "alt": e.target.fileName
                        }).appendTo(imgDiv);

                        $("<a>", {
                            "href": "#",
                            "class": "btn btn-sm",
                            "text": "Изтрий",
                            "onclick": "OnDeleteClick(this)"
                        }).appendTo(imgDiv);

                        imgDiv.appendTo(image_holder);
                    }
                    image_holder.show();
                    reader.fileName = $(this)[0].files[i].name;
                    reader.readAsDataURL($(this)[0].files[i]);
                }
            } else {
                alert("Браузърът не поддържа FileReader!");
            }
        } else {
            alert("Моля, избирайте само изображения!");
        }
    });
});