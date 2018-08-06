using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;
using CRM.Repositories;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM.Controllers
{
    public class AgentsController : BaseController
    {
        private IUnitOfWork _uow;
        private IAgentRepository _agentRepo;

        public AgentsController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _agentRepo = unitOfWork.AgentRepository;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_agentRepo.Get(), loadOptions);
        }

        [HttpGet]
        public object GetAgentsByOffice(int officeId, DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_agentRepo.GetAgentsByOffice(officeId), loadOptions);
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var model = new Agent();
            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _agentRepo.Add(model);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Guid key, string values)
        {
            var model = _agentRepo.GetByUid(key);
            if (model == null)
                return StatusCode(409, "Agent not found");

            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _agentRepo.Update(model);

            return Ok();
        }

        [HttpDelete]
        public void Delete(Guid key)
        {
            var model = _agentRepo.GetByUid(key);

            _agentRepo.Remove(model);
        }
    }
}
