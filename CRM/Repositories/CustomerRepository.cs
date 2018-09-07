using CRM.Data;
using CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Customer entity)
        {
            // Address can be null because properties in Address are not required.
            // So, must initialize it when they are not entered.
            if (entity.Address == null)
                entity.Address = new Address();

            // *** entity.AddressId will be assigned automatically after the address is added
            //
            _context.Addresses.Add(entity.Address);

            // *** Not necessarily to maully generate field "Id" because EF will do the job
            //
            //entity.Id = Guid.NewGuid();

            _context.Customers.Add(entity); // Id is generated when Add() is called
            //_context.SaveChanges(); // commit by Controller
        }

        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }

        public Customer GetByUid(Guid uid)
        {
            // [P1]
            return _context.Customers.Include(i => i.Address).FirstOrDefault(w => w.Id == uid);
        }

        public Customer GetByEmail(string email)
        {
            // [P1]
            return _context.Customers.Include(i => i.Address).FirstOrDefault(w => w.EMail == email);
        }

        public IEnumerable<Customer> Get()
        {
            return _context.Customers.Include(i => i.Address);
        }

        public void Update(Customer entity)
        {
            //_context.Update(entity.Address); // **MUST FIX => EF insert a new address rather than update the relative address
            // [P1] To solve this, the entity pulled by GetByUid/GetById must include "Address" before update

            _context.Update(entity);
            _context.SaveChanges();
        }

        public void Remove(Customer entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Customer> GetAllIncludeLeads()
        {
            return _context.Customers.Include(lead => lead.Leads).ThenInclude(type => type.LeadType);
        }

        public bool IsCustomerExisted(string email)
        {
            return _context.Customers.FirstOrDefault(w => w.EMail == email) != null ? true : false;
        }
    }
}
