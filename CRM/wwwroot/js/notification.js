var notification = {
    displayTime: 2000,

    types: {
        success: "success",
        info: "info",
        warning: "warning",
        error: "error",
        getByCode: function (statusCode) {
            if (parseInt(statusCode / notification.statusCodes.success) === 1)
                return notification.types.success;
            else if (parseInt(statusCode / notification.statusCodes.clientError) === 1)
                return notification.types.error;
            else if (parseInt(statusCode / notification.statusCodes.serverError) === 1)
                return notification.types.error;
            else
                return notification.types.warning;
        }
    },

    statusCodes: {
        success: 200,
        clientError: 400,
        serverError: 500,
    },

    alert: {
        show: function (msg, statusCode) {
            DevExpress.ui.notify(msg, notification.types.getByCode(statusCode), notification.displayTime);
        },
        showSuccess: function (msg) {
            DevExpress.ui.notify(msg, notification.types.success, notification.displayTime);
        },
        showInfo: function (msg) {
            DevExpress.ui.notify(msg, notification.types.info, notification.displayTime);
        },
        showWarning: function (msg) {
            DevExpress.ui.notify(msg, notification.types.warning, notification.displayTime);
        },
        showError: function (msg) {
            DevExpress.ui.notify(msg, notification.types.error, notification.displayTime);
        },
    },
}