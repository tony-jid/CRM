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
        public DbSet<LeadAssignmentState> LeadAssignmentStates { get; set; }
        public DbSet<LeadState> LeadStates { get; set; }
        public DbSet<LeadType> LeadTypes { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<PartnerBranch> PartnerBranches { get; set; }
        public DbSet<PartnerService> PartnerServices { get; set; }
        public DbSet<SalesPerson> SalesPeople { get; set; }
        public DbSet<Models.State> States { get; set; }
        public DbSet<StateAction> StateActions { get; set; }
        public DbSet<MessageTemplate> MessageTemplates { get; set; }

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

            builder.Entity<Company>().HasData( new Company { Id = 1, Name = "Comparison Advantage", ABN = "65 626 309 073", GST = 10, Logo = "logo-dark.png" });

            // State data
            builder.Entity<State>().HasData(
                    new State { Id = "S0", Owner = "Unknown", Seq = 1, Name = "Unknown" },
                    new State { Id = "SL1", Owner = "Lead", Seq = 1, Name = "New" },
                    new State { Id = "SL2", Owner = "Lead", Seq = 2, Name = "Assigned" },
                    new State { Id = "SL3", Owner = "Lead", Seq = 3, Name = "Re-assigned" },
                    new State { Id = "SLA1", Owner = "LeadAssignment", Seq = 1, Name = "Considering" },
                    new State { Id = "SLA2", Owner = "LeadAssignment", Seq = 1, Name = "Accepted" },
                    new State { Id = "SLA3", Owner = "LeadAssignment", Seq = 1, Name = "Rejected" },
                    new State { Id = "SLA4", Owner = "LeadAssignment", Seq = 1, Name = "Invoiced" },
                    new State { Id = "SLA5", Owner = "LeadAssignment", Seq = 1, Name = "Re-invoiced" }
                );
            // Action data
            builder.Entity<Models.Action>().HasData(
                    new Models.Action { Id = "AL1", ControllerName = "Message", ActionName = "Send", NextStateId = "S0", Icon = "batch-icon batch-icon-envelope", DisplayName = "Send message", ActionTarget = "Message", RequestType = "Post" },
                    new Models.Action { Id = "AL2", ControllerName = "Leads", ActionName = "Assignments", NextStateId = "SL2", Icon = "batch-icon batch-icon-user-alt-2", DisplayName = "Assign partners", ActionTarget = "Window", RequestType = "Get" },
                    new Models.Action { Id = "AL3", ControllerName = "Leads", ActionName = "Assignments", NextStateId = "SL3", Icon = "batch-icon batch-icon-user-alt-2", DisplayName = "Re-assign partners", ActionTarget = "Window", RequestType = "Get" },
                    new Models.Action { Id = "ALA1", ControllerName = "LeadAssignments", ActionName = "Comment", NextStateId = "S0", Icon = "batch-icon batch-icon-speech-bubble-left-tip-text", DisplayName = "Comment lead", ActionTarget = "Message", RequestType = "Post" },
                    new Models.Action { Id = "ALA2", ControllerName = "LeadAssignments", ActionName = "Accept", NextStateId = "SLA2", Icon = "batch-icon batch-icon-tick", DisplayName = "Accept lead", ActionTarget = "Ajax", RequestType = "Put" },
                    new Models.Action { Id = "ALA3", ControllerName = "LeadAssignments", ActionName = "Reject", NextStateId = "SLA3", Icon = "batch-icon batch-icon-cross", DisplayName = "Reject lead", ActionTarget = "Ajax", RequestType = "Put" },
                    new Models.Action { Id = "ALA4", ControllerName = "LeadAssignments", ActionName = "SendInvoice", NextStateId = "SLA4", Icon = "fa fa-dollar", DisplayName = "Send invoice", ActionTarget = "Ajax", RequestType = "Post" },
                    new Models.Action { Id = "ALA5", ControllerName = "LeadAssignments", ActionName = "ResendInvoice", NextStateId = "SLA5", Icon = "fa fa-dollar", DisplayName = "Re-send invoice", ActionTarget = "Ajax", RequestType = "Post" }
                );
            // State-Action data
            builder.Entity<StateAction>().HasData(
                    new StateAction { StateId = "SL1", ActionId = "AL1" },
                    new StateAction { StateId = "SL1", ActionId = "AL2" },
                    new StateAction { StateId = "SL2", ActionId = "AL1" },
                    new StateAction { StateId = "SL2", ActionId = "AL3" },
                    new StateAction { StateId = "SL3", ActionId = "AL1" },
                    new StateAction { StateId = "SL3", ActionId = "AL3" },
                    new StateAction { StateId = "SLA1", ActionId = "AL1" },
                    new StateAction { StateId = "SLA1", ActionId = "ALA1" },
                    new StateAction { StateId = "SLA1", ActionId = "ALA2" },
                    new StateAction { StateId = "SLA1", ActionId = "ALA3" }
                );

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

            builder.Entity<LeadState>().HasKey(key => new { key.StateId, key.LeadId, key.ActionTimestamp });
            builder.Entity<LeadState>()
                .HasOne(ls => ls.State)
                .WithMany(s => s.LeadStates)
                .HasForeignKey(ls => ls.StateId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<LeadState>()
                .HasOne(ls => ls.Lead)
                .WithMany(l => l.LeadStates)
                .HasForeignKey(ls => ls.LeadId)
                .OnDelete(DeleteBehavior.Cascade);

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
            builder.Entity<LeadAssignmentState>().HasKey(key => new { key.StateId, key.LeadAssignmentId, key.ActionTimestamp });
            builder.Entity<LeadAssignmentState>()
                .HasOne(las => las.State)
                .WithMany(s => s.LeadAssignmentStates)
                .HasForeignKey(las => las.StateId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<LeadAssignmentState>()
                .HasOne(las => las.LeadAssignment)
                .WithMany(la => la.LeadAssignmentStates)
                .HasForeignKey(las => las.LeadAssignmentId)
                .OnDelete(DeleteBehavior.Cascade);

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

            // Partner 1-M PartnerService M-1 LeadType
            //
            builder.Entity<PartnerService>().HasKey(key => new { key.PartnerId, key.LeadTypeId });
            builder.Entity<PartnerService>()
                .HasOne(service => service.Partner)
                .WithMany(partner => partner.PartnerServices)
                .HasForeignKey(service => service.PartnerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<PartnerService>()
                .HasOne(service => service.LeadType)
                .WithMany(type => type.PartnerServices)
                .HasForeignKey(service => service.LeadTypeId)
                .OnDelete(DeleteBehavior.Restrict);


            // [Start] Identity Service Relationship
            //
            // ApplicationUser 1-1 Agent
            builder.Entity<ApplicationUser>()
                .HasOne(user => user.Agent)
                .WithOne(agent => agent.ApplicationUser)
                .HasForeignKey<Agent>(agent => agent.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            // ApplicationUser 1-1 SalePerson
            builder.Entity<ApplicationUser>()
                .HasOne(user => user.SalesPerson)
                .WithOne(agent => agent.ApplicationUser)
                .HasForeignKey<SalesPerson>(salesPerson => salesPerson.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            //
            // [End] Identity Service Relationship
        }
    }
}
