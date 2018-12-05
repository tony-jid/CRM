$(document).ready(function () {
    partner.methods.setDsTagBoxLeadTypes();
});

var partner = {
    vars: {
        selectedServices: [],
        dsTagBoxLeadTypes: undefined
    },

    ids: {
        tagBoxServices: "#tagBoxServices",
        _tagBoxServices: "tagBoxServices",
        _uploaderLogo: "uploaderLogo",
    },

    templates: {
        tagBoxServices: function (cellElement, cellInfo, dxCellType) {
            var _cellId, _readOnly;

            if (dxCellType === dxGrid.cellTypes.cellItem) {
                _cellId = partner.ids._tagBoxServices.concat(cellInfo.key);
                _readOnly = true;
            } else {
                _cellId = partner.ids._tagBoxServices.concat(cellInfo.id);
                _readOnly = false;
            }

            $(cellElement).append("<div id='" + _cellId + "' />");

            $("#" + _cellId).dxTagBox({
                dataSource: partner.methods.getDsTagBoxLeadTypes(),
                //dataSource: DevExpress.data.AspNet.createStore({
                //    loadUrl: site.apis.leadtypes.get(),
                //    onBeforeSend: function (method, ajaxOptions) {
                //        ajaxOptions.xhrFields = { withCredentials: true };
                //    },
                //    key: "Id"
                //}),
                value: cellInfo.value,
                multiline: true,
                searchEnabled: true,
                hideSelectedItems: true,
                placeholder: "Services...",
                displayExpr: "Name",
                valueExpr: "Id",
                searchExpr: ["Name"],
                readOnly: _readOnly,
                onValueChanged: function (e) {
                    if (!_readOnly) {
                        if (typeof (cellInfo.setValue) === "function") // cellInfo.setValue is not function in "CellTemplate"
                            cellInfo.setValue(e.value);
                    }
                },
            });
        },
        uploaderLogo: function (cellElement, cellInfo, dxCellType) {
            var _cellId;

            if (dxCellType === dxGrid.cellTypes.cellItem) {
                _cellId = partner.ids._uploaderLogo.concat(cellInfo.key);
            } else {
                _cellId = partner.ids._uploaderLogo.concat(cellInfo.id);
            }

            $(cellElement).append("<div id='" + _cellId + "' class='file-uploader' />");

            $("#" + _cellId).dxFileUploader({
                name: "logo",
                selectButtonText: "Select partner's logo",
                labelText: "",
                accept: "image/*",
                uploadMode: "instantly",
                uploadUrl: site.apis.partners.uploadLogo(),
                showFileList: true,
                onValueChanged: function (e) {
                    if (typeof (cellInfo.setValue) === "function") {    // cellInfo.setValue is not function in "CellTemplate"
                        var fileName = _cellId.concat("_" + e.value[0].name);

                        cellInfo.setValue(fileName);
                        e.component.option('uploadUrl', site.apis.partners.uploadLogo() + "?fileName=" + fileName); // reset url along with unique fileName
                    }
                },
            });
        },
    },

    instances: {
        tagBoxServices: function () {
            var instance = $(partner.ids.tagBoxServices).dxTagBox("instance");
            if (typeof (instance) === "undefined")
                console.log(partner.ids.tagBoxServices.concat(" is not found!"));

            return instance;
        },
    },   

    handlers: {

    },

    callbacks: {
        
    },

    methods: {
        setDsTagBoxLeadTypes: function () {
            ajax.callers.crm(
                ajax.controllers.leadtypes.name,
                ajax.controllers.leadtypes.actions.Get,
                {},
                function (response) {
                    //console.log(response);
                    partner.vars.dsTagBoxLeadTypes = response.data;
                }
            );
        },
        getDsTagBoxLeadTypes: function () {
            if (site.methods.isDefined(partner.vars.dsTagBoxLeadTypes)) {
                return partner.vars.dsTagBoxLeadTypes;
            } else {
                return DevExpress.data.AspNet.createStore({
                    loadUrl: site.apis.leadtypes.get(),
                    onBeforeSend: function (method, ajaxOptions) {
                        ajaxOptions.xhrFields = { withCredentials: true };
                    },
                    key: "Id"
                });
            }
        },
    },
}