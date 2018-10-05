var customer = {
    ids: {
        
    },

    templates: {
    },

    handlers: {},

    instances: {},

    methods: {},

    // Overiding functions === Start ===
    //
    dxGrid: {
        handlers: {
            onToolbarPreparing: function (e_grid) {
                dxGrid.handlers.onToolbarPreparing(e_grid);

                dxGrid.toolbar.methods.addToolbarItem(e_grid,
                    dxGrid.toolbar.widgets.optionFilter(e_grid, stateTextValues, "Address.State", "All State")
                );
            }
        }
    },
}