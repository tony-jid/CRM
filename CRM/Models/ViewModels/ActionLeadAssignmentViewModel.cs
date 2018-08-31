﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Models.ViewModels
{
    public class ActionLeadAssignmentViewModel : IAction
    {
        public Guid PartnerBranchId { get; set; }
        public int LeadAssignmentId { get; set; }

        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string ActionTarget { get; set; }
        public string RequestType { get; set; }
        public string DisplayName { get; set; }
        public string Icon { get; set; }
        public int NextStateId { get; set; }
    }
}