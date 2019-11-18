function ImgPreview(input) {
    if (input.files[0]) {
        var uploading = new FileReader();
        uploading.onload = function (displayImg) {
            $("#img-sound").attr("src", displayImg.target.result)
        }
        uploading.readAsDataURL(input.files[0])
    }
}