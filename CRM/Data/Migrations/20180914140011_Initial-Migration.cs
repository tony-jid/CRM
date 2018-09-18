using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StreetAddress = table.Column<string>(maxLength: 256, nullable: true),
                    Suburb = table.Column<string>(maxLength: 64, nullable: true),
                    State = table.Column<string>(maxLength: 4, nullable: true),
                    PostCode = table.Column<string>(maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    ABN = table.Column<string>(nullable: false),
                    GST = table.Column<double>(nullable: false),
                    Logo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    No = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: false),
                    SubTotal = table.Column<double>(nullable: false),
                    GST = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.No);
                });

            migrationBuilder.CreateTable(
                name: "LeadTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Logo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 5, nullable: false),
                    Owner = table.Column<string>(nullable: false),
                    Seq = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Repeatable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContactName = table.Column<string>(maxLength: 256, nullable: false),
                    ContactNumber = table.Column<string>(nullable: false),
                    EMail = table.Column<string>(nullable: false),
                    BusinessName = table.Column<string>(maxLength: 256, nullable: true),
                    AddressId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offices_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offices_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartnerBranches",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AddressId = table.Column<int>(nullable: false),
                    PartnerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerBranches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartnerBranches_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartnerBranches_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartnerServices",
                columns: table => new
                {
                    PartnerId = table.Column<Guid>(nullable: false),
                    LeadTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerServices", x => new { x.PartnerId, x.LeadTypeId });
                    table.ForeignKey(
                        name: "FK_PartnerServices_LeadTypes_LeadTypeId",
                        column: x => x.LeadTypeId,
                        principalTable: "LeadTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartnerServices_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 5, nullable: false),
                    ControllerName = table.Column<string>(nullable: true),
                    ActionName = table.Column<string>(nullable: true),
                    ActionTarget = table.Column<string>(nullable: true),
                    RequestType = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(maxLength: 30, nullable: true),
                    NextStateId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actions_States_NextStateId",
                        column: x => x.NextStateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Leads",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Details = table.Column<string>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    LeadTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leads_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leads_LeadTypes_LeadTypeId",
                        column: x => x.LeadTypeId,
                        principalTable: "LeadTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContactName = table.Column<string>(nullable: false),
                    ContactNumber = table.Column<string>(nullable: false),
                    EMail = table.Column<string>(nullable: false),
                    OfficeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agents_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesPeople",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContactName = table.Column<string>(nullable: false),
                    ContactNumber = table.Column<string>(nullable: false),
                    EMail = table.Column<string>(nullable: false),
                    BranchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesPeople", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesPeople_PartnerBranches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "PartnerBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StateActions",
                columns: table => new
                {
                    StateId = table.Column<string>(nullable: false),
                    ActionId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateActions", x => new { x.StateId, x.ActionId });
                    table.ForeignKey(
                        name: "FK_StateActions_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StateActions_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeadAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Rating = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    LeadId = table.Column<Guid>(nullable: false),
                    PartnerBranchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadAssignments_Leads_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Leads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeadAssignments_PartnerBranches_PartnerBranchId",
                        column: x => x.PartnerBranchId,
                        principalTable: "PartnerBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeadStates",
                columns: table => new
                {
                    LeadId = table.Column<Guid>(nullable: false),
                    StateId = table.Column<string>(nullable: false),
                    Actor = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    ActionTimestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadStates", x => new { x.StateId, x.LeadId, x.ActionTimestamp });
                    table.ForeignKey(
                        name: "FK_LeadStates_Leads_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Leads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeadStates_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    InvoiceNo = table.Column<int>(nullable: false),
                    LeadAssignmentId = table.Column<int>(nullable: false),
                    Reinvoiced = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => new { x.InvoiceNo, x.LeadAssignmentId });
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceNo",
                        column: x => x.InvoiceNo,
                        principalTable: "Invoices",
                        principalColumn: "No",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_LeadAssignments_LeadAssignmentId",
                        column: x => x.LeadAssignmentId,
                        principalTable: "LeadAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeadAssignmentStates",
                columns: table => new
                {
                    LeadAssignmentId = table.Column<int>(nullable: false),
                    StateId = table.Column<string>(nullable: false),
                    Actor = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    ActionTimestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadAssignmentStates", x => new { x.StateId, x.LeadAssignmentId, x.ActionTimestamp });
                    table.ForeignKey(
                        name: "FK_LeadAssignmentStates_LeadAssignments_LeadAssignmentId",
                        column: x => x.LeadAssignmentId,
                        principalTable: "LeadAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeadAssignmentStates_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "ABN", "GST", "Logo", "Name" },
                values: new object[] { 1, "65 626 309 073", 10.0, "logo-dark.png", "Comparison Advantage" });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "Name", "Owner", "Repeatable", "Seq" },
                values: new object[,]
                {
                    { "S0", "Unknown", "Unknown", false, 1 },
                    { "SL1", "New", "Lead", false, 1 },
                    { "SL2", "Assigned", "Lead", false, 2 },
                    { "SL3", "Re-assigned", "Lead", false, 3 },
                    { "SLA1", "Considering", "LeadAssignment", false, 1 },
                    { "SLA2", "Accepted", "LeadAssignment", false, 1 },
                    { "SLA3", "Rejected", "LeadAssignment", false, 1 },
                    { "SLA4", "Invoiced", "LeadAssignment", false, 1 },
                    { "SLA5", "Re-invoiced", "LeadAssignment", false, 1 }
                });

            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "Id", "ActionName", "ActionTarget", "ControllerName", "DisplayName", "Icon", "NextStateId", "RequestType" },
                values: new object[,]
                {
                    { "AL1", "Send", "Message", "Message", "Send message", "envelope", "S0", "Post" },
                    { "ALA1", "Comment", "Message", "LeadAssigments", "Comment lead", "", "S0", "Post" },
                    { "AL2", "Assignments", "Window", "Leads", "Assign partners", "user-alt-2", "SL2", "Get" },
                    { "AL3", "Assignments", "Window", "Leads", "Re-assign partners", "user-alt-2", "SL3", "Get" },
                    { "ALA2", "Accept", "Ajax", "LeadAssigments", "Accept lead", "", "SLA2", "Put" },
                    { "ALA3", "Reject", "Ajax", "LeadAssigments", "Reject lead", "", "SLA3", "Put" },
                    { "ALA4", "SendInvoice", "Ajax", "LeadAssigments", "Send invoice", "", "SLA4", "Post" },
                    { "ALA5", "ResendInvoice", "Ajax", "LeadAssigments", "Re-send invoice", "", "SLA5", "Post" }
                });

            migrationBuilder.InsertData(
                table: "StateActions",
                columns: new[] { "StateId", "ActionId" },
                values: new object[,]
                {
                    { "SL1", "AL1" },
                    { "SL2", "AL1" },
                    { "SL3", "AL1" },
                    { "SL1", "AL2" },
                    { "SL2", "AL3" },
                    { "SL3", "AL3" }
                });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_NextStateId",
                table: "Actions",
                column: "NextStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_OfficeId",
                table: "Agents",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressId",
                table: "Customers",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_LeadAssignmentId",
                table: "InvoiceItems",
                column: "LeadAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadAssignments_LeadId",
                table: "LeadAssignments",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadAssignments_PartnerBranchId",
                table: "LeadAssignments",
                column: "PartnerBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadAssignmentStates_LeadAssignmentId",
                table: "LeadAssignmentStates",
                column: "LeadAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_CustomerId",
                table: "Leads",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_LeadTypeId",
                table: "Leads",
                column: "LeadTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadStates_LeadId",
                table: "LeadStates",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_AddressId",
                table: "Offices",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offices_CompanyId",
                table: "Offices",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerBranches_AddressId",
                table: "PartnerBranches",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartnerBranches_PartnerId",
                table: "PartnerBranches",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerServices_LeadTypeId",
                table: "PartnerServices",
                column: "LeadTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesPeople_BranchId",
                table: "SalesPeople",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_StateActions_ActionId",
                table: "StateActions",
                column: "ActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "LeadAssignmentStates");

            migrationBuilder.DropTable(
                name: "LeadStates");

            migrationBuilder.DropTable(
                name: "PartnerServices");

            migrationBuilder.DropTable(
                name: "SalesPeople");

            migrationBuilder.DropTable(
                name: "StateActions");

            migrationBuilder.DropTable(
                name: "Offices");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "LeadAssignments");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Leads");

            migrationBuilder.DropTable(
                name: "PartnerBranches");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "LeadTypes");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
