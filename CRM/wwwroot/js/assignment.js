var assignment = {
    selectedBranches: [],

    ids: {
        leadId: "#__leadId",
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
    },

    handlers: {
        clickBtnAdd: function () {
            $(assignment.ids.btnAdd).unbind();
            $(assignment.ids.btnAdd).click(function () {
                assignment.addItemsToSelectedBranches();
                assignment.refreshGridSelectedBranches();
            });
        },
        clickBtnRemove: function () {
            $(assignment.ids.btnRemove).unbind();
            $(assignment.ids.btnRemove).click(function () {
                assignment.removeItemsFromSelectedBranches();
                assignment.refreshGridSelectedBranches();
            });
        },
        clickBtnAssign: function () {
            $(assignment.ids.btnAssign).unbind();
            $(assignment.ids.btnAssign).click(function () {
                if (assignment.selectedBranches.length) {
                    ajax.callers.crm(
                        ajax.controllers.assignment.name
                        , ajax.controllers.assignment.actions.assignPartners
                        , assignment.getViewModel()
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

                } else if (actionInstance.ActionTarget === action.targets.message) {

                }
            }
        },
    },

    instances: {
        gridSearchBranches: function () {
            return $(assignment.ids.gridSearchBranches).dxDataGrid('instance');
        },
        gridSelectedBranches: function () {
            return $(assignment.ids.gridSelectedBranches).dxDataGrid('instance');
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
            }

            return instance;
        }
    },

    callbacks: {
        onAssignmentSuccess: function (response) {
            assignment.clearSelectedBranches();
            assignment.refreshGridSelectedBranches();

            assignment.refreshGridSearchBranches();
            assignment.refreshGridLeadAssignments();

            $(window).scrollTop(0);
        },
        onAcceptLeadSuccess: function (response) {
            // response.Value = LeadId
            assignment.refreshGridLeadAssignments(response.Value);
            notification.alert.showSuccess('Successfully accepted the lead');
        },
        onRejectLeadSuccess: function (response) {
            // response.Value = LeadId
            assignment.refreshGridLeadAssignments(response.Value);
            notification.alert.showWarning('Successfully reject the lead');
        }
    },

    methods: {
        getAssignmentResponseVM: function (leadId, leadAssignmentId, action) {
            return new LeadAssignmentResponseVM(leadId, leadAssignmentId, action);
        }
    },

    getRowsFromSearchGrid: function () {
        return assignment.instances.gridSearchBranches().getSelectedRowsData();
    },
    getRowsFromSelectedGrid: function () {
        return assignment.instances.gridSelectedBranches().getSelectedRowsData();
    },
    alertRowIds: function (rows) {
        var msg = [];
        for (var i = 0; i < rows.length; i++) {
            msg.push(rows[i].Id);
        }

        alert(msg.join('\n'));
    },

    addItemsToSelectedBranches: function () {
        var rows = assignment.getRowsFromSearchGrid();
        //assignment.alertRowIds(rows);

        if (rows.length) {
            for (var i = 0; i < rows.length; i++) {
                var isIdDuplicate = false;

                for (var j = 0; j < assignment.selectedBranches.length; j++) {
                    if (rows[i].Id === assignment.selectedBranches[j].Id) {
                        isIdDuplicate = true;
                        break;
                    }
                }

                if (!isIdDuplicate)
                    assignment.selectedBranches.push(rows[i]);
            }

            //assignment.alertRowIds(assignment.selectedBranches);
        }
    },    
    removeItemsFromSelectedBranches: function () {
        var rows = assignment.getRowsFromSelectedGrid();

        if (rows.length) {
            for (var i = 0; i < rows.length; i++) {
                var beingRemovedIndex;

                for (var j = 0; j < assignment.selectedBranches.length; j++) {
                    if (rows[i].Id === assignment.selectedBranches[j].Id) {
                        beingRemovedIndex = j;
                        break;
                    }
                }

                assignment.selectedBranches.splice(beingRemovedIndex, 1);
            }
        }
    },
    clearSelectedBranches: function () {
        assignment.selectedBranches.splice(0, assignment.selectedBranches.length);
    },

    getViewModel: function () {
        var viewModel = new LeadAssignmentSelectedPartnerVM($(assignment.ids.leadId).val());

        for (var i = 0; i < assignment.selectedBranches.length; i++) {
            viewModel.addPartner(assignment.selectedBranches[i].Id);
        }

        return viewModel;
    },

    refreshGridSearchBranches: function () {
        var instance = assignment.instances.gridSearchBranches();
        if (typeof (instance) !== "undefined")
            instance.refresh();
        else
            console.log("gridSearchBranches is not in this page.");
    },
    refreshGridSelectedBranches: function () {
        var instance = assignment.instances.gridSelectedBranches();

        if (typeof (instance) !== "undefined") {
            instance.refresh();
            CustomGrid.setToolbarTotalItems("SelectedBranches", assignment.selectedBranches);
        }
        else
            console.log("gridSelectedBranches is not in this page.");
    },
    refreshGridLeadAssignments: function (suffixId) {
        assignment.instances.gridLeadAssignments(suffixId).refresh();
    },
};