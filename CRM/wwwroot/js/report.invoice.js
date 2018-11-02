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
    },

    instances: {
    },

    callbacks: {
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
                    dxGrid.toolbar.widgets.optionDateRange(e_grid, "SubmittedDateTime", "Submitted On...")
                );
                dxGrid.toolbar.methods.addToolbarItem(e_grid,
                    dxGrid.toolbar.widgets.optionDateRange(e_grid, "AssignedDateTime", "Assigned On...")
                );
                dxGrid.toolbar.methods.addToolbarItem(e_grid,
                    dxGrid.toolbar.widgets.optionDateRange(e_grid, "AcceptedDateTime", "Accepted On...")
                );
            },
        },
    },
}