var action = {
    sources: {
        agent: "Agent",
        partner: "Partner",
        customer: "Customer",
        lead: "Lead",
        assignment: "Assignment",
    },

    targets: {
        window: "Window",
        message: "Message",
        ajax: "Ajax"
    },

    requestTypes: {
        get: "Get",
        post: "Post",
        put: "Put",
        delete: "Delete"
    },

    perform: function (source, actionInstance, data, callback) {
        let requestType = actionInstance.RequestType;
        let target = actionInstance.ActionTarget;
        let ajaxAction = {
            type: actionInstance.RequestType,
            name: actionInstance.ActionName
        }

        if (typeof (callback) !== 'function')
            callback = function (response) { };

        switch (requestType) {
            case action.requestTypes.get:
                if (source === action.sources.lead) {
                    // action-target = window
                    window.open("/{0}/{1}/{2}/".format(actionInstance.ControllerName, actionInstance.ActionName, actionInstance.LeadId), "_blank");
                }

                break;
            case action.requestTypes.post:
                if (source === action.sources.lead) {
                    if (target === action.targets.message) {
                        // using dynamic data to support "Message Compose"
                        email.methods.showModal(data.recipients, "", "");
                    }
                }
                break;
            case action.requestTypes.put:
                if (source === action.sources.assignment) {
                    if (target === action.targets.ajax) {
                        ajax.callers.crm(
                            actionInstance.ControllerName
                            , ajaxAction
                            , data
                            , callback);
                    }
                }
                break;
            case action.requestTypes.delete:
                break;
            default:
                break;
        }
    },
}