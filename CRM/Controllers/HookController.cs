using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CRM.Enum;
using CRM.Models;
using CRM.Models.ViewModels;
using CRM.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    public class HookController : Controller
    {
        private IUnitOfWork _uow;
        private LeadRepository _leadRepo;
        private ICustomerRepository _cusRepo;
        private MessageController _messageController;

        public HookController(IUnitOfWork unitOfWork, MessageController messageController)
        {
            _uow = unitOfWork;
            _leadRepo = unitOfWork.LeadRepository;
            _cusRepo = unitOfWork.CustomerRepository;
            _messageController = messageController;
        }

        [HttpPost]
        public async Task HookLead([FromBody]HookVM data)
        {
            // Gravity Forms separates form fields with "----------" by considering a section field that come right after a page break
            // So, we can separate lead details by using "--------------------------------"
            //
            try
            {
                var customer = this.CreateCustomerEntity(data);
                if (_cusRepo.IsCustomerExisted(data.EMail))
                {
                    customer = _cusRepo.GetByEmail(customer.EMail);
                }
                else
                {
                    _cusRepo.Add(customer);
                }

                var lead = this.CreateLeadEntity(data, customer);
                _leadRepo.Add(lead, "WordPress");

                if (_uow.Commit())
                {
                    //RedirectToAction(nameof(MessageController.SendCompanyLeadHooked), nameof(EnumController.Message), new { leadId = lead.Id });
                    await _messageController.SendCompanyLeadHooked(lead.Id, this.Url, this.Request);
                }
                else
                {

                }
            }
            catch (Exception e)
            {
                int x = 0;
                // logging here
            }
        }

        private Customer CreateCustomerEntity(HookVM data)
        {
            return new Customer {
                ContactName = data.ContactName
                , BusinessName = data.BusinessName
                , ContactNumber = data.ContactNumber
                , EMail = data.EMail };
        }

        private Lead CreateLeadEntity(HookVM data, Customer customer)
        {
            return new Lead {
                CustomerId = customer.Id
                , LeadTypeId = data.LeadTypeId
                , Details = this.RipLeadDetails(data.Details)
            };
        }

        private string RipLeadDetails(string details)
        {
            var splitedDetails = Regex.Split(details, @"\--+");

            // if nothing match, always return at least 1 item
            return splitedDetails[0].Replace(":", ":<b>").Replace("\n\n", "</b><br>").Replace("\n", "</b><br>");
        }
    }
}