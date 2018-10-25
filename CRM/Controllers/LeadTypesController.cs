using CRM.Enum;
using CRM.Helpers;
using CRM.Models;
using CRM.Repositories;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CRM.Controllers
{
    [Authorize(Roles = nameof(EnumApplicationRole.Admin) 
        + "," + nameof(EnumApplicationRole.Manager)
        + "," + nameof(EnumApplicationRole.Agent))]
    public class LeadTypesController : BaseController
    {
        private IUnitOfWork _uow;
        private ILeadTypeRepository _leadTypeRepo;
        private IHostingEnvironment _hostingEnvironment;

        public string ImagePath
        {
            get
            {
                return Path.Combine(_hostingEnvironment.WebRootPath, ImageHelper.PATH_LEAD_TYPE);
            }
        }

        public LeadTypesController(IUnitOfWork unitOfWork, IHostingEnvironment hostingEnvironment)
        {
            _uow = unitOfWork;
            _leadTypeRepo = unitOfWork.LeadTypeRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_leadTypeRepo.Get(), loadOptions);
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var model = new LeadType();
            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _leadTypeRepo.Add(model);

            // Adding logo
            if (!String.IsNullOrEmpty(model.Image))
            {
                this.CreateImageFromTempFile(model);
            }

            return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        public IActionResult UploadImage()
        {
            try
            {
                IFormFile image = Request.Form.Files["image"];
                string tempFileName = Request.Query["fileName"].ToString();

                var tempFilePath = Path.Combine(this.ImagePath, tempFileName);

                if (FileHelper.SaveFile(image, tempFilePath))
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
        public IActionResult Put(int key, string values)
        {
            var model = _leadTypeRepo.Get(key);
            if (model == null)
                return StatusCode(409, "LeadType not found");

            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _leadTypeRepo.Update(model);

            // Updating logo
            if (!String.IsNullOrEmpty(model.Image))
            {
                // If the logo's name is equal to partner's id, means that there is no new logo updated.
                //
                if (Path.GetFileNameWithoutExtension(model.Image) != model.Id.ToString())
                {
                    this.CreateImageFromTempFile(model);
                }
            }

            return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete]
        public IActionResult Delete(int key)
        {
            var model = _leadTypeRepo.Get(key);

            _leadTypeRepo.Remove(model);

            if (_uow.Commit(this.ModelState))
            {
                if (!String.IsNullOrEmpty(model.Image))
                    FileHelper.DeleteFile(Path.Combine(this.ImagePath, model.Image));

                return Ok();
            }
            else
                return BadRequest(GetFullErrorMessage(this.ModelState));
        }

        private void CreateImageFromTempFile(LeadType leadType)
        {
            string tempFileName;
            var tempFilePath = Path.Combine(this.ImagePath, leadType.Image);

            using (var tempFileStream = FileHelper.OpenFile(tempFilePath))
            {
                tempFileName = tempFileStream.Name;

                var newFileName = leadType.Id + Path.GetExtension(tempFileStream.Name);
                var newFilePath = Path.Combine(this.ImagePath, newFileName);

                FileHelper.SaveFile(tempFileStream, newFilePath);
                leadType.Image = newFileName;
            }

            FileHelper.DeleteFile(tempFileName);
        }
    }
}