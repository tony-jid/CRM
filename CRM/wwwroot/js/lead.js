var lead = {
    ids: {
        customerId: "__customerId",
        gridLeads: "#gridLeads",
        _lookupCustomer: "lookupCustomer",
        editorDetails: "#editorDetails",
        _editorDetails: "editorDetails",
        _dateBoxCreatedOn: "dateBoxCreatedOn",
    },

    actionNames: {
        sendLeadMessage: "SendLeadMessage",
        sendLeadRequestInfo: "SendLeadRequestInfo",
        invoiceByLeads: "InvoiceByLeads",
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
        dateBoxCreatedOn_NotWorkable: function (cellElement, cellInfo) {
            if (site.methods.isDefined(cellInfo.item)) {
                if (cellInfo.item.dataField === "CreatedOn") {
                    var _cellId = lead.ids._dateBoxCreatedOn;

                    if (site.methods.isDefined(cellInfo.id))
                        _cellId = lead.ids._dateBoxCreatedOn.concat(cellInfo.id);

                    $(cellElement).append("<div id='" + _cellId + "' />");

                    var defaultDate;
                    if (site.methods.isDefined(cellInfo.value))
                        defaultDate = cellInfo.value;
                    else
                        defaultDate = moment();

                    $("#" + _cellId).dxDateBox({
                        name: _cellId,
                        //value: defaultDate,
                        //disabled: true,
                        onValueChanged: function (e) {
                            if (site.methods.isFunction(cellInfo.setValue)) // cellInfo.setValue is not function in "CellTemplate"
                                cellInfo.setValue(e.value);
                        }
                    }).dxValidator({ validationRules: [{ type: "required", message: "WTF" }], });

                    setTimeout(function () { $("#" + _cellId).dxDateBox("instance").option("value", defaultDate); }, 1000);

                    //if (site.methods.isFunction(cellInfo.setValue)) // cellInfo.setValue is not function in "CellTemplate"
                        //cellInfo.setValue(defaultDate);
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

    callbacks: {
        onSendLeadMessageSuccess: function (response) {
            // response = MessageViewModel
            lead.instances.gridLeads(response.CustomerId).refresh();
            notification.alert.showSuccess('Successfully sent the message.');
        },
        onSendLeadRequestInfoSuccess: function (response) {
            // response = MessageViewModel
            lead.instances.gridLeads(response.CustomerId).refresh();
            notification.alert.showSuccess('Successfully sent the request information message.');
        }
    },

    handlers: {
        onActionChanged: function (e) {
            // have to check whether [selectedItem] is null, because [e.component.reset()] raises event [onSelectionChanged]
            if (e.selectedItem !== null) {
                var actionInstance = e.selectedItem;

                if (actionInstance.ActionTarget === action.targets.message) {
                    //var dataItems = lead.methods.getLeadDataItems(actionInstance.CustomerId);
                    //var email = lead.methods.getMessageRecipient(dataItems, actionInstance.CustomerId);
                    var email = actionInstance.CustomerEmail;

                    var callback = function () { };

                    if (actionInstance.ActionName === lead.actionNames.sendLeadMessage)
                        callback = lead.callbacks.onSendLeadMessageSuccess;
                    else if (actionInstance.ActionName === lead.actionNames.sendLeadRequestInfo)
                        callback = lead.callbacks.onSendLeadRequestInfoSuccess;

                    // using dynamic data to support "Message Compose"
                    var messageData = {
                        recipients: []
                    };
                    messageData.recipients.push(email);
                    
                    action.perform(action.sources.lead, actionInstance, messageData, callback);
                } else {
                    action.perform(action.sources.lead, actionInstance);
                }

                e.component.reset();
            }
        },
        onGroupActionChanged: function (e_grid, e_selectBox) {
            if (e_selectBox.selectedItem != null) {
                var selectedAction = e_selectBox.selectedItem;
                var selectedRows = e_grid.component.getSelectedRowsData();
                //console.log(selectedAction);
                //console.log(selectedRows);

                if (selectedRows.length) {
                    if (selectedAction.ActionTarget === action.targets.message) {
                        var emails = [];
                        var callback = function () { };

                        if (selectedAction.ActionName === lead.actionNames.sendLeadMessage) {
                            emails = lead.methods.getMessageRecipients(selectedRows, selectedAction.Id);
                            callback = lead.callbacks.onSendLeadMessageSuccess;

                            lead.methods.showMessageCompose(emails, selectedAction, callback);
                        }
                        else if (selectedAction.ActionName === lead.actionNames.sendLeadRequestInfo) {
                            emails = lead.methods.getMessageRecipients(selectedRows, selectedAction.Id);
                            callback = lead.callbacks.onSendLeadRequestInfoSuccess;

                            lead.methods.showMessageCompose(emails, selectedAction, callback);
                        }
                        else if (selectedAction.ActionName === assignment.actionNames.sendAssignmentMessage) {
                            // Sending message to partners
                            callback = assignment.callbacks.onSendAssignmentMessageSuccess;

                            ajax.callers.crm(
                                ajax.controllers.assignment.name,
                                ajax.controllers.assignment.actions.getByLeads,
                                ajax.controllers.assignment.actions.getByLeads.params(lead.methods.getLeadIds(selectedRows)),
                                function (response) {
                                    // response = List of lead-assignments
                                    //console.log(response);
                                    emails = lead.methods.getMessageRecipients(response, selectedAction.Id);

                                    lead.methods.showMessageCompose(emails, selectedAction, callback);
                                }
                            );
                        }
                    }
                    else if (selectedAction.ActionTarget === action.targets.window) {
                        if (selectedAction.ActionName === lead.actionNames.invoiceByLeads) {
                            var leadIds = lead.methods.getLeadIds(selectedRows);
                            var params = ajax.methods.getListParams(leadIds);
                            window.open("/{0}/{1}?{2}".format(selectedAction.ControllerName, selectedAction.ActionName, params), "_blank");
                        }
                    }
                }
                else {
                    notification.alert.showWarning("No items selected!");
                }

                e_selectBox.component.reset();
            }
        },
    },

    methods: {
        loadDataSourceLeads: function () {
            return lead.instances.gridLeads().option("dataSource").store.load();
        },
        getLeadDataItems: function (suffixId) {
            return dxGrid.options.dataItems(lead.instances.gridLeads(suffixId));
        },
        getMessageRecipients: function (data, actionId) {
            var emails = [];
            for (var i = 0; i < data.length; i++) {
                for (var j = 0; j < data[i].Actions.length; j++) {
                    // Adding email to the list only when the current status of an item has the selected action
                    if (data[i].Actions[j].Id == actionId) {
                        if (site.methods.isDefined(data[i].CustomerEmail)) {
                            emails.push(data[i].CustomerEmail);
                        }
                        else {
                            for (var k = 0; k < data[i].Actions[j].PartnerEmails.length; k++) {
                                emails.push(data[i].Actions[j].PartnerEmails[k]);
                            }
                        }

                        break;
                    }
                }
            }

            return emails;
        },
        getMessageRecipient: function (data, customerId) {
            for (var i = 0; i < data.length; i++) {
                if (data[i].CustomerId === customerId)
                    return data[i].CustomerEmail;
            }
        },
        getLeadIds: function (items) {
            var ids = [];
            for (var i = 0; i < items.length; i++) {
                ids.push(items[i].Id);
            }

            return ids;
        },
        showMessageCompose: function (emails, selectedAction, callback) {
            if (emails.length) {
                // using dynamic data to support "Message Compose"
                var messageData = {
                    recipients: emails
                };

                action.perform(action.sources.lead, selectedAction, messageData, callback);
            }
            else {
                notification.alert.showWarning("No recipients found!");
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
                dxGrid.toolbar.methods.addToolbarItem(e_grid,
                    dxGrid.toolbar.widgets.optionLeadGroupActions(e_grid, lead.handlers.onGroupActionChanged)
                );
            }
        }
    },
}