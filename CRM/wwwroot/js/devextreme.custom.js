var dxGrid = {
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