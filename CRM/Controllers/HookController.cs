using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        private ILeadRepository _leadRepo;
        private ICustomerRepository _cusRepo;

        public HookController(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _leadRepo = unitOfWork.LeadRepository;
            _cusRepo = unitOfWork.CustomerRepository;
        }

        [HttpPost]
        public IActionResult HookLead([FromBody]HookVM data)
        {
            // Gravity Forms separates form fields with "----------" by considering a section field that come right after a page break
            // So, we can separate lead details by using "--------------------------------"
            //

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
            _leadRepo.Add(lead);

            return _uow.Commit() ? Ok() : StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
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