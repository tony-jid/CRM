var messagetemplate = {
    ids: {
        templateMessageBody: "#templateMessageBody",
        _templateMessageBody: "templateMessageBody"
    },

    templates: {
        editorMessageBody: function (cellElement, cellInfo) {
            if (site.methods.isDefined(cellInfo.item)) {
                if (cellInfo.item.dataField === "MessageBody") {
                    $(cellElement).append("<textarea id='" + messagetemplate.ids._templateMessageBody + "' class='form-control email-body load-ckeditor' rows='8'></textarea>");

                    if (site.methods.isDefined(CKEDITOR.instances.templateMessageBody)) {
                        CKEDITOR.instances.templateMessageBody.destroy();
                    }

                    // Set message on the editor after the initialization
                    $(messagetemplate.ids.templateMessageBody).ckeditor(function (e) {
                        if (site.methods.isDefined(cellInfo.value))
                            CKEDITOR.instances.templateMessageBody.setData(cellInfo.value);

                        CKEDITOR.instances.templateMessageBody.on("change", function (eventInfo) {
                            var text = eventInfo.editor.getData();
                            cellInfo.setValue(text);
                        });
                    });
                }
            }
        },
    },

    handlers: {},

    instances: {},

    methods: {},
}