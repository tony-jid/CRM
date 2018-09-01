var email = {
    action: {},
    recipients: [],

    ids: {
        btnSend: "#btnSend",
        emailRecipients: "#emailRecipients",
        emailSubject: "#emailSubject",
        emailEditor: "#emailEditor",
        emailModal: '#mail-compose-popup',
    },

    handlers: {
        shownModal: function (recipients, subject, msg) {
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
                ajax.callers.crm(
                    ajax.controllers.message.name
                    , ajax.controllers.message.actions.sendMessage
                    , model
                    , email.handlers.onSendSuccess);
            });
        },
        onSendSuccess: function (response) {
            alert(JSON.stringify(response));
        }
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
    },

    methods: {
        showModal: function (recipients, subject, msg) {

            email.handlers.shownModal(recipients, subject, msg);
            email.instances.modal().show();
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
                , email.methods.getMessage());

        }
    }
};