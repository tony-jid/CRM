﻿var lead = {
    ids: {
        gridLeads: "#gridLeads",
    },

    instances: {
        gridLeads: function () {
            return $(lead.ids.gridLeads).dxDataGrid("instance");
        },
    },

    handlers: {
        onActionChanged: function (e) {
            // have to check whether [selectedItem] is null, because [e.component.reset()] raises event [onSelectionChanged]
            if (e.selectedItem !== null) {
                var actionInstance = e.selectedItem;

                if (actionInstance.ActionTarget === action.targets.message) {
                    lead.methods.loadDataSourceLeads().done(function (data) {
                        var email = lead.methods.getMessageRecipient(actionInstance.CustomerId, data);
                        //console.log(email);

                        // creating dynamic data to support "Message Compose"
                        var messageData = {
                            recipients: []
                        };
                        messageData.recipients.push(email);

                        action.perform(action.sources.lead, actionInstance, messageData);
                    });
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
                            dxGrid.toolbar.methods.newOptionItem("CustomerName", "Grouping by Customer"),
                            dxGrid.toolbar.methods.newOptionItem("Type", "Grouping by Type"),
                            dxGrid.toolbar.methods.newOptionItem("Status", "Grouping by Status"),
                        ]
                    )
                );
                dxGrid.toolbar.methods.addToolbarItem(e_grid,
                    dxGrid.toolbar.widgets.optionDateRange(e_grid, "Created...", function (e_selectbox) {
                        //console.log(e_selectbox.value)

                        if (e_selectbox.value != null) {
                            e_grid.component.filter(["CreatedOn", ">=", moment(e_selectbox.value.start)], ["CreatedOn", "<=", moment(e_selectbox.value.end)]);
                        }
                        else {
                            e_grid.component.clearFilter();
                        }
                    })
                );
            }
        }
    },
}