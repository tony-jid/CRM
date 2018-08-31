var lead = {
    events: {
        onActionChanged: function (e) {
            action.perform(action.sources.lead, e.selectedItem);
            e.component.reset();
        }
    }
}