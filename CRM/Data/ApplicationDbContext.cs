using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CRM.Models;

namespace CRM.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Models.Action> Actions { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<LeadAssignment> LeadAssignments { get; set; }
        public DbSet<LeadState> LeadStates { get; set; }
        public DbSet<LeadType> LeadTypes { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<PartnerBranch> PartnerBranches { get; set; }
        public DbSet<SalesPerson> SalesPeople { get; set; }
        public DbSet<Models.State> States { get; set; }
        public DbSet<StateAction> StateActions { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // Customer -> Leads <- LeadType
            // Many-to-Many Relationship
            //
            builder.Entity<Lead>()
                .HasOne(lead => lead.Customer)
                .WithMany(customer => customer.Leads)
                .HasForeignKey(lead => lead.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Lead>()
                .HasOne(lead => lead.LeadType)
                .WithMany(leadType => leadType.Leads)
                .HasForeignKey(lead => lead.LeadTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // ** No longer used
            // TransactionType - Self-Recursive Relationship for the next transaction
            // One-to-One Relationship
            //
            //builder.Entity<TransactionType>()
            //    .HasOne(current => current.NextTransactionType)
            //    .WithOne()
            //    .HasForeignKey<TransactionType>(current => current.NextTransactionTypeId)
            //    .OnDelete(DeleteBehavior.Restrict);

            // Removing to use M-M instead between State & Lead so that transactions can be tracked for the history
            // Lead -> Transaction <- TransactionType : Many-to-Many Relationship
            //
            //builder.Entity<Transaction>()
            //    .HasOne(tran => tran.Lead)
            //    .WithMany(lead => lead.Transactions)
            //    .HasForeignKey(tran => tran.LeadId)
            //    .OnDelete(DeleteBehavior.Cascade);
            //builder.Entity<Transaction>()
            //    .HasOne(tran => tran.TransactionType)
            //    .WithMany(type => type.Transactions)
            //    .HasForeignKey(tran => tran.TransactionTypeId)
            //    .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<LeadState>().HasKey(key => new { key.StateId, key.LeadId, key.CreatedTimestamp });
            builder.Entity<LeadState>()
                .HasOne(ls => ls.State)
                .WithMany(s => s.LeadStates)
                .HasForeignKey(ls => ls.StateId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<LeadState>()
                .HasOne(ls => ls.Lead)
                .WithMany(l => l.LeadStates)
                .HasForeignKey(ls => ls.LeadId);

            // Company 1-M Office
            //
            builder.Entity<Office>()
                .HasOne(o => o.Company)
                .WithMany(c => c.Offices)
                .HasForeignKey(o => o.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Agent M-1 Office
            //
            builder.Entity<Agent>()
                .HasOne(agent => agent.Office)
                .WithMany(office => office.Agents)
                .HasForeignKey(agent => agent.OfficeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Partner 1-M PartnerBranch
            builder.Entity<PartnerBranch>()
                .HasOne(branch => branch.Partner)
                .WithMany(partner => partner.Branches)
                .HasForeignKey(branch => branch.PartnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // PartnerBranch 1-M SalesPerson
            builder.Entity<SalesPerson>()
                .HasOne(s => s.Branch)
                .WithMany(b => b.SalesPeople)
                .HasForeignKey(s => s.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // State 1-M StateAction M-1 Action
            builder.Entity<StateAction>().HasKey(key => new { key.StateId, key.ActionId });
            builder.Entity<StateAction>()
                .HasOne(sa => sa.State)
                .WithMany(s => s.StateActions)
                .HasForeignKey(sa => sa.StateId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<StateAction>()
                .HasOne(sa => sa.Action)
                .WithMany(a => a.StateActions)
                .HasForeignKey(sa => sa.ActionId)
                .OnDelete(DeleteBehavior.Restrict);

            // ** Removing to use M-M instead between State & LeadAssignment so that transactions can be tracked for the history
            // LeadAssignment M-1 State
            //builder.Entity<LeadAssignment>()
            //    .HasOne(la => la.State)
            //    .WithMany(s => s.LeadAssignments)
            //    .HasForeignKey(la => la.StateId)
            //    .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<LeadAssignmentState>().HasKey(key => new { key.StateId, key.LeadAssignmentId, key.CreatedTimestamp });
            builder.Entity<LeadAssignmentState>()
                .HasOne(las => las.State)
                .WithMany(s => s.LeadAssignmentStates)
                .HasForeignKey(las => las.StateId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<LeadAssignmentState>()
                .HasOne(las => las.LeadAssignment)
                .WithMany(la => la.LeadAssignmentStates)
                .HasForeignKey(las => las.LeadAssignmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Lead 1-M LeadAssignment M-1 PartnerBranch
            builder.Entity<LeadAssignment>()
                .HasOne(la => la.Lead)
                .WithMany(l => l.LeadAssignments)
                .HasForeignKey(la => la.LeadId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<LeadAssignment>()
                .HasOne(la => la.PartnerBranch)
                .WithMany(p => p.LeadAssignments)
                .HasForeignKey(la => la.PartnerBranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // Address 1-1 Office
            builder.Entity<Address>()
                .HasOne(a => a.Office)
                .WithOne(o => o.Address)
                .HasForeignKey<Office>(o => o.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            // Address 1-1 PartnerBranch
            builder.Entity<Address>()
                .HasOne(a => a.PartnerBranch)
                .WithOne(p => p.Address)
                .HasForeignKey<PartnerBranch>(p => p.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            // Address 1-1 Customer
            builder.Entity<Address>()
                .HasOne(a => a.Customer)
                .WithOne(c => c.Address)
                .HasForeignKey<Customer>(c => c.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            // Action M-1 NextState
            //
            builder.Entity<Models.Action>()
                .HasOne(a => a.NextState)
                .WithMany(ns => ns.ActionsWithNextSate)
                .HasForeignKey(a => a.NextStateId)
                .OnDelete(DeleteBehavior.Restrict);

            // Invoice 1-M InvoiceItem M-1 LeadAssignment
            //
            builder.Entity<InvoiceItem>().HasKey(key => new { key.InvoiceNo, key.LeadAssignmentId });
            builder.Entity<InvoiceItem>()
                .HasOne(item => item.Invoice)
                .WithMany(i => i.InvoiceItems)
                .HasForeignKey(item => item.InvoiceNo)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<InvoiceItem>()
                .HasOne(item => item.LeadAssignment)
                .WithMany(la => la.InvoiceItems)
                .HasForeignKey(item => item.LeadAssignmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
