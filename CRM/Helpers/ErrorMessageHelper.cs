using CRM.Enum;
using CRM.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Helpers
{
    public static class ErrorMessageHelper
    {
        public static void AddModelStateError(ModelStateDictionary modelState, string errorMessage)
        {
            if (modelState != null)
                modelState.AddModelError(string.Empty, errorMessage);
        }

        public static void AddModelStateError(ModelStateDictionary modelState, EnumErrorMessageDescriptions errorMessageDescription)
        {
            if (modelState != null)
                modelState.AddModelError(string.Empty, errorMessageDescription.GetDesc());
        }

        public static void AddModelStateError(ModelStateDictionary modelState, EnumErrorMessageHints errorMessageHint)
        {
            if (modelState != null)
            {
                var intErrorHint = (int)errorMessageHint;
                var errorMessageDescription = ((EnumErrorMessageDescriptions)intErrorHint);
                ErrorMessageHelper.AddModelStateError(modelState, errorMessageDescription);
            }
        }
    }
}
