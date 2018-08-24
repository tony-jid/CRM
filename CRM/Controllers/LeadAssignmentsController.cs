using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Repositories;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    public class LeadAssignmentsController : BaseController
    {
        private IUnitOfWork _uow;
        private ILeadAssignmentRepository _leadAssRepo;

        public LeadAssignmentsController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _leadAssRepo = unitOfWork.LeadAssignmentRepository;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions, Guid leadId)
        {
            return DataSourceLoader.Load(_leadAssRepo.GetByLead(leadId), loadOptions);
        }
    }
}