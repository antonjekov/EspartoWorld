var myWidget = cloudinary.createUploadWidget({
    cloudName: 'mielproject',
    uploadPreset: 'k21wtmfa',
    folder: 'EspartoWorld',
    sources: ['local', 'url', 'camera'],
    multiple: false,
    resourceType: 'image',
    cropping: 'true',
    maxImageFileSize: 500000,
    maxImageWidth: 1500,
    maxImageHeight: 800,
    croppingValidateDimensions: true,
    theme: 'minimal',
    // TO DO WIDGET TRANSLATE
    //language: 'es',
    //text: {
    //    'es': {
    //        'back': 'volver',
    //        "menu": {
    //            "camera": "Camara",
    //            "browse" : "Buscar"
    //        },
    //    },
    //},
    singleUploadAutoClose: true
}, (error, result) => {
    if (!error && result && result.event === "success") {
        document.getElementById("ImageUrl").value = result.info.secure_url;
        document.getElementById("imageGroup").style.visibility = "hidden";
        document.getElementById("courseImage").src = result.info.secure_url
    }
}
)

document.getElementById("upload_widget").addEventListener("click", function () {
    myWidget.open();
}, false);

