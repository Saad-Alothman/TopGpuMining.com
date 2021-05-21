class CropperManager {

    constructor(cropperDivItem) {

        this.cropperDiv = cropperDivItem;

        let fileInputSelector = $('input[type="file"]', cropperDivItem);

        let aspectRatioData = $(fileInputSelector).data("aspect-ratio");

        this.modalCropImg = '#cropper-modal-img';
        this.modalId = "#multi-cropper-modal";
        this.fileInputId = fileInputSelector;
        this.cropperImg = "#multi-cropper-image";
        this.aspectRatio = this.getAspectRatio(aspectRatioData);
        this.buttonText = $(fileInputSelector).data('btn-text');
        this.maxSize = $(fileInputSelector).data('max-size');
        this.alert = $(fileInputSelector).data('alert');
        this.buttonTemplat = $(fileInputSelector).data('btn-template');
        this.previewImg = $(fileInputSelector).data('preview-img');
        this.canvasData = '';
        this.cropBoxData = '';
        this.originalImageSource = $(this.previewImg).attr('src');
        this.cropper = '';
        this.form = $(fileInputSelector).data('form');
        this.copyElement = $(fileInputSelector).data('copy');
        this.selector = fileInputSelector;

        this.onCropMove = function (e) {

        };

        let instance = this;

        $(fileInputSelector).on('change', function (e) {

            instance.onSelectImage(e);
        });


        this.initCropper();
    }

    initCropper() {

        var instance = this;
        let cropperImageEle = document.querySelector(this.cropperImg);

        this.cropper = new Cropper(cropperImageEle, {
            aspectRatio: this.aspectRatio,
            guides: false,
            background: false,
            center: true,
            viewMode: 1,
            responsive: true,
            scalable: false,
            movable: false,
            dragMode: "none",
            toggleDragModeOnDblclick: false,
            minCropBoxWidth: 256,
            crop: function (e) {
                instance.onCropMove(e);
            },
            cropend: function (event) {

            },
            ready: function () {

            }
        });

    }

    resetCropper() {

        this.cropper.destroy();
        this.cropper.init();
    }

    getCroppedImage() {

        return this.cropper.getCroppedCanvas().toDataURL('image/jpeg');
    }

    getAspectRatio(data) {

        var dataString = data.split('/');

        var left = parseFloat(dataString[0]);

        var right = parseFloat(dataString[1]);

        return left / right;
    }

    showErrorAlert(msg) {

        let alert = {
            alertType: 4,
            isAutoHide: false,
            message: msg,
            close: true
        };

        showAlert(alert, this.alert);
    }

    clearAlert() {
        $(this.alert).html('');
    }

    onSelectImage(e) {

        var file = e.target.files[0];

        if (!file)
            return;

        let reader = new FileReader();

        let instance = this;

        reader.onload = function (event) {

            var imgData = event.target.result;

            let saveButton = $('[data-save-button]', instance.modalId);

            $(saveButton).unbind();

            $(saveButton).on('click', function () {
                instance.onSave();
            })

            $(instance.cropperImg).attr("src", imgData);

            if (instance.modalId) {

                showModal(instance.modalId);

                $(instance.modalId).on("shown.bs.modal", function () {
                    instance.resetCropper();
                });
            }

            if (instance.copyElement) {

                var copy = instance.copyElement;

                var clone = $(instance.selector).clone();

                clone.addClass("hidden");

                $(copy).html('');

                $(copy).html(clone);
            }

        };

        reader.readAsDataURL(file);

    }

    onSave() {

        $(this.modalId).modal('hide');

        let img = this.getCroppedImage();

        var croppedData = this.cropper.getData();

        $('[data-cropper-x]', this.cropperDiv).val(croppedData.x);

        $('[data-cropper-y]', this.cropperDiv).val(croppedData.y);

        $('[data-cropper-width]', this.cropperDiv).val(croppedData.width);

        $('[data-cropper-height]', this.cropperDiv).val(croppedData.height);

        $('[data-cropper-rotate]', this.cropperDiv).val(croppedData.rotate);

        $(this.previewImg).attr("src", img);
    }
}