var leadtype = {
    vars: {
    },

    ids: {
        _uploaderImage: "uploaderImage",
    },

    templates: {
        uploaderImage: function (cellElement, cellInfo, dxCellType) {
            var _cellId;

            if (dxCellType === dxGrid.cellTypes.cellItem) {
                _cellId = leadtype.ids._uploaderImage.concat(cellInfo.key);
            } else {
                _cellId = leadtype.ids._uploaderImage.concat(cellInfo.id);
            }

            $(cellElement).append("<div id='" + _cellId + "' class='file-uploader' />");

            $("#" + _cellId).dxFileUploader({
                name: "image",
                selectButtonText: "Select lead type's image",
                labelText: "",
                accept: "image/*",
                uploadMode: "instantly",
                uploadUrl: site.apis.leadtypes.uploadImage(),
                showFileList: true,
                onValueChanged: function (e) {
                    if (typeof (cellInfo.setValue) === "function") {    // cellInfo.setValue is not function in "CellTemplate"
                        var fileName = _cellId.concat("_" + e.value[0].name);

                        cellInfo.setValue(fileName);
                        e.component.option('uploadUrl', site.apis.leadtypes.uploadImage() + "?fileName=" + fileName); // reset url along with unique fileName
                    }
                },
            });
        },
    },

    instances: {
    },

    handlers: {

    },

    callbacks: {

    },

    methods: {

    },
}