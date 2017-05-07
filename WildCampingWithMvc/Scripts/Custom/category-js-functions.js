$(document).ready(function () {
    $("#fileUpload").on("change", function () {
        //Get count of selected files
        var entireUrl = $(this)[0].value;
        var extn = entireUrl.substring(entireUrl.lastIndexOf('.') + 1).toLowerCase();
        var image_holder = $("#image-holder");

        if (extn == "gif" || extn == "png" || extn == "jpg" || extn == "jpeg") {
            if (typeof (FileReader) != "undefined") {
                image_holder.empty();
                //START CHECK SIZE
                var size = $(this)[0].files[0].size;
                var name = $(this)[0].files[0].name;
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
                        "name": "ImageFileName"
                    }).appendTo(imgDiv);

                    $("<input />", {
                        "type": "hidden",
                        "value": e.target.result,
                        "name": "ImageFileData"
                    }).appendTo(imgDiv);

                    $("<img />", {
                        "src": e.target.result,
                        "class": "thumb-image",
                        "alt": e.target.fileName
                    }).appendTo(imgDiv);

                    imgDiv.appendTo(image_holder);
                }
                image_holder.show();
                reader.fileName = $(this)[0].files[0].name;
                reader.readAsDataURL($(this)[0].files[0]);
            } else {
                alert("Браузърът не поддържа FileReader!");
            }
        } else {
            alert("Моля, избирайте само изображения!");
        }
    });
});