using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models;
using CRM.Models.ViewModels;
using CRM.Repositories;
using CRM.Services;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRM.Controllers
{
    public class MessageController : BaseController
    {
        private IUnitOfWork _uow;
        private IMessageRepository _msgRepo;
        private IEmailSender _emailSender;

        public MessageController(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _uow = unitOfWork;
            _msgRepo = unitOfWork.MessageRepository;

            _emailSender = emailSender;
        }
        
        public virtual IActionResult Templates()
        {
            return View();
        }

        [HttpGet]
        public object GetRecipients(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_msgRepo.GetRecipients(), loadOptions);
        }

        [HttpPost]
        public JsonResult Send([FromBody]MessageViewModel data)
        {
            _emailSender.SendEmailAsync(data.Recipients.Select(s => s.Email).ToArray(), data.Subject, data.Message);

            return Json(data);
        }

        //[HttpGet]
        //public object GetTemplates()
        //{
        //    return _msgRepo.GetTemplates();
        //}
        [HttpGet]
        public object GetTemplates(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_msgRepo.GetTemplates(), loadOptions);
        }

        [HttpPost]
        public IActionResult AddTemplate(string values)
        {
            var model = new MessageTemplate();
            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _msgRepo.AddTemplate(model);

            return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public IActionResult UpdateTemplate(int key, string values)
        {
            var model = _msgRepo.GetTemplate(key);
            if (model == null)
                return StatusCode(409, "Message Template not found");

            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            _msgRepo.UpdateTemplate(model);

            return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete]
        public IActionResult RemoveTemplate(int key)
        {
            var model = _msgRepo.GetTemplate(key);
            if (model == null)
                return StatusCode(409, "Message Template not found");

            _msgRepo.RemoveTemplate(model);

            return _uow.Commit() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}