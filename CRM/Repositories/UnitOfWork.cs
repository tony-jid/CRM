using CRM.Data;
using CRM.Enum;
using CRM.Extensions;
using CRM.Helpers;
using CRM.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                throw dbUpdateEx;
                return false;
            }
            catch (Exception ex)
            {
                // Environment: Development => simply throw an exception
                //              Production => returning "false" and writing log
                throw ex;
                return false;
            }
        }

        public bool Commit(ModelStateDictionary modelState)
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException dbUpdateEx)
            {
                // logging

                string errorMessage = dbUpdateEx?.InnerException.Message ?? "An error occurred while updating the data";
                if (errorMessage.Contains(EnumErrorMessageHints.REFERENCE_CONSTRAINT.GetDesc()))
                    ErrorMessageHelper.AddModelStateError(modelState, EnumErrorMessageHints.REFERENCE_CONSTRAINT);
                else
                    ErrorMessageHelper.AddModelStateError(modelState, EnumErrorMessageDescriptions.EXCEPTION);

                return false;
            }
            catch (Exception ex)
            {
                // logging

                ErrorMessageHelper.AddModelStateError(modelState, EnumErrorMessageDescriptions.EXCEPTION);
                return false;
            }
        }

        private IMessageRepository _msgRepo;
        public IMessageRepository MessageRepository
        {
            get
            {
                if (_msgRepo == null)
                    _msgRepo = new MessageRepository(_context);
                return _msgRepo;
            }
        }

        private ICompanyRepository _companyRepo;
        public ICompanyRepository CompanyRepository {
            get {
                if (_companyRepo == null)
                    _companyRepo = new CompanyRepository(_context);
                return _companyRepo;
            }
        }

        private IOfficeRepository _officeRepo;

        public IOfficeRepository OfficeRepository
        {
            get
            {
                if (_officeRepo == null)
                    _officeRepo = new OfficeRepository(_context);
                return _officeRepo;
            }
        }


        private IAgentRepository _agentRepo;

        public IAgentRepository AgentRepository
        {
            get
            {
                if (_agentRepo == null)
                    _agentRepo = new AgentRepository(_context);
                return _agentRepo;
            }
        }

        private LeadRepository _leadRepo;
        public LeadRepository LeadRepository {
            get {
                if (_leadRepo == null)
                    _leadRepo = new LeadRepository(_context);
                return _leadRepo;
            }
        }

        private LeadAssignmentRepository _leadAssRepo;
        public LeadAssignmentRepository LeadAssignmentRepository
        {
            get
            {
                if (_leadAssRepo == null)
                    _leadAssRepo = new LeadAssignmentRepository(_context);
                return _leadAssRepo;
            }
        }

        private ILeadTypeRepository _leadTypeRepo;
        public ILeadTypeRepository LeadTypeRepository
        {
            get
            {
                if (_leadTypeRepo == null)
                    _leadTypeRepo = new LeadTypeRepository(_context);
                return _leadTypeRepo;
            }
        }

        private ICustomerRepository _cusRepo;

        public ICustomerRepository CustomerRepository
        {
            get {
                if (_cusRepo == null)
                    _cusRepo = new CustomerRepository(_context);
                return _cusRepo;
            }
        }

        private IPartnerRepository _partnerRepo;

        public IPartnerRepository PartnerRepository
        {
            get
            {
                if (_partnerRepo == null)
                    _partnerRepo = new PartnerRepository(_context);
                return _partnerRepo;
            }
        }

        private IPartnerBranchRepository _partnerBranchRepo;
        public IPartnerBranchRepository PartnerBranchRepository
        {
            get
            {
                if (_partnerBranchRepo == null)
                    _partnerBranchRepo = new PartnerBranchRepository(_context);
                return _partnerBranchRepo;
            }
        }
        

        private ISalesPersonRepository _salesPersonRepo;

        public ISalesPersonRepository SalesPersonRepository
        {
            get
            {
                if (_salesPersonRepo == null)
                    _salesPersonRepo = new SalesPersonRepository(_context);
                return _salesPersonRepo;
            }
        }

        private ActionRepository _actionRepositoryRepo;
        public ActionRepository ActionRepository
        {
            get
            {
                if (_actionRepositoryRepo == null)
                    _actionRepositoryRepo = new ActionRepository(_context);
                return _actionRepositoryRepo;
            }
        }

        private ReportRepository _reportRepository;
        public ReportRepository ReportRepository
        {
            get
            {
                if (_reportRepository == null)
                    _reportRepository = new ReportRepository(_context);
                return _reportRepository;
            }
        }
    }
}
