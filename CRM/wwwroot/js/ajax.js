var ajax = {
    urls: {
        crm: '/{0}/{1}',
    },

    controllers: {
        assignment: {
            name: 'LeadAssignments',
            actions: {
                assignPartners: {
                    type: 'POST',
                    name: 'AjaxPostToAssignPartners'
                }
            }
        }
    },

    callers: {
        crm: function (controller, action, values, onSuccess) {
            $.ajax({
                url: ajax.urls.crm.format(controller, action.name),
                type: action.type,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                traditional: true,
                data: JSON.stringify(values),
                success: function (response) {
                    if (typeof (onSuccess) === "function") {
                        onSuccess(response);
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    alert('Request Status: ' + xhr.status + '; Status Text: ' + textStatus + '; Error: ' + errorThrown);
                }
            });
        },
    }
};