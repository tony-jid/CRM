using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Mvc.Builders;

namespace CRM.DXConfigurators
{
    public static class DXGridConfigs
    {
        public static DataGridBuilder<T> CommonConfigs<T>(this DataGridBuilder<T> grid)
        {
            return grid
                .ShowBorders(true)
                .ShowRowLines(true)
                .ShowColumnLines(false)
                .RowAlternationEnabled(true)
                .HoverStateEnabled(true)
                .RemoteOperations(false)
                .WordWrapEnabled(true)
                .SearchPanel(DXGridConfigs.ShowSearchPanel())
                .Paging(DXGridConfigs.MasterPaging())
                .Pager(DXGridConfigs.Pager())
                .OnContentReady("dxGrid.handlers.onContentReady")
                .OnToolbarPreparing("dxGrid.handlers.onToolbarPreparing")
                .OnRowInserted("dxGrid.handlers.onRowInserted")
                .OnRowUpdated("dxGrid.handlers.onRowUpdated")
                .OnRowRemoved("dxGrid.handlers.onRowRemoved")
                .OnRowClick("dxGrid.handlers.onRowDoubleClick")
            ;
        }

        //public static DataGridBuilder<T> CommonConfigs<T>(this DataGridBuilder<T> grid, string gridId)
        //{
        //    return grid
        //        //.ID(new JS("'" + gridId + "'.concat(" + suffixId + ")"))
        //        //.ID(new JS($"'{gridId}'.concat('{suffixId}')"))
        //        .ID(gridId)
        //        .ShowBorders(true)
        //        .ShowRowLines(true)
        //        .ShowColumnLines(false)
        //        .RowAlternationEnabled(true)
        //        .HoverStateEnabled(true)
        //        .RemoteOperations(false)
        //        .Paging(DXGridConfigs.MasterPaging())
        //        .Pager(DXGridConfigs.Pager())
        //        .OnContentReady($"function(e) {{ dxGrid.handlers.onContentReady(e, '{gridId}'); }}")
        //        .OnToolbarPreparing($"function(e) {{ dxGrid.handlers.onToolbarPreparing(e, '{gridId}'); }}")
        //        .OnRowInserted("dxGrid.handlers.onRowInserted")
        //        .OnRowUpdated("dxGrid.handlers.onRowUpdated")
        //        .OnRowRemoved("dxGrid.handlers.onRowRemoved")
        //        .OnRowClick("dxGrid.handlers.onRowDoubleClick")
        //    ;
        //}

        public static Action<DataGridPagingBuilder> MasterPaging()
        {
            return (paging) =>
            {
                paging.PageSize(10);
            };
        }

        public static Action<DataGridPagingBuilder> DetailsPaging()
        {
            return (paging) =>
            {
                paging.PageSize(5);
            };
        }

        public static Action<DataGridPagerBuilder> Pager()
        {
            return (pager) => {
                pager.ShowPageSizeSelector(false);
                pager.ShowInfo(true);
                pager.Visible(true);
            };
        }

        public static Action<DataGridPagerBuilder> NoPager()
        {
            return (pager) => {
                pager.ShowPageSizeSelector(false);
                pager.ShowInfo(false);
                pager.Visible(false);
            };
        }

        public static Action<DataGridPagerBuilder> PagerShowSelector()
        {
            return (pager) => {
                pager.ShowPageSizeSelector(true);
                pager.ShowInfo(true);
                pager.Visible(true);
                pager.AllowedPageSizes(new int[] { 10, 20, 50, 100 });
            };
        }

        public static Action<DataGridEditingBuilder<T>> Editing<T>(string popupTitle)
        {
            return (editing) => {
                editing.Mode(GridEditMode.Popup);
                editing.UseIcons(true);
                editing.AllowAdding(true);
                editing.AllowUpdating(true);
                editing.AllowDeleting(true);
                editing.Popup(DXGridConfigs.EditingPopup(popupTitle));
            };
        }

        public static Action<PopupBuilder> EditingPopup(string title, int maxHeight = 400)
        {
            return (popup) => {
                popup.Title(title);
                popup.ShowTitle(true);
                popup.DragEnabled(false);
                popup.MaxHeight(maxHeight);
                popup.Position(pos => pos
                    .Of(new JS("window"))
                    .Offset(0, 50)
                );
            };
        }

        public static Action<DataGridSelectionBuilder> SelectionCheckBoxMode()
        {
            return selection =>
            {
                selection.AllowSelectAll(true);
                selection.ShowCheckBoxesMode(GridSelectionShowCheckBoxesMode.Always);
                selection.Mode(SelectionMode.Multiple);
            };
        }

        public static Action<DataGridSearchPanelBuilder> ShowSearchPanel()
        {
            return searchPanel =>
            {
                searchPanel.Visible(true);
                searchPanel.Width(200);
                searchPanel.SearchVisibleColumnsOnly(true);
                searchPanel.HighlightSearchText(false);
            };
        }
    }
}
