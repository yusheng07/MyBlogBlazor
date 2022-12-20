class Md5Helper {
    static dotNetHelper;
    static isShowConsole;

    static setDotNetHelper(value) {
        Md5Helper.dotNetHelper = value;
    }

    static setIsShowConsole(value) {
        Md5Helper.isShowConsole = value;
    }

    static setInputComponent() {
        $('#filepicker').change(function () {
            alert('Parsing Files! Be patient!');
            //TODO: sent file count to Blazor
            Md5Helper.dotNetHelper.invokeMethodAsync('JsSendFileCntAsync', event.target.files.length);
            //
            for (const file of event.target.files) {
                Md5Helper.getFileMd5(file).done(function (result, file) {
                    //TODO: sent file hash calc finish signal to Blazor                    
                    Md5Helper.dotNetHelper.invokeMethodAsync('JsSendFileHashAsync', file.name, file.webkitRelativePath, file.size, result);
                });
            }
        });
    }

    static getFileMd5(file) {
        var dfd = jQuery.Deferred();
        /**
         * reference:
         * 	https://github.com/satazor/SparkMD5
         */
        var blobSlice = File.prototype.slice || File.prototype.mozSlice || File.prototype.webkitSlice,
            chunkSize = 2097152,                             // Read in chunks of 2MB
            chunks = Math.ceil(file.size / chunkSize),
            currentChunk = 0,
            spark = new SparkMD5.ArrayBuffer(),
            fileReader = new FileReader();

        fileReader.onload = function (e) {
            dfd.notify('read chunk # ' + (currentChunk + 1) + ' of ' + chunks);
            if (Md5Helper.isShowConsole) {
                console.log('read chunk #', currentChunk + 1, 'of', chunks + ' [' + file.webkitRelativePath + ']');
            }
            spark.append(e.target.result);                   // Append array buffer
            currentChunk++;

            if (currentChunk < chunks) {
                loadNext();
            } else {
                dfd.resolve(spark.end(), file);
            }
        };

        fileReader.onerror = function () {
            dfd.reject('oops, something went wrong.');
        };

        var loadNext = function () {
            var start = currentChunk * chunkSize,
                end = ((start + chunkSize) >= file.size) ? file.size : start + chunkSize;

            fileReader.readAsArrayBuffer(blobSlice.call(file, start, end));
        };

        loadNext();

        return dfd.promise();
    }
}
window.Md5Helper = Md5Helper;