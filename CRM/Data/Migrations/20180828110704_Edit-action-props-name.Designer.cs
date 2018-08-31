﻿// <auto-generated />
using System;
using CRM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRM.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180828110704_Edit-action-props-name")]
    partial class Editactionpropsname
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CRM.Models.Action", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActionName");

                    b.Property<string>("ActionTarget");

                    b.Property<string>("ControllerName");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Icon")
                        .HasMaxLength(30);

                    b.Property<int>("NextStateId");

                    b.Property<string>("RequestType");

                    b.HasKey("Id");

                    b.HasIndex("NextStateId");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("CRM.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PostCode")
                        .HasMaxLength(4);

                    b.Property<string>("State")
                        .HasMaxLength(4);

                    b.Property<string>("StreetAddress")
                        .HasMaxLength(256);

                    b.Property<string>("Suburb")
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("CRM.Models.Agent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContactName")
                        .IsRequired();

                    b.Property<string>("ContactNumber")
                        .IsRequired();

                    b.Property<string>("EMail")
                        .IsRequired();

                    b.Property<int>("OfficeId");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.ToTable("Agents");
                });

            modelBuilder.Entity("CRM.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CRM.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ABN")
                        .IsRequired();

                    b.Property<double>("GST");

                    b.Property<string>("Logo");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("CRM.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddressId");

                    b.Property<string>("BusinessName")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("ContactNumber")
                        .IsRequired();

                    b.Property<string>("EMail")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CRM.Models.Invoice", b =>
                {
                    b.Property<int>("No")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<DateTime>("DueDate");

                    b.Property<double>("GST");

                    b.Property<double>("SubTotal");

                    b.Property<double>("Total");

                    b.HasKey("No");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("CRM.Models.InvoiceItem", b =>
                {
                    b.Property<int>("InvoiceNo");

                    b.Property<int>("LeadAssignmentId");

                    b.Property<bool>("Reinvoiced");

                    b.HasKey("InvoiceNo", "LeadAssignmentId");

                    b.HasIndex("LeadAssignmentId");

                    b.ToTable("InvoiceItems");
                });

            modelBuilder.Entity("CRM.Models.Lead", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CustomerId");

                    b.Property<string>("Details")
                        .IsRequired();

                    b.Property<int>("LeadTypeId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("LeadTypeId");

                    b.ToTable("Leads");
                });

            modelBuilder.Entity("CRM.Models.LeadAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<Guid>("LeadId");

                    b.Property<Guid>("PartnerBranchId");

                    b.Property<int>("Rating");

                    b.HasKey("Id");

                    b.HasIndex("LeadId");

                    b.HasIndex("PartnerBranchId");

                    b.ToTable("LeadAssignments");
                });

            modelBuilder.Entity("CRM.Models.LeadAssignmentState", b =>
                {
                    b.Property<int>("StateId");

                    b.Property<int>("LeadAssignmentId");

                    b.Property<DateTime>("ActionTimestamp");

                    b.Property<string>("Action");

                    b.Property<string>("Actor");

                    b.HasKey("StateId", "LeadAssignmentId", "ActionTimestamp");

                    b.HasIndex("LeadAssignmentId");

                    b.ToTable("LeadAssignmentStates");
                });

            modelBuilder.Entity("CRM.Models.LeadState", b =>
                {
                    b.Property<int>("StateId");

                    b.Property<Guid>("LeadId");

                    b.Property<DateTime>("ActionTimestamp");

                    b.Property<string>("Action");

                    b.Property<string>("Actor");

                    b.HasKey("StateId", "LeadId", "ActionTimestamp");

                    b.HasIndex("LeadId");

                    b.ToTable("LeadStates");
                });

            modelBuilder.Entity("CRM.Models.LeadType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Image");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.ToTable("LeadTypes");
                });

            modelBuilder.Entity("CRM.Models.Office", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddressId");

                    b.Property<int>("CompanyId");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("CompanyId");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("CRM.Models.Partner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Logo");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("CRM.Models.PartnerBranch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddressId");

                    b.Property<Guid>("PartnerId");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("PartnerId");

                    b.ToTable("PartnerBranches");
                });

            modelBuilder.Entity("CRM.Models.PartnerService", b =>
                {
                    b.Property<Guid>("PartnerId");

                    b.Property<int>("LeadTypeId");

                    b.HasKey("PartnerId", "LeadTypeId");

                    b.HasIndex("LeadTypeId");

                    b.ToTable("PartnerServices");
                });

            modelBuilder.Entity("CRM.Models.SalesPerson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BranchId");

                    b.Property<string>("ContactName")
                        .IsRequired();

                    b.Property<string>("ContactNumber")
                        .IsRequired();

                    b.Property<string>("EMail")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("SalesPeople");
                });

            modelBuilder.Entity("CRM.Models.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Owner")
                        .IsRequired();

                    b.Property<bool>("Repeatable");

                    b.Property<int>("Seq");

                    b.HasKey("Id");

                    b.ToTable("States");
                });

            modelBuilder.Entity("CRM.Models.StateAction", b =>
                {
                    b.Property<int>("StateId");

                    b.Property<int>("ActionId");

                    b.HasKey("StateId", "ActionId");

                    b.HasIndex("ActionId");

                    b.ToTable("StateActions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CRM.Models.Action", b =>
                {
                    b.HasOne("CRM.Models.State", "NextState")
                        .WithMany("ActionsWithNextSate")
                        .HasForeignKey("NextStateId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CRM.Models.Agent", b =>
                {
                    b.HasOne("CRM.Models.Office", "Office")
                        .WithMany("Agents")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CRM.Models.Customer", b =>
                {
                    b.HasOne("CRM.Models.Address", "Address")
                        .WithOne("Customer")
                        .HasForeignKey("CRM.Models.Customer", "AddressId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CRM.Models.InvoiceItem", b =>
                {
                    b.HasOne("CRM.Models.Invoice", "Invoice")
                        .WithMany("InvoiceItems")
                        .HasForeignKey("InvoiceNo")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CRM.Models.LeadAssignment", "LeadAssignment")
                        .WithMany("InvoiceItems")
                        .HasForeignKey("LeadAssignmentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CRM.Models.Lead", b =>
                {
                    b.HasOne("CRM.Models.Customer", "Customer")
                        .WithMany("Leads")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRM.Models.LeadType", "LeadType")
                        .WithMany("Leads")
                        .HasForeignKey("LeadTypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CRM.Models.LeadAssignment", b =>
                {
                    b.HasOne("CRM.Models.Lead", "Lead")
                        .WithMany("LeadAssignments")
                        .HasForeignKey("LeadId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CRM.Models.PartnerBranch", "PartnerBranch")
                        .WithMany("LeadAssignments")
                        .HasForeignKey("PartnerBranchId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CRM.Models.LeadAssignmentState", b =>
                {
                    b.HasOne("CRM.Models.LeadAssignment", "LeadAssignment")
                        .WithMany("LeadAssignmentStates")
                        .HasForeignKey("LeadAssignmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRM.Models.State", "State")
                        .WithMany("LeadAssignmentStates")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CRM.Models.LeadState", b =>
                {
                    b.HasOne("CRM.Models.Lead", "Lead")
                        .WithMany("LeadStates")
                        .HasForeignKey("LeadId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRM.Models.State", "State")
                        .WithMany("LeadStates")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CRM.Models.Office", b =>
                {
                    b.HasOne("CRM.Models.Address", "Address")
                        .WithOne("Office")
                        .HasForeignKey("CRM.Models.Office", "AddressId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CRM.Models.Company", "Company")
                        .WithMany("Offices")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CRM.Models.PartnerBranch", b =>
                {
                    b.HasOne("CRM.Models.Address", "Address")
                        .WithOne("PartnerBranch")
                        .HasForeignKey("CRM.Models.PartnerBranch", "AddressId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CRM.Models.Partner", "Partner")
                        .WithMany("Branches")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CRM.Models.PartnerService", b =>
                {
                    b.HasOne("CRM.Models.LeadType", "LeadType")
                        .WithMany("PartnerServices")
                        .HasForeignKey("LeadTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CRM.Models.Partner", "Partner")
                        .WithMany("PartnerServices")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CRM.Models.SalesPerson", b =>
                {
                    b.HasOne("CRM.Models.PartnerBranch", "Branch")
                        .WithMany("SalesPeople")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CRM.Models.StateAction", b =>
                {
                    b.HasOne("CRM.Models.Action", "Action")
                        .WithMany("StateActions")
                        .HasForeignKey("ActionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CRM.Models.State", "State")
                        .WithMany("StateActions")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CRM.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CRM.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CRM.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CRM.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
