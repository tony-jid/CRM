using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enum
{
    public enum EnumErrorMessageHints
    {
        [Description("")]
        EXCEPTION = 0,

        [Description("REFERENCE constraint")]
        REFERENCE_CONSTRAINT = 1,
    }

    public enum EnumErrorMessageDescriptions
    {
        [Description("Warning! Internal service error, please try again.")]
        EXCEPTION = 0,

        [Description("Warning! Could not delete the item because there is important data related to it.")]
        REFERENCE_CONSTRAINT = 1,
    }
}
