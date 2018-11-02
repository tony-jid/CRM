var assignment = {
    vars: {
        selectedBranches: [],
    },

    ids: {
        leadId: "#__leadId",
        leadState: "#__leadState",

        btnAdd: "#btnAdd",
        btnRemove: '#btnRemove',
        btnAssign: '#btnAssign',
        gridSearchBranches: '#gridSearchBranches',
        gridSelectedBranches: '#gridSelectedBranches',
        gridLeadAssignments: '#gridLeadAssignments',
    },

    actionNames: {
        acceptLead: "Accept",
        rejectLead: "Reject",
        sendAssignmentMessage: "SendAssignmentMessage",
        commentLead: "CommentLead",
        invoiceByAssignments: "InvoiceByAssignments",
    },

    handlers: {
        clickBtnAdd: function () {
            $(assignment.ids.btnAdd).unbind();
            $(assignment.ids.btnAdd).click(function () {
                assignment.methods.addItemsToSelectedBranches();
                assignment.methods.refreshGridSelectedBranches();
            });
        },
        clickBtnRemove: function () {
            $(assignment.ids.btnRemove).unbind();
            $(assignment.ids.btnRemove).click(function () {
                assignment.methods.removeItemsFromSelectedBranches();
                assignment.methods.refreshGridSelectedBranches();
            });
        },
        clickBtnAssign: function () {
            $(assignment.ids.btnAssign).unbind();
            $(assignment.ids.btnAssign).click(function () {
                if (assignment.vars.selectedBranches.length) {
                    ajax.callers.crm(
                        ajax.controllers.assignment.name
                        , ajax.controllers.assignment.actions.assignPartners
                        , assignment.methods.getViewModel()
                        , assignment.callbacks.onAssignmentSuccess);
                }
            });
        },
        onAssignmentActionChanged: function (e) {
            // have to check whether [selectedItem] is null, because [e.component.reset()] raises event [onSelectionChanged]
            if (e.selectedItem !== null) {
                var actionInstance = e.selectedItem;

                if (actionInstance.ActionTarget === action.targets.ajax) {
                    var actionVM = assignment.methods.getAssignmentResponseVM(actionInstance.LeadId, actionInstance.LeadAssignmentId, actionInstance);

                    var callback = function () { };
                    if (actionInstance.ActionName === assignment.actionNames.acceptLead)
                        callback = assignment.callbacks.onAcceptLeadSuccess;
                    else if (actionInstance.ActionName === assignment.actionNames.rejectLead)
                        callback = assignment.callbacks.onRejectLeadSuccess;

                    action.perform(action.sources.assignment, actionInstance, actionVM, callback);

                }
                else if (actionInstance.ActionTarget === action.targets.message) {
                    var callback = function () { };
                    if (actionInstance.ActionName === assignment.actionNames.sendAssignmentMessage)
                        callback = assignment.callbacks.onSendAssignmentMessageSuccess;

                    // using dynamic data to support "Message Compose"
                    var messageData = {
                        recipients: []
                    };

                    // CustomerEmail will be null, if the grid is opened via Partner Portal
                    if (actionInstance.CustomerEmail != null) {
                        messageData.recipients.push(actionInstance.CustomerEmail);
                    }
                    else {
                        messageData.recipients = actionInstance.PartnerEmails;
                    }

                    action.perform(action.sources.assignment, actionInstance, messageData, callback);
                }
                else if (actionInstance.ActionTarget === action.targets.rating) {
                    var callback = function () { };
                    if (actionInstance.ActionName === assignment.actionNames.commentLead)
                        callback = assignment.callbacks.onCommentLeadSuccess;

                    action.perform(action.sources.assignment, actionInstance, actionInstance.Rating, callback);
                }
                else if (actionInstance.ActionTarget === action.targets.window) {
                    if (actionInstance.ActionName === assignment.actionNames.invoiceByAssignments) {
                        var leadAssignmentId = actionInstance.LeadAssignmentId;
                        var params = ajax.methods.getListParams([leadAssignmentId]);

                        window.open("/{0}/{1}?{2}".format(actionInstance.ControllerName, actionInstance.ActionName, params), "_blank");
                    }
                }

                e.component.reset();
            }
        },
    },

    instances: {
        gridSearchBranches: function () {
            if ($(assignment.ids.gridSearchBranches).length)
                return $(assignment.ids.gridSearchBranches).dxDataGrid('instance');
            else {
                console.log(assignment.ids.gridSearchBranches.concat(" is not found!"))
                return undefined;
            }

        },
        gridSelectedBranches: function () {
            if ($(assignment.ids.gridSelectedBranches).length)
                return $(assignment.ids.gridSelectedBranches).dxDataGrid('instance');
            else {
                console.log(assignment.ids.gridSelectedBranches.concat(" is not found!"));
                return undefined;
            }
        },
        gridLeadAssignments: function (suffixId) {
            if (typeof (suffixId) === "undefined") {
                if ($(assignment.ids.leadId).length)
                    suffixId = $(assignment.ids.leadId).val();
                else
                    suffixId = "";
            }

            var instance = $(assignment.ids.gridLeadAssignments.concat(suffixId)).dxDataGrid('instance');

            if (typeof (instance) === "undefined") {
                instance = $(assignment.ids.gridLeadAssignments).dxDataGrid('instance');
            } else {
                console.log(assignment.ids.gridLeadAssignments.concat(suffixId).concat(" is not found! Returning default instance."));
            }

            return instance;
        }
    },

    callbacks: {
        onAssignmentSuccess: function (response) {
            assignment.methods.clearSelectedBranches();
            assignment.methods.refreshGridSelectedBranches();

            assignment.methods.refreshGridSearchBranches();
            assignment.methods.refreshGridLeadAssignments();

            $(window).scrollTop(0);
        },
        onAcceptLeadSuccess: function (response) {
            // response.Value = LeadId
            assignment.methods.refreshGridLeadAssignments(response.Value);
            notification.alert.showSuccess('Successfully accepted the lead.');
        },
        onRejectLeadSuccess: function (response) {
            // response.Value = LeadId
            assignment.methods.refreshGridLeadAssignments(response.Value);
            notification.alert.showWarning('Successfully rejected the lead.');
        },
        onSendAssignmentMessageSuccess: function (response) {
            // response = MessageViewModel
            assignment.methods.refreshGridLeadAssignments(response.LeadId);
            notification.alert.showSuccess('Successfully sent the message.');
        },
        onCommentLeadSuccess: function (response) {
            // response.Value = LeadId
            assignment.methods.refreshGridLeadAssignments(response.Value);
            notification.alert.showSuccess('Successfully commented the lead.');
        },
    },

    methods: {
        getAssignmentResponseVM: function (leadId, leadAssignmentId, action) {
            return new LeadAssignmentResponseVM(leadId, leadAssignmentId, action);
        },
        getViewModel: function () {
            var viewModel = new LeadAssignmentSelectedPartnerVM($(assignment.ids.leadId).val());

            for (var i = 0; i < assignment.vars.selectedBranches.length; i++) {
                viewModel.addPartner(assignment.vars.selectedBranches[i].Id);
            }

            return viewModel;
        },

        getLeadState: function () {
            if ($(assignment.ids.leadState).length)
                return $(assignment.ids.leadState).val();
            else
                return "";
        },
        getAssignmentDataItems: function (suffixId) {
            return dxGrid.options.dataItems(assignment.instances.gridLeadAssignments(suffixId));
        },

        getRowsFromSearchGrid: function () {
            return assignment.instances.gridSearchBranches().getSelectedRowsData();
        },
        getRowsFromSelectedGrid: function () {
            return assignment.instances.gridSelectedBranches().getSelectedRowsData();
        },

        clearSelectedBranches: function () {
            assignment.vars.selectedBranches.splice(0, assignment.vars.selectedBranches.length);
        },
        addItemsToSelectedBranches: function () {
            var rows = assignment.methods.getRowsFromSearchGrid();
            //assignment.alertRowIds(rows);

            if (rows.length) {
                for (var i = 0; i < rows.length; i++) {
                    var isIdDuplicate = false;

                    for (var j = 0; j < assignment.vars.selectedBranches.length; j++) {
                        if (rows[i].Id === assignment.vars.selectedBranches[j].Id) {
                            isIdDuplicate = true;
                            break;
                        }
                    }

                    if (!isIdDuplicate)
                        assignment.vars.selectedBranches.push(rows[i]);
                }

                //assignment.alertRowIds(assignment.vars.selectedBranches);
            }
        },

        refreshGridSearchBranches: function () {
            var instance = assignment.instances.gridSearchBranches();
            if (typeof (instance) !== "undefined") {
                instance.refresh();
            }
        },
        refreshGridSelectedBranches: function () {
            var instance = assignment.instances.gridSelectedBranches();

            if (typeof (instance) !== "undefined") {
                instance.refresh();
            }
        },
        refreshGridLeadAssignments: function (suffixId) {
            assignment.instances.gridLeadAssignments(suffixId).refresh();
        },

        removeItemsFromSelectedBranches: function () {
            var rows = assignment.methods.getRowsFromSelectedGrid();

            if (rows.length) {
                for (var i = 0; i < rows.length; i++) {
                    var beingRemovedIndex;

                    for (var j = 0; j < assignment.vars.selectedBranches.length; j++) {
                        if (rows[i].Id === assignment.vars.selectedBranches[j].Id) {
                            beingRemovedIndex = j;
                            break;
                        }
                    }

                    assignment.vars.selectedBranches.splice(beingRemovedIndex, 1);
                }
            }
        },
    },

    // Overiding functions === Start ===
    //
    dxGrid: {
        handlers: {
            gridAssignmentsByPartnerOnToolbarPreparing: function (e_grid) {
                dxGrid.handlers.onToolbarPreparing(e_grid);
                dxGrid.toolbar.methods.addToolbarItem(e_grid,
                    dxGrid.toolbar.widgets.optionGrouping(e_grid,
                        [
                            dxGrid.toolbar.methods.newOptionItem("Type", "Grouping by Type"),
                            dxGrid.toolbar.methods.newOptionItem("Customer", "Grouping by Customer"),
                            dxGrid.toolbar.methods.newOptionItem("Status", "Grouping by Status"),
                        ]
                    )
                );
                dxGrid.toolbar.methods.addToolbarItem(e_grid,
                    dxGrid.toolbar.widgets.optionDateRange(e_grid, "AssignedOn", "Assigned...")
                );
            },
            gridAssignmentsByLeadOnToolbarPreparing: function (e_grid) {
                dxGrid.handlers.onToolbarPreparing(e_grid);
                dxGrid.toolbar.methods.addToolbarItem(e_grid,
                    dxGrid.toolbar.widgets.optionDateRange(e_grid, "AssignedOn", "Assigned...")
                );
            },
            gridSearchBranchesOnToolbarPreparing: function (e_grid) {
                dxGrid.handlers.onToolbarPreparing(e_grid);
                dxGrid.toolbar.methods.addToolbarItem(e_grid,
                    dxGrid.toolbar.widgets.optionFilter(e_grid, stateTextValues, "Address.State", "All State", assignment.methods.getLeadState())
                );
            }
        }
    },

    alertRowIds: function (rows) {
        var msg = [];
        for (var i = 0; i < rows.length; i++) {
            msg.push(rows[i].Id);
        }

        alert(msg.join('\n'));
    },
}