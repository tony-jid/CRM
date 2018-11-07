using CRM.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Helpers
{
    public static class MessageHelper
    {
        public const string CUSTOMER_DETAILS = @"
<b>Customer Details</b>
<ul>
    <li>Name    : {0}</li>
    <li>Email   : {1}</li>
    <li>Contact Number  : {2}</li>
    <li>Business Name   : {3}</li>
    <li>Address         : {4}</li>
</ul>
";

        public const string LEAD_DETAILS = @"
<b>Lead Details</b>
    <li>Lead Type           : {0}</li>
    <li>Lead Details        : {1}</li>
</ul>
";

        public static string GetLeadDetails(LeadVM leadVM)
        {
            var customerDetails = String.Format(MessageHelper.CUSTOMER_DETAILS, leadVM.CustomerName, leadVM.CustomerEmail, leadVM.CustomerContactNumber
                , leadVM.CustomerBusinessName, leadVM.CustomerAddress);
            var leadDetails = String.Format(MessageHelper.LEAD_DETAILS, leadVM.LeadTypeName, leadVM.Details);

            return $"{customerDetails} <br/> {leadDetails}";
        }
    }
}
