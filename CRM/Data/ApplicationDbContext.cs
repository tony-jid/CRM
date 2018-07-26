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

            // TransactionType - Self-Recursive Relationship for the next transaction
            // One-to-One Relationship
            //
            builder.Entity<TransactionType>()
                .HasOne(current => current.NextTransactionType)
                .WithOne()
                .HasForeignKey<TransactionType>(current => current.NextTransactionTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Lead -> Transaction <- TransactionType
            // Many-to-Many Relationship
            //
            builder.Entity<Transaction>()
                .HasOne(tran => tran.Lead)
                .WithMany(lead => lead.Transactions)
                .HasForeignKey(tran => tran.LeadId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Transaction>()
                .HasOne(tran => tran.TransactionType)
                .WithMany(type => type.Transactions)
                .HasForeignKey(tran => tran.TransactionTypeId)
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

            // LeadAssignment M-1 State
            builder.Entity<LeadAssignment>()
                .HasOne(la => la.State)
                .WithMany(s => s.LeadAssignments)
                .HasForeignKey(la => la.StateId)
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

        }
    }
}
