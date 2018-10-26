var rating = {
    action: {},
    callback: function () { },

    ids: {
        modal: "#rating-popup",
        txtComment: "#txtComment",
        lblCommentedOn: "#lblCommentedOn",
        btnSubmit: "#btnSubmit",
    },

    handlers: {
        showModal: function (comment, commentedOn, actionInstance, callback) {
            // Keep "actionInstance", as controller and action pointers
            rating.action = actionInstance;

            if (site.methods.isFunction(callback))
                rating.callback = function (response) { callback(response); rating.methods.hideModal(); };
            else
                rating.callback = rating.callbacks.onDefaultCommentSuccess;
            
            rating.instances.modal().off('shown');
            rating.instances.modal().on({
                'shown': function (e) {
                    rating.handlers.clickBtnSubmit();

                    rating.methods.setComment(comment);
                    rating.methods.setCommentedOn(commentedOn);
                }
            });
        },
        clickBtnSubmit: function () {
            $(rating.ids.btnSubmit).unbind();
            $(rating.ids.btnSubmit).click(function () {
                var model = rating.methods.getRatingVM();

                //console.log(rating.action);
                //console.log(model);

                ajax.callers.crm(
                    rating.action.ControllerName
                    , { name: rating.action.ActionName, type: rating.action.RequestType }
                    , model
                    , rating.callback);
            });
        },
    },

    callbacks: {
        onDefaultCommentSuccess: function (response) {
            notification.alert.showSuccess("Successfully commented the lead.");
            rating.methods.hideModal();
        },
    },

    instances: {
        modal: function () {
            return $(rating.ids.modal).dxPopup("instance");
        },
        comment: function () {
            return $(rating.ids.txtComment).dxTextArea("instance");
        },
    },

    methods: {
        showModal: function (comment, commentedOn, actionInstance, callback) {
            rating.handlers.showModal(comment, commentedOn, actionInstance, callback);
            rating.instances.modal().show();
        },
        hideModal: function () {
            rating.instances.modal().hide();
        },
        getRatingVM: function () {
            return new LeadAssignmentRatingVM(
                site.methods.isDefined(rating.action.LeadId) ? rating.action.LeadId : ""
                , site.methods.isDefined(rating.action.LeadAssignmentId) ? rating.action.LeadAssignmentId : 0
                , rating.methods.getComment()
            );
        },
        setComment: function (text) {
            rating.instances.comment().option("value", text);
        },
        getComment: function () {
            return rating.instances.comment().option("value");
        },
        setCommentedOn: function (text) {
            $(rating.ids.lblCommentedOn).text(text);
        },
    },
};