class ClipboardHelper {
    static dotNetHelper;

    static setDotNetHelper(value) {
        ClipboardHelper.dotNetHelper = value;
    }

    static setPasteImgHook() {
        //ref:
        //https://ourcodeworld.com/articles/read/491/how-to-retrieve-images-from-the-clipboard-with-javascript-in-the-browser 
        //https://www.techiedelight.com/paste-image-from-clipboard-using-javascript/
        //https://pjchender.dev/webapis/webapis-blob-file/

        document.onpaste = function (pasteEvent) {
            // consider the first item (can be easily extended for multiple items)
            var item = pasteEvent.clipboardData.items[0];

            if (item.type.indexOf("image") === 0) {
                var blob = item.getAsFile();

                var reader = new FileReader();
                reader.onload = function (event) {
                    const dataURL = event.target.result; // Base64 Image
                    //document.getElementById("container").src = dataURL;                    

                    //TODO: send Base64 Image to Blazor
                    ClipboardHelper.dotNetHelper.invokeMethodAsync('JsSendBase64ImgAsync', dataURL);
                };

                reader.readAsDataURL(blob);
            }
        }
    }
}
window.ClipboardHelper = ClipboardHelper;