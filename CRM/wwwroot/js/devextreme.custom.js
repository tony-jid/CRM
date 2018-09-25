var dxGrid = {
    vars: {
        clickTimer: null,
        prevClickedRowIndex: -1,
    },

    options: {
        id: function (e) {
            return e.element.attr('id');
        },
        masterDetail: function (e) {
            return e.component.option("masterDetail");
        },
        dataItems: function (e) {
            return e.component.getDataSource().items();
        },
    },

    handlers: {
        onRowInserted: function (e) {
            notification.alert.showSuccess("Successfully added an item");
        },
        onRowUpdated: function (e) {
            notification.alert.showSuccess("Successfully updated the item");
        },
        onRowRemoved: function (e) {
            notification.alert.showSuccess("Successfully removed the item");
        },
        onContentReady: function (e) {
            //console.log(dxGrid.options.id(e));

            dxGrid.toolbar.methods.resetTotalCount(e, dxGrid.options.id(e));
            dxGrid.methods.setTooltip(dxGrid.options.id(e));
        },
        onToolbarPreparing: function (e) {
            e.toolbarOptions.items.unshift(
                dxGrid.toolbar.widgets.totalCount(e, dxGrid.options.id(e)),
                dxGrid.toolbar.widgets.btnRefresh(e),
            );

            if (dxGrid.options.masterDetail(e).enabled == true) {
                e.toolbarOptions.items.push(dxGrid.toolbar.widgets.btnExpandAll(e));
                e.toolbarOptions.items.push(dxGrid.toolbar.widgets.btnCollapseAll(e));
            }

            dxGrid.toolbar.methods.setSearchPanelLocation(e.toolbarOptions.items);

            //console.log(e.toolbarOptions.items);
        },
        onRowDoubleClick: function (e) {
            // Not reasonable, in detail-gris, this event fired twice & e.rowIndex is not correct
            // Solution => checking undefined of and using e.dataIndex instead
            //console.log("RowIndex[{0}] is clicked!".format(e.dataIndex));

            if (typeof (e.dataIndex) !== "undefined") {
                if (dxGrid.vars.clickTimer && dxGrid.vars.prevClickedRowIndex === e.dataIndex) {
                    // double click
                    clearTimeout(dxGrid.vars.clickTimer);
                    dxGrid.vars.clickTimer = null;

                    if (dxGrid.options.masterDetail(e).enabled == true)
                        dxGrid.methods.toggleExpandRow(e, e.key);

                    //console.log("RowIndex[{0}] is double clicked!".format(e.dataIndex));
                } else {
                    // signle click
                    dxGrid.vars.clickTimer = setTimeout(function () {
                        //console.log('single click');
                        clearTimeout(dxGrid.vars.clickTimer);
                        dxGrid.vars.clickTimer = null;
                    }, 250);
                }

                dxGrid.vars.prevClickedRowIndex = e.dataIndex;
            }
        },
    },

    methods: {
        setTooltip: function (id) {
            $('#' + id).find('[data-toggle="tooltip"]').tooltip();
        },
        toggleExpandRow: function (e, rowKey) {
            if (e.component.isRowExpanded(rowKey))
                e.component.collapseRow(rowKey);
            else
                e.component.expandRow(rowKey);
        },
        collapseAll: function (e) {
            e.component.collapseAll(-1);
        },
        expandAll: function (e) {
            e.component.expandAll(-1);
        },
    },

    toolbar: {
        ids: {
            totalCount: "{0}grid-{1}-toolbar-count",
        },

        templates: {
            totalCount: function(e, id) {
                return $("<div/>")
                    .addClass("informer")
                    .append(
                        $('<h2 id="{0}" />'.format(dxGrid.toolbar.ids.totalCount.format("", id)))
                            .addClass("count")
                            .text(0),
                        $("<span />")
                            .addClass("name")
                            .text("Total Items")
                    );
            },
        },

        widgets: {
            totalCount: function (e, id) {
                return {
                    location: "before",
                    template: dxGrid.toolbar.templates.totalCount(e, id)
                }
            },
            btnExpandAll: function (e) {
                return {
                    location: "before",
                    widget: "dxButton",
                    options: {
                        icon: "batch-icon batch-icon-marquee-download",
                        hint: "Expand all",
                        onClick: function () {
                            dxGrid.methods.expandAll(e);
                        }
                    }
                }
            },
            btnCollapseAll: function (e) {
                return {
                    location: "before",
                    widget: "dxButton",
                    options: {
                        icon: "batch-icon batch-icon-marquee-upload",
                        hint: "Collapse all",
                        onClick: function () {
                            dxGrid.methods.collapseAll(e);
                        }
                    }
                }
            },
            btnRefresh: function (e) {
                return {
                    location: "after",
                    widget: "dxButton",
                    options: {
                        icon: "refresh",
                        onClick: function () {
                            e.component.refresh();
                        }
                    }
                }
            },
            optionGrouping: function (e, groupingItems) {
                groupingItems.unshift(dxGrid.toolbar.methods.getDefaultGroupingOptionItem());

                return {
                    location: "before",
                    widget: "dxSelectBox",
                    options: {
                        width: 200,
                        //items: [{
                        //    value: "CustomerStoreState",
                        //    text: "Grouping by State"
                        //}, {
                        //    value: "Employee",
                        //    text: "Grouping by Employee"
                        //}],
                        items: groupingItems,
                        displayExpr: "text",
                        valueExpr: "value",
                        value: groupingItems.length ? groupingItems[0].value : "", // default the option by the first item
                        onValueChanged: function (_e) {
                            e.component.clearGrouping();

                            if (_e.value !== "")
                                e.component.columnOption(_e.value, "groupIndex", 0);
                        }
                    }
                }
            },
            optionDateRange: function (e, placeholder, onValueChanged) {
                var dateItems = dateHelper.items.getDateRange();

                if (typeof (placeholder) !== "undefined") {
                    dateItems.unshift(dxGrid.toolbar.methods.newOptionItem(null, placeholder));
                }

                return {
                    location: "before",
                    widget: "dxSelectBox",
                    options: {
                        width: 200,
                        items: dateItems,
                        displayExpr: "text",
                        valueExpr: "value",
                        onValueChanged: typeof (onValueChanged) !== "undefined" ? onValueChanged : function () { }
                    }
                }
            },
            optionDateRange: function (e, placeholder, onValueChanged) {
                var dateItems = dateHelper.items.getDateRange();

                if (typeof (placeholder) !== "undefined") {
                    dateItems.unshift(dxGrid.toolbar.methods.newOptionItem(null, placeholder));
                }

                return {
                    location: "before",
                    widget: "dxSelectBox",
                    options: {
                        width: 200,
                        items: dateItems,
                        displayExpr: "text",
                        valueExpr: "value",
                        onValueChanged: typeof (onValueChanged) !== "undefined" ? onValueChanged : function () { }
                    }
                }
            },
        },

        methods: {
            addToolbarItem: function (e, widget) {
                e.toolbarOptions.items.push(widget);
            },
            getDefaultGroupingOptionItem: function () {
                return { value: "", text: "No Grouping" };
            },
            newOptionItem: function (_value, _text) {
                return { value: _value, text: _text };
            },
            setSearchPanelLocation: function (toolbarItems) {
                var searchPanel = $.grep(toolbarItems, function (item) {
                    if (typeof (item.name) !== "undefined")
                        return item.name === "searchPanel";
                    else
                        return false;
                });

                if (searchPanel.length) {
                    searchPanel[0].location = "before";
                }
            },
            resetTotalCount: function (e, id) {
                //console.log(e.component.getDataSource().items().length);
                $(dxGrid.toolbar.ids.totalCount.format("#", id)).text(dxGrid.options.dataItems(e).length);

                // This statement is poor performance
                //e.component
                //    .option("dataSource")
                //    .store
                //    .load()
                //    .done(function (data) {
                //        $(dxGrid.toolbar.ids.totalCount.format("#", id)).text(data.length);
                //    });
            }
        },
    },
}