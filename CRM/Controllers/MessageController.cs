using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Models.ViewModels;
using CRM.Repositories;
using CRM.Services;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}