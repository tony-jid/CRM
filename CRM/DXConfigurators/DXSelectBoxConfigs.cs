using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models.ViewModels;
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Mvc.Builders;

namespace CRM.DXConfigurators
{
    public static class DXSelectBoxConfigs
    {
        public static SelectBoxBuilder ActionCommonConfigs(this SelectBoxBuilder selectBox)
        {
            return selectBox
                .Placeholder("Actions..")
                .DisplayExpr("ActionName")
                .ValueExpr("ActionName")
                .SearchEnabled(false)
                //.OnSelectionChanged("function(e) { alert(e.selectedItem.ActionName); e.component.reset(); }")
                .DropDownButtonTemplate("<span class='batch-icon batch-icon-menu-pull-down text-danger' />")
                .ItemTemplate("<span class='<%= Icon %> text-primary' /> <%= DisplayName %>");
        }
    }
}
