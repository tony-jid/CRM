var reportInvoice = {
    vars: {
        partnerNames: [],

    },

    ids: {
        gridReportInvoice: "#gridReportInvoice"
    },

    actionNames: {

    },

    handlers: {
        onInvoiceExporting: function (e) {
            // _selectionOnly is undefined, if "Export All" is clicked
            var isExportingSelectedRows = dxGrid.options.exportOptions(e)._selectionOnly;

            if (site.methods.isDefined(isExportingSelectedRows)) {
                if (isExportingSelectedRows) {
                    var selectedRows = e.component.getSelectedRowsData();
                    if (selectedRows.length < 1) {
                        e.cancel = true;
                        notification.alert.showWarning("No items selected!");
                    }
                } else {
                    console.log("Something wrong! isExportingSelectedRows = " + isExportingSelectedRows);
                }
            }
        },
        onInvoiceExported: function (e) {
            var exportedItems;
            var isExportingSelectedRows = dxGrid.options.exportOptions(e)._selectionOnly;

            if (isExportingSelectedRows) {
                exportedItems = e.component.getSelectedRowsData();
            } else {
                exportedItems = dxGrid.options.dataItems(e.component);
            }

            // Updating lead assignments' status to "Invoiced" or "Re-invoiced" after exporting
            var leadAssignmentIds = site.methods.getDistinctArrayFromObject(exportedItems, "LeadAssignmentId");
            //console.log(leadAssignmentIds);

            ajax.callers.crm(
                ajax.controllers.reports.name,
                ajax.controllers.reports.actions.updateInvoicedLeadAssignmentStatus,
                exportedItems,
                reportInvoice.callbacks.onUpdateStatusSuccess
            );
        },
    },

    instances: {
        gridReportInvoice: function () {
            return $(reportInvoice.ids.gridReportInvoice).dxDataGrid("instance");
        },
    },

    callbacks: {
        onUpdateStatusSuccess: function (response) {
            //console.log(response);
            reportInvoice.instances.gridReportInvoice().refresh();
            notification.alert.showSuccess("Updated status invoice successfully.");
        },
    },

    methods: {

    },

    // Overiding functions === Start ===
    //
    dxGrid: {
        handlers: {
            onContentReady: function (e) {
                dxGrid.handlers.onContentReady(e);
                //var dataItems = dxGrid.options.dataItems(e.component);
                //console.log(dataItems);
                //var distinctArray = site.methods.getDistinctArrayFromObject(dataItems, "PartnerName");
                //console.log(distinctArray);
            },
            onToolbarPreparing: function (e_grid) {
                dxGrid.handlers.onToolbarPreparing(e_grid);
                dxGrid.toolbar.methods.addToolbarItem(e_grid,
                    dxGrid.toolbar.widgets.optionDateRange(e_grid, "SubmittedDate", "Submitted...")
                );
                dxGrid.toolbar.methods.addToolbarItem(e_grid,
                    dxGrid.toolbar.widgets.optionDateRange(e_grid, "AssignedDate", "Assigned...")
                );
                dxGrid.toolbar.methods.addToolbarItem(e_grid,
                    dxGrid.toolbar.widgets.optionDateRange(e_grid, "AcceptedDate", "Accepted...")
                );
            },
        },
    },
}