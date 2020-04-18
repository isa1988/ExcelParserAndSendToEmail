$(document).ready(function () {
    var dropZone = $('#dropZone')
    var file = document.querySelector('#inportFile')
    // Проверка поддержки браузером
    if (typeof(window.FileReader) == 'undefined') {
        dropZone.text('Не поддерживается браузером!');
        dropZone.addClass('error');
    }
    // Добавляем класс hover при наведении
    dropZone[0].ondragover = function() {
        dropZone.addClass('hover');
        return false;
    };
    // Убираем класс hover
    dropZone[0].ondragleave = function() {
        dropZone.removeClass('hover');
        return false;
    };
    // Обрабатываем событие Drop
    dropZone[0].ondrop = function(event) {
        event.preventDefault();
        dropZone.removeClass('hover');
        dropZone.addClass('drop');
        $('#imgAddBase').remove();
        document.getElementById("file").files = event.dataTransfer.files;
        for (var i = 0; i < event.dataTransfer.files.length; i++) {
            var file = event.dataTransfer.files.item(i);
            var reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = function(e) {
                // browser completed reading file - display it
                // console.log($('#imagePreview'),e.target.result);
                var $img = $('<img id=imgAddBase>');
                $img.attr('src', e.target.result);
            };
        }

    };


    var fileTypes = [
        'Excel/xls',
        'Excel/xlsx'
    ]

    function validFileType(file) {
        for (var i = 0; i < fileTypes.length; i++) {
            if (file.type === fileTypes[i]) {
                return true;
            }
        }

        return false;
    }

    function updateImageDisplay() {
        var curFiles = file.files;
        dropZone.removeClass('drop');
        dropZone.addClass('hover');
        $('#imgAddBase').remove();
        if (curFiles.length !== 0) {
            for (var i = 0; i < curFiles.length; i++) {
                if (validFileType(curFiles[i])) {
                    dropZone.removeClass('hover');
                    dropZone.addClass('drop');
                    var $img = $('<img id=imgAddBase>');
                    $img.attr('src', window.URL.createObjectURL(curFiles[i]));
                }
            }
        }

    }

    file.addEventListener('change', updateImageDisplay);
});
