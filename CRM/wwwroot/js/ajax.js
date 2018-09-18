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

            $.ajax({
                url: ajaxUrl,
                type: ajaxAction.type,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(values),
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