using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CRM.Enum;
using CRM.Helpers;
using CRM.Models;
using CRM.Models.ViewModels;
using CRM.Repositories;
using CRM.Services;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRM.Controllers
{
    [Authorize(Roles = nameof(EnumApplicationRole.Admin) 
        + "," + nameof(EnumApplicationRole.Manager) 
        + "," + nameof(EnumApplicationRole.Agent) 
        + "," + nameof(EnumApplicationRole.Partner))]
    public class PartnersController : BaseController
    {
        private IUnitOfWork _uow;
        private IPartnerRepository _partnerRepo;
        private IPartnerBranchRepository _partnerBranchRepo;
        private IHostingEnvironment _hostingEnvironment;
        private AccountManager _accountManager;

        public string LogoPath { get {
                return Path.Combine(_hostingEnvironment.WebRootPath, ImageHelper.PATH_PARTNER);
            }
        }

        public PartnersController(IUnitOfWork unitOfWork, IHostingEnvironment hostingEnvironment, IEmailSender emailSender
            , UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _uow = unitOfWork;
            _partnerRepo = unitOfWork.PartnerRepository;
            _partnerBranchRepo = unitOfWork.PartnerBranchRepository;
            _hostingEnvironment = hostingEnvironment;
            _accountManager = new AccountManager(userManager, roleManager, signInManager, emailSender);
        }

        //[HttpGet("{partnerId}")]
        //public IActionResult Portal (Guid partnerId)
        [HttpGet]
        [Authorize(Roles = nameof(EnumApplicationRole.Partner))]
        public IActionResult Portal()
        {
            //if (!User.IsInRole(nameof(EnumApplicationRole.Partner)))
            //    RedirectToAction(nameof(AccountController.AccessDenied), nameof(EnumController.Account));

            // *** For test, will be assigned by "Partner User Login"
            //Guid partnerId = new Guid("5DD63725-0737-4D40-5853-08D62779DCF3");

            var user = _accountManager.GetUserAsync(User.Identity.Name).Result;
            var salesPersonId = user.Id;

            var partner = _partnerRepo.GetBySalesPerson(salesPersonId);
            return View(partner);
        }

        [HttpGet]
        [Authorize(Roles = nameof(EnumApplicationRole.Admin) + "," + nameof(EnumApplicationRole.Manager) + "," + nameof(EnumApplicationRole.Agent))]
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

            // Adding partner
            _partnerRepo.Add(partner);

            // Adding services
            var partnerVM = new PartnerVM();
            JsonConvert.PopulateObject(values, partnerVM);

            _partnerRepo.AddServices(partner.Id, partnerVM.Services);

            // Adding logo
            if (!String.IsNullOrEmpty(partner.Logo))
            {
                this.CreateLogoFromTempFile(partner);
            }

            return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        public IActionResult UploadLogo()
        {
            try
            {
                IFormFile logo = Request.Form.Files["logo"];
                string tempFileName = Request.Query["fileName"].ToString();
                
                var tempFilePath = Path.Combine(this.LogoPath, tempFileName);

                if (FileHelper.SaveFile(logo, tempFilePath))
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
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
            
            // Updating partner
            _partnerRepo.Update(partner);

            // Updating services
            // In editing mode
            //      - services are [] => all services removed
            //      - services are null => no changes
            var partnerVM = new PartnerVM();
            JsonConvert.PopulateObject(values, partnerVM);

            if (partnerVM.Services != null)
                _partnerRepo.UpdateServices(key, partnerVM.Services);

            // Updating logo
            if (!String.IsNullOrEmpty(partner.Logo))
            {
                // If the logo's name is equal to partner's id, means that there is no new logo updated.
                //
                if (Path.GetFileNameWithoutExtension(partner.Logo) != partner.Id.ToString())
                {
                    this.CreateLogoFromTempFile(partner);
                }
            }

            return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete]
        public void Delete(Guid key)
        {
            var model = _partnerRepo.GetByUid(key);

            _partnerRepo.RemoveServices(key);
            _partnerRepo.Remove(model);

            if (!String.IsNullOrEmpty(model.Logo))
                FileHelper.DeleteFile(Path.Combine(this.LogoPath, model.Logo));

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

        private void CreateLogoFromTempFile(Partner partner) {
            string tempFileName;
            var tempFilePath = Path.Combine(this.LogoPath, partner.Logo);

            using (var tempFileStream = FileHelper.OpenFile(tempFilePath))
            {
                tempFileName = tempFileStream.Name;

                var newFileName = partner.Id + Path.GetExtension(tempFileStream.Name);
                var newFilePath = Path.Combine(this.LogoPath, newFileName);

                FileHelper.SaveFile(tempFileStream, newFilePath);
                partner.Logo = newFileName;
            }

            FileHelper.DeleteFile(tempFileName);
        }
    }
}