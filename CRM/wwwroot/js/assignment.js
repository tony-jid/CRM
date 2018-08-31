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
                        , assignment.events.onAssignmentSuccess);
                }
            });
        },
    },

    instances: {
        gridSearchBranches: function () {
            return $(assignment.ids.gridSearchBranches).dxDataGrid('instance');
        },
        gridSelectedBranches: function () {
            return $(assignment.ids.gridSelectedBranches).dxDataGrid('instance');
        },
        gridLeadAssignments: function () {
            return $(assignment.ids.gridLeadAssignments).dxDataGrid('instance');
        }
    },

    events: {
        onAssignmentSuccess: function (response) {
            assignment.clearSelectedBranches();
            assignment.refreshGridSelectedBranches();

            assignment.refreshGridSearchBranches();
            assignment.refreshGridLeadAssignments();

            $(window).scrollTop(0);
        },
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
        var viewModel = new LeadAssignmentSelectedPartnerViewModel($(assignment.ids.leadId).val());

        for (var i = 0; i < assignment.selectedBranches.length; i++) {
            viewModel.addPartner(assignment.selectedBranches[i].Id);
        }

        return viewModel;
    },

    refreshGridSearchBranches: function () {
        assignment.instances.gridSearchBranches().refresh();
    },
    refreshGridSelectedBranches: function () {
        assignment.instances.gridSelectedBranches().refresh();
        CustomGrid.setToolbarTotalItems("SelectedBranches", assignment.selectedBranches);
    },
    refreshGridLeadAssignments: function () {
        assignment.instances.gridLeadAssignments().refresh();
    },
};