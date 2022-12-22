function saveAsFile(fileName, byteBase64) {    
    var link = document.createElement('a');
    var mediaType = 'data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,';
    link.download = fileName;
    link.href = mediaType + byteBase64;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}
 