var company = {
    ids: {
        formCompany: "#formCompany",
        btnUpdate: "#btnUpdate",
    },

    instances: {
        formCompany: function () {
            return $(company.ids.formCompany).dxForm("instance");
        },
    },

    handlers: {
        contentReadyFormCompany: function () {
            //company.handlers.clickBtnUpdate();
        },
        clickBtnUpdate: function () {
            ajax.callers.crm(
                ajax.controllers.company.name
                , ajax.controllers.company.actions.updateCompany
                , company.methods.getCompanyData()
                , company.handlers.onUpdateInfoSuccess);
        },
        onUpdateInfoSuccess: function (response) {
            notification.alert.show('Successfully updated the company info', response.StatusCode);
        },
    },

    methods: {
        getCompanyData: function () {
            return company.instances.formCompany().option("formData");
        },
    },
}