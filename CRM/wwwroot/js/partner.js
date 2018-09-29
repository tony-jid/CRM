var partner = {
    vars: {
        selectedServices: []
    },

    ids: {
        tagBoxServices: "#tagBoxServices",
        _tagBoxServices: "tagBoxServices",
    },

    templates: {
        tagBoxServices: function (cellElement, cellInfo, dxCellType) {
            var _cellId, _readOnly;

            if (dxCellType === dxGrid.cellTypes.cellItem) {
                _cellId = cellInfo.key;
                _readOnly = true;
            } else {
                _cellId = cellInfo.id;
                _readOnly = false;
            }

            $(cellElement).append("<div id='" + _cellId + "' />");

            console.log(_cellId);

            $("#" + _cellId).dxTagBox({
                dataSource: DevExpress.data.AspNet.createStore({
                    loadUrl: site.apis.leadtypes.get(),
                    onBeforeSend: function (method, ajaxOptions) {
                        ajaxOptions.xhrFields = { withCredentials: true };
                    },
                    key: "Id"
                }),
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
                        if (typeof (cellInfo.setValue) === "function")
                            cellInfo.setValue(e.value);
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

    },
}