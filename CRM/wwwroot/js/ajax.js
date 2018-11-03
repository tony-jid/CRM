var ajax = {
    urls: {
        crm: '/{0}/{1}',
    },

    controllers: {
        company: {
            name: 'Company',
            actions: {
                updateCompany: {
                    type: 'PUT',
                    name: 'UpdateCompany'
                },
            },
        },
        message: {
            name: 'Message',
            actions: {
                sendMessage: {
                    type: 'POST',
                    name: 'Send'
                },
            },
        },
        assignment: {
            name: 'LeadAssignments',
            actions: {
                getByLeads: { // param => id
                    type: 'GET',
                    name: 'GetByLeads',
                    params: function (leadIds) {
                        return ajax.methods.getListParams(leadIds);
                    }
                },
                assignPartners: {
                    type: 'POST',
                    name: 'AjaxPostToAssignPartners'
                },
            },
        },
        reports: {
            name: "reports",
            actions: {
                updateInvoicedLeadAssignmentStatus: {
                    type: "PUT",
                    name: "UpdateInvoicedLeadAssignmentStatus",
                    params: function (leadAssignmentIds) {
                        return ajax.methods.getListParams(leadAssignmentIds);
                    },
                },
            },
        },
    },

    callers: {
        crm: function (ajaxController, ajaxAction, values, onSuccess) {
            var ajaxUrl = ajax.urls.crm.format(ajaxController, ajaxAction.name);

            var ajaxBodyData = "";
            if (ajaxAction.type === "GET") {
                ajaxUrl = ajaxUrl.concat("?").concat(values);
            } else {
                ajaxBodyData = JSON.stringify(values);
            }

            $.ajax({
                url: ajaxUrl,
                type: ajaxAction.type,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: ajaxBodyData,
                success: function (response) {
                    if (typeof (onSuccess) === "function") {
                        onSuccess(response);
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    alert('Request Status: ' + xhr.status + '; Error: ' + errorThrown + '; Status Text: ' + textStatus);
                }
            });
        },
    },

    methods: {
        getListParams: function (listValue, paramName) {
            var params = "";
            if (!site.methods.isDefined(paramName))
                paramName = "ids";

            for (var i = 0; i < listValue.length; i++) {
                if (params.length)
                    params = params.concat("&");

                params = params.concat(paramName).concat("=").concat(listValue[i]);
            }

            return params;
        },
    },
};