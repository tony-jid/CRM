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
        modal: "Modal",
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
                    window.open("/{0}/{1}/{2}/".format(data.ControllerName, data.ActionName, data.LeadId), "_blank");
                }

                break;

            default:
                break;
        }
    },
}