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
                            "class": "thumb-image"
                        }).appendTo(imgDiv);

                        $("<a>", {
                            "href": "#",
                            "class": "btn btn-small",
                            "text": "Изтрий"
                        }).on("click", function () {

                            $(this).closest("div").remove();
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