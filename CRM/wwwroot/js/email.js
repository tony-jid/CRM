var email = {
    action: {},
    callback: function () { },
    recipients: [],

    ids: {
        btnSend: "#btnSend",
        emailRecipients: "#emailRecipients",
        emailSubject: "#emailSubject",
        emailEditor: "#emailEditor",
        emailModal: "#mail-compose-popup",
        selectBoxTemplates: "#selectBoxTemplates",
    },

    handlers: {
        shownModal: function (recipients, subject, msg, actionInstance, callback) {
            // Keep "actionInstance", as controller and action pointers
            email.action = actionInstance;
            
            if (site.methods.isFunction(callback))
                email.callback = function (response) { callback(response); email.methods.hideModal(); };
            else
                email.callback = email.callbacks.onDefaultSendSuccess;

            email.instances.modal().off('shown');
            email.instances.modal().on({
                'shown': function (e) {
                    email.handlers.clickBtnSend();

                    email.methods.setRecipients(recipients);
                    email.methods.setSubject(subject);
                    //CKEditor must be initialized everytime the modal is showing
                    email.methods.initEditor(msg)
                }
            });
        },
        clickBtnSend: function () {
            $(email.ids.btnSend).unbind();
            $(email.ids.btnSend).click(function () {
                var model = email.methods.getMessageModel();

                //console.log(email.action);
                console.log(model);

                ajax.callers.crm(
                    //ajax.controllers.message.name
                    //, ajax.controllers.message.actions.sendMessage
                    email.action.ControllerName
                    , { name: email.action.ActionName, type: email.action.RequestType }
                    , model
                    , email.callback);
            });
        },
        onTemplatesSelectionChanged: function (e) {
            if (e.selectedItem !== null) {
                email.methods.setSubject(e.selectedItem.MessageSubject);
                email.methods.setMessage(e.selectedItem.MessageBody);

                e.component.reset();
            }
        },
    },

    callbacks: {
        onDefaultSendSuccess: function (response) {
            notification.alert.showSuccess("Successfully send the message.");
            email.methods.hideModal();
            //alert(JSON.stringify(response));
        },
    },

    instances: {
        modal: function () {
            return $(email.ids.emailModal).dxPopup("instance");
        },
        recipients: function () {
            return $(email.ids.emailRecipients).dxTagBox("instance");
        },
        editor: function () {
            return CKEDITOR.instances.emailEditor;
        },
        selectBoxTemplates: function () {
            return $(email.ids.selectBoxTemplates).dxSelectBox("instance");
        },
    },

    methods: {
        showModal: function (recipients, subject, msg, actionInstance, callback) {
            email.handlers.shownModal(recipients, subject, msg, actionInstance, callback);
            email.instances.modal().show();
        },
        hideModal: function () {
            email.instances.modal().hide();
        },
        initEditor: function (msg) {
            if (typeof (email.instances.editor()) !== 'undefined') {
                email.instances.editor().destroy();
            }

            // Set message on the editor after the initialization
            $(email.ids.emailEditor).ckeditor(function (e) {
                email.methods.setMessage(msg);
            });
        },
        setMessage: function (msg) {
            email.instances.editor().setData(msg);
        },
        getMessage: function () {
            return email.instances.editor().getData();
        },
        setSubject: function (text) {
            $(email.ids.emailSubject).val(text);
        },
        getSubject: function () {
            return $(email.ids.emailSubject).val();
        },
        setRecipients: function (emails) {
            email.instances.recipients().option("value", emails);
        },
        getRecipients: function () {
            return email.instances.recipients().option("selectedItems");
        },
        getMessageModel: function () {
            return new MessageViewModel(
                email.methods.getRecipients()
                , email.methods.getSubject()
                , email.methods.getMessage()
                , site.methods.isDefined(email.action.CustomerId) ? email.action.CustomerId : ""
                , site.methods.isDefined(email.action.LeadId) ? email.action.LeadId : ""
                , site.methods.isDefined(email.action.LeadAssignmentId) ? email.action.LeadAssignmentId : 0
            );

        }
    }
};