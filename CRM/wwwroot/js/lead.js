var lead = {
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
                var dataAction = e.selectedItem;

                if (dataAction.ActionTarget === action.targets.message) {
                    lead.methods.loadDataSourceLeads().done(function (data) {
                        var email = lead.methods.getMessageRecipient(dataAction.CustomerId, data);
                        //console.log(email);

                        // creating dynamic props to support "Message Compose"
                        dataAction.Message = { Recipients: [] };
                        dataAction.Message.Recipients.push(email);

                        action.perform(action.sources.lead, dataAction);
                    });
                } else {
                    action.perform(action.sources.lead, dataAction);
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
}