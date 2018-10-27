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
                        var params = "";
                        for (var i = 0; i < leadIds.length; i++) {
                            if (params.length)
                                params = params.concat("&");

                            params = params.concat("ids=").concat(leadIds[i]);
                        }

                        return params;
                    }
                },
                assignPartners: {
                    type: 'POST',
                    name: 'AjaxPostToAssignPartners'
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
};