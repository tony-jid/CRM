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
        onContentReady: function (e, id) {
            //console.log(dxGrid.options.id(e));

            dxGrid.toolbar.methods.resetTotalCount(e, dxGrid.options.id(e));
            dxGrid.methods.setTooltip(dxGrid.options.id(e));
        },
        onToolbarPreparing: function (e, id) {
            e.toolbarOptions.items.unshift(
                dxGrid.toolbar.widgets.totalCount(e, dxGrid.options.id(e)),
                dxGrid.toolbar.widgets.btnRefresh(e),
            );

            if (dxGrid.options.masterDetail(e).enabled == true) {
                e.toolbarOptions.items.push(dxGrid.toolbar.widgets.btnExpandAll(e));
                e.toolbarOptions.items.push(dxGrid.toolbar.widgets.btnCollapseAll(e));
            }
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
        },

        methods: {
            resetTotalCount: function (e, id) {
                e.component
                    .option("dataSource")
                    .store
                    .load()
                    .done(function (data) {
                        $(dxGrid.toolbar.ids.totalCount.format("#", id)).text(data.length);
                    });
            }
        },
    },
}


var CustomGrid = {
    toolbarCountId: 'grid-{0}-toolbar-count',
    
    toolbarBtnRefresh: function (e) {
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
    toolbarCountSection: function (e, itemUnit) {
        return {
            localtion: "before",
            template: this.toolbarCountTemplate(e, itemUnit)
        }
    },

    toolbarCountTemplate: function (e, itemUnit) {
        //this.count(e, itemUnit);

        return $("<div/>")
            .addClass("informer")
            .append(
                $('<h2 id="{0}" />'.format(this.toolbarCountId.format(itemUnit)))
                    .addClass("count")
                    .text(0),
                $("<span />")
                    .addClass("name")
                    .text("Total Items")
        );
    },
    count: function (e, itemUnit) {
        //$('#' + e.element.attr('id')).dxDataGrid('instance')
        e.component
            .option("dataSource")
            .store
            .load()
            .done(function (data) {
                CustomGrid.setToolbarTotalItems(itemUnit, data);
                //$('#grid-' + itemUnit + '-toolbar-count').text(data.length);
            });
    },
    setToolbarTotalItems: function (gridName, dataSource) {
        $('#grid-' + gridName + '-toolbar-count').text(dataSource.length);
    }
}

var CustomGridEvent = {
    onSelectionChangedExpandDetails: function (e) {
        e.component.collapseAll(-1);
        e.component.expandRow(e.currentSelectedRowKeys[0]);
    },
    onContentReady: function (e, gridName) {
        CustomGrid.count(e, gridName);
    },
    onToolbarPreparing: function (e, gridName) {
        e.toolbarOptions.items.unshift(
            {
                location: "before",
                template: CustomGrid.toolbarCountTemplate(e, gridName)
            },
            CustomGrid.toolbarBtnRefresh(e)
        );
    }
}