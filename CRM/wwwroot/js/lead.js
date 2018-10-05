var lead = {
    ids: {
        customerId: "__customerId",
        gridLeads: "#gridLeads",
        _lookupCustomer: "lookupCustomer",
        editorDetails: "#editorDetails",
        _editorDetails: "editorDetails",
    },

    templates: {
        lookupCustomer: function (cellElement, cellInfo) {
            var _cellId = lead.ids._lookupCustomer;

            if (site.methods.isDefined(cellInfo.id))
                _cellId = lead.ids._lookupCustomer.concat(cellInfo.id);
            
            $(cellElement).append("<div id='" + _cellId + "' />");

            $("#" + _cellId).dxLookup({
                name: _cellId,
                dataSource: DevExpress.data.AspNet.createStore({
                    loadUrl: site.apis.customers.getForLookup(),
                    onBeforeSend: function (method, ajaxOptions) {
                        ajaxOptions.xhrFields = { withCredentials: true };
                    },
                    key: "Id"
                }),
                closeOnOutsideClick: true,
                value: cellInfo.value,
                placeholder: "Customer...",
                displayExpr: "CustomerUnique",
                valueExpr: "Id",
                value: cellInfo.value,
                searchExpr: ["CustomerUnique"],                
                onValueChanged: function (e) {
                    if (site.methods.isFunction(cellInfo.setValue)) // cellInfo.setValue is not function in "CellTemplate"
                        cellInfo.setValue(e.value);
                },
            });

            //$("#" + _cellId).dxLookup("instance").option("value", null);
            //$("#" + _cellId).dxLookup("instance").option("opend", true);
            //$("#" + _cellId).dxLookup("instance").option("value", "5f6028b8-716a-40b7-d632-08d62ac3adc5");

            //setTimeout(function () {    
                
            //}, 2000);
        },
        editorDetails: function (cellElement, cellInfo) {
            if (site.methods.isDefined(cellInfo.item)) {
                if (cellInfo.item.dataField === "Details") {
                    var _cellId = lead.ids._editorDetails;

                    if (site.methods.isDefined(cellInfo.id))
                        _cellId = lead.ids._editorDetails.concat(cellInfo.id);

                    $(cellElement).append("<textarea id='" + _cellId + "' class='form-control email-body load-ckeditor' rows='3'></textarea>");
                    

                    for (var instance in CKEDITOR.instances) {
                        CKEDITOR.instances[instance].destroy();
                    }

                    if (site.methods.isDefined(CKEDITOR.instances[_cellId])) {
                        CKEDITOR.instances[_cellId].destroy();
                    }

                    // Set message on the editor after the initialization
                    $("#" + _cellId).ckeditor(function (e) {
                        if (site.methods.isDefined(cellInfo.value))
                            CKEDITOR.instances[_cellId].setData(cellInfo.value);

                        CKEDITOR.instances[_cellId].on("change", function (eventInfo) {
                            var text = eventInfo.editor.getData();
                            cellInfo.setValue(text);
                        });
                    });
                }
            }
        },
        editorDetails_SingleInstance: function (cellElement, cellInfo) {
            if (site.methods.isDefined(cellInfo.item)) {
                if (cellInfo.item.dataField === "Details") {
                    var _cellId = lead.ids._editorDetails;

                    //if (site.methods.isDefined(cellInfo.id))
                    //    _cellId = lead.ids._editorDetails.concat(cellInfo.id);

                    $(cellElement).append("<textarea id='" + _cellId + "' class='form-control email-body load-ckeditor' rows='3'></textarea>");

                    if (site.methods.isDefined(CKEDITOR.instances.editorDetails)) {
                        CKEDITOR.instances.editorDetails.destroy();
                    }

                    // Set message on the editor after the initialization
                    $(lead.ids.editorDetails).ckeditor(function (e) {
                        if (site.methods.isDefined(cellInfo.value))
                            CKEDITOR.instances.editorDetails.setData(cellInfo.value);

                        CKEDITOR.instances.editorDetails.on("change", function (eventInfo) {
                            var text = eventInfo.editor.getData();
                            cellInfo.setValue(text);
                        });
                    });
                }
            }
        },
    },

    instances: {
        gridLeads: function (suffixId) {
            if (typeof (suffixId) === "undefined") {
                if ($(lead.ids.customerId).length) {
                    suffixId = $(lead.ids.customerId).val();
                }
                else {
                    console.log("The hidden customerId is not found! Returning default instance!");

                    suffixId = "";
                }
            } else {
                if (!($(lead.ids.gridLeads.concat(suffixId)).length)) {
                    console.log(lead.ids.gridLeads.concat(suffixId).concat(" is not found! Returning default instance!"));

                    suffixId = "";
                }
            }

            return $(lead.ids.gridLeads.concat(suffixId)).dxDataGrid("instance");
        },
    },

    handlers: {
        onActionChanged: function (e) {
            // have to check whether [selectedItem] is null, because [e.component.reset()] raises event [onSelectionChanged]
            if (e.selectedItem !== null) {
                var actionInstance = e.selectedItem;

                if (actionInstance.ActionTarget === action.targets.message) {
                    var dataItems = lead.methods.getLeadDataItems(actionInstance.CustomerId);
                    var email = lead.methods.getMessageRecipient(actionInstance.CustomerId, dataItems);

                    // using dynamic data to support "Message Compose"
                    var messageData = {
                        recipients: []
                    };
                    messageData.recipients.push(email);

                    action.perform(action.sources.lead, actionInstance, messageData);

                    //lead.methods.loadDataSourceLeads().done(function (data) {
                    //    var email = lead.methods.getMessageRecipient(actionInstance.CustomerId, data);
                    //    //console.log(email);

                    //    // creating dynamic data to support "Message Compose"
                    //    var messageData = {
                    //        recipients: []
                    //    };
                    //    messageData.recipients.push(email);

                    //    action.perform(action.sources.lead, actionInstance, messageData);
                    //});
                } else {
                    action.perform(action.sources.lead, actionInstance);
                }

                e.component.reset();
            }
        }
    },

    methods: {
        loadDataSourceLeads: function () {
            return lead.instances.gridLeads().option("dataSource").store.load();
        },
        getLeadDataItems: function (suffixId) {
            return dxGrid.options.dataItems(lead.instances.gridLeads(suffixId));
        },
        getMessageRecipient: function (customerId, data) {
            for (var i = 0; i < data.length; i++) {
                if (data[i].CustomerId === customerId)
                    return data[i].CustomerEmail;
            }
        },
    },

    // Overiding functions === Start ===
    //
    dxGrid: {
        handlers: {
            onToolbarPreparing: function(e_grid) {
                dxGrid.handlers.onToolbarPreparing(e_grid);
                dxGrid.toolbar.methods.addToolbarItem(e_grid,
                    dxGrid.toolbar.widgets.optionGrouping(e_grid,
                        [
                            dxGrid.toolbar.methods.newOptionItem("Customer", "Grouping by Customer"),
                            dxGrid.toolbar.methods.newOptionItem("Type", "Grouping by Type"),
                            dxGrid.toolbar.methods.newOptionItem("Status", "Grouping by Status"),
                        ]
                    )
                );
                dxGrid.toolbar.methods.addToolbarItem(e_grid,
                    dxGrid.toolbar.widgets.optionDateRange(e_grid, "CreatedOn", "Created...")
                );
            }
        }
    },
}