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

    requests: {
        get: "Get",
        post: "Post",
        put: "Put",
        delete: "Delete"
    },

    perform: function (source, data) {
        let request = data.RequestType;
        let target = data.ActionTarget;

        switch (request) {
            case action.requests.get:
                if (source === action.sources.lead) {
                    // action-target = window
                    window.open("/{0}/{1}/{2}/".format(data.ControllerName, data.ActionName, data.LeadId), "_blank");
                }

                break;
            case action.requests.post:
                if (target === action.targets.message) {
                    // using dynamic props to support "Message Compose"
                    email.methods.showModal(data.Message.Recipients, "", "");
                }
                break;
            default:
                break;
        }
    },
}