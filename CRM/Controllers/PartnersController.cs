using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;
using CRM.Models.ViewModels;
using CRM.Repositories;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRM.Controllers
{
    public class PartnersController : BaseController
    {
        private IUnitOfWork _uow;
        private IPartnerRepository _partnerRepo;

        public PartnersController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _partnerRepo = unitOfWork.PartnerRepository;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(this.GetPartnerVMs(_partnerRepo.Get()), loadOptions);
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var partner = new Partner();
            JsonConvert.PopulateObject(values, partner);

            if (!TryValidateModel(partner))
                return BadRequest(GetFullErrorMessage(ModelState));

            _partnerRepo.Add(partner);

            // Adding services
            var partnerVM = new PartnerVM();
            JsonConvert.PopulateObject(values, partnerVM);

            _partnerRepo.AddServices(partner.Id, partnerVM.Services);

            return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public IActionResult Put(Guid key, string values)
        {
            Partner partner = _partnerRepo.GetByUid(key);
            if (partner == null)
                return StatusCode(409, "Partner not found");

            if (!TryValidateModel(partner))
                return BadRequest(GetFullErrorMessage(ModelState));

            JsonConvert.PopulateObject(values, partner);            
            
            _partnerRepo.Update(partner);

            // Updating services
            // In editing mode
            //      - services are [] => all services removed
            //      - services are null => no changes
            var partnerVM = new PartnerVM();
            JsonConvert.PopulateObject(values, partnerVM);

            if (partnerVM.Services != null)
                _partnerRepo.UpdateServices(key, partnerVM.Services);

            return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete]
        public void Delete(Guid key)
        {
            var model = _partnerRepo.GetByUid(key);

            _partnerRepo.RemoveServices(key);
            _partnerRepo.Remove(model);

            _uow.Commit();
        }

        private PartnerVM GetPartnerVM(Partner partner)
        {
            return new PartnerVM() {
                Id = partner.Id,
                Name = partner.Name,
                Logo = partner.Logo,
                Branches = partner.Branches,
                PartnerServices = partner.PartnerServices,
                Services = partner.PartnerServices != null ? partner.PartnerServices.Select(i => i.LeadTypeId).ToArray() : new int[] { }
            };
        }

        private IEnumerable<PartnerVM> GetPartnerVMs(IEnumerable<Partner> partners)
        {
            return partners.Select(s => this.GetPartnerVM(s));
        }
    }
}