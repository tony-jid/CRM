using CRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Helpers
{
    public static class AddressHelper
    {
        public static string MergeAddress(Address address)
        {
            return AddressHelper.MergeAddress(address.StreetAddress, address.Suburb, address.State, address.PostCode);
        }

        public static string MergeAddress(string street, string suburb, string state, string postcode)
        {
            return (string.IsNullOrEmpty(street) ? "" : street + ", ")
                + (string.IsNullOrEmpty(suburb)? "" : suburb + ", ") 
                + state + " " + postcode;
        }
    }
}
