using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class RenamingClasstoPlural : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Action_State_NextStateId",
                table: "Action");

            migrationBuilder.DropForeignKey(
                name: "FK_Agent_Office_OfficeId",
                table: "Agent");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Address_AddressId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItem_Invoice_InvoiceNo",
                table: "InvoiceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItem_LeadAssignment_LeadAssignmentId",
                table: "InvoiceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Lead_Customer_CustomerId",
                table: "Lead");

            migrationBuilder.DropForeignKey(
                name: "FK_Lead_LeadType_LeadTypeId",
                table: "Lead");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadAssignment_Lead_LeadId",
                table: "LeadAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadAssignment_PartnerBranch_PartnerBranchId",
                table: "LeadAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadAssignmentState_LeadAssignment_LeadAssignmentId",
                table: "LeadAssignmentState");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadAssignmentState_State_StateId",
                table: "LeadAssignmentState");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadState_Lead_LeadId",
                table: "LeadState");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadState_State_StateId",
                table: "LeadState");

            migrationBuilder.DropForeignKey(
                name: "FK_Office_Address_AddressId",
                table: "Office");

            migrationBuilder.DropForeignKey(
                name: "FK_Office_Company_CompanyId",
                table: "Office");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerBranch_Address_AddressId",
                table: "PartnerBranch");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerBranch_Partner_PartnerId",
                table: "PartnerBranch");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesPerson_PartnerBranch_BranchId",
                table: "SalesPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_StateAction_Action_ActionId",
                table: "StateAction");

            migrationBuilder.DropForeignKey(
                name: "FK_StateAction_State_StateId",
                table: "StateAction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StateAction",
                table: "StateAction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_State",
                table: "State");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesPerson",
                table: "SalesPerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartnerBranch",
                table: "PartnerBranch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Partner",
                table: "Partner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Office",
                table: "Office");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadType",
                table: "LeadType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadState",
                table: "LeadState");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadAssignment",
                table: "LeadAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lead",
                table: "Lead");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceItem",
                table: "InvoiceItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agent",
                table: "Agent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Action",
                table: "Action");

            migrationBuilder.RenameTable(
                name: "StateAction",
                newName: "StateActions");

            migrationBuilder.RenameTable(
                name: "State",
                newName: "States");

            migrationBuilder.RenameTable(
                name: "SalesPerson",
                newName: "SalesPeople");

            migrationBuilder.RenameTable(
                name: "PartnerBranch",
                newName: "PartnerBranches");

            migrationBuilder.RenameTable(
                name: "Partner",
                newName: "Partners");

            migrationBuilder.RenameTable(
                name: "Office",
                newName: "Offices");

            migrationBuilder.RenameTable(
                name: "LeadType",
                newName: "LeadTypes");

            migrationBuilder.RenameTable(
                name: "LeadState",
                newName: "LeadStates");

            migrationBuilder.RenameTable(
                name: "LeadAssignment",
                newName: "LeadAssignments");

            migrationBuilder.RenameTable(
                name: "Lead",
                newName: "Leads");

            migrationBuilder.RenameTable(
                name: "InvoiceItem",
                newName: "InvoiceItems");

            migrationBuilder.RenameTable(
                name: "Invoice",
                newName: "Invoices");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "Agent",
                newName: "Agents");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameTable(
                name: "Action",
                newName: "Actions");

            migrationBuilder.RenameIndex(
                name: "IX_StateAction_ActionId",
                table: "StateActions",
                newName: "IX_StateActions_ActionId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesPerson_BranchId",
                table: "SalesPeople",
                newName: "IX_SalesPeople_BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_PartnerBranch_PartnerId",
                table: "PartnerBranches",
                newName: "IX_PartnerBranches_PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_PartnerBranch_AddressId",
                table: "PartnerBranches",
                newName: "IX_PartnerBranches_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Office_CompanyId",
                table: "Offices",
                newName: "IX_Offices_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Office_AddressId",
                table: "Offices",
                newName: "IX_Offices_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_LeadState_LeadId",
                table: "LeadStates",
                newName: "IX_LeadStates_LeadId");

            migrationBuilder.RenameIndex(
                name: "IX_LeadAssignment_PartnerBranchId",
                table: "LeadAssignments",
                newName: "IX_LeadAssignments_PartnerBranchId");

            migrationBuilder.RenameIndex(
                name: "IX_LeadAssignment_LeadId",
                table: "LeadAssignments",
                newName: "IX_LeadAssignments_LeadId");

            migrationBuilder.RenameIndex(
                name: "IX_Lead_LeadTypeId",
                table: "Leads",
                newName: "IX_Leads_LeadTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Lead_CustomerId",
                table: "Leads",
                newName: "IX_Leads_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceItem_LeadAssignmentId",
                table: "InvoiceItems",
                newName: "IX_InvoiceItems_LeadAssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_AddressId",
                table: "Customers",
                newName: "IX_Customers_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Agent_OfficeId",
                table: "Agents",
                newName: "IX_Agents_OfficeId");

            migrationBuilder.RenameIndex(
                name: "IX_Action_NextStateId",
                table: "Actions",
                newName: "IX_Actions_NextStateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StateActions",
                table: "StateActions",
                columns: new[] { "StateId", "ActionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_States",
                table: "States",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesPeople",
                table: "SalesPeople",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartnerBranches",
                table: "PartnerBranches",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Partners",
                table: "Partners",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offices",
                table: "Offices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadTypes",
                table: "LeadTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadStates",
                table: "LeadStates",
                columns: new[] { "StateId", "LeadId", "CreatedTimestamp" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadAssignments",
                table: "LeadAssignments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Leads",
                table: "Leads",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceItems",
                table: "InvoiceItems",
                columns: new[] { "InvoiceNo", "LeadAssignmentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices",
                column: "No");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agents",
                table: "Agents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actions",
                table: "Actions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_States_NextStateId",
                table: "Actions",
                column: "NextStateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Offices_OfficeId",
                table: "Agents",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Addresses_AddressId",
                table: "Customers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_Invoices_InvoiceNo",
                table: "InvoiceItems",
                column: "InvoiceNo",
                principalTable: "Invoices",
                principalColumn: "No",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_LeadAssignments_LeadAssignmentId",
                table: "InvoiceItems",
                column: "LeadAssignmentId",
                principalTable: "LeadAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadAssignments_Leads_LeadId",
                table: "LeadAssignments",
                column: "LeadId",
                principalTable: "Leads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadAssignments_PartnerBranches_PartnerBranchId",
                table: "LeadAssignments",
                column: "PartnerBranchId",
                principalTable: "PartnerBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadAssignmentState_LeadAssignments_LeadAssignmentId",
                table: "LeadAssignmentState",
                column: "LeadAssignmentId",
                principalTable: "LeadAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadAssignmentState_States_StateId",
                table: "LeadAssignmentState",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Customers_CustomerId",
                table: "Leads",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_LeadTypes_LeadTypeId",
                table: "Leads",
                column: "LeadTypeId",
                principalTable: "LeadTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadStates_Leads_LeadId",
                table: "LeadStates",
                column: "LeadId",
                principalTable: "Leads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadStates_States_StateId",
                table: "LeadStates",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_Addresses_AddressId",
                table: "Offices",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_Company_CompanyId",
                table: "Offices",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerBranches_Addresses_AddressId",
                table: "PartnerBranches",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerBranches_Partners_PartnerId",
                table: "PartnerBranches",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesPeople_PartnerBranches_BranchId",
                table: "SalesPeople",
                column: "BranchId",
                principalTable: "PartnerBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StateActions_Actions_ActionId",
                table: "StateActions",
                column: "ActionId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StateActions_States_StateId",
                table: "StateActions",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_States_NextStateId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Offices_OfficeId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Addresses_AddressId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_Invoices_InvoiceNo",
                table: "InvoiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_LeadAssignments_LeadAssignmentId",
                table: "InvoiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadAssignments_Leads_LeadId",
                table: "LeadAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadAssignments_PartnerBranches_PartnerBranchId",
                table: "LeadAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadAssignmentState_LeadAssignments_LeadAssignmentId",
                table: "LeadAssignmentState");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadAssignmentState_States_StateId",
                table: "LeadAssignmentState");

            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Customers_CustomerId",
                table: "Leads");

            migrationBuilder.DropForeignKey(
                name: "FK_Leads_LeadTypes_LeadTypeId",
                table: "Leads");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadStates_Leads_LeadId",
                table: "LeadStates");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadStates_States_StateId",
                table: "LeadStates");

            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Addresses_AddressId",
                table: "Offices");

            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Company_CompanyId",
                table: "Offices");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerBranches_Addresses_AddressId",
                table: "PartnerBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_PartnerBranches_Partners_PartnerId",
                table: "PartnerBranches");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesPeople_PartnerBranches_BranchId",
                table: "SalesPeople");

            migrationBuilder.DropForeignKey(
                name: "FK_StateActions_Actions_ActionId",
                table: "StateActions");

            migrationBuilder.DropForeignKey(
                name: "FK_StateActions_States_StateId",
                table: "StateActions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_States",
                table: "States");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StateActions",
                table: "StateActions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesPeople",
                table: "SalesPeople");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Partners",
                table: "Partners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartnerBranches",
                table: "PartnerBranches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offices",
                table: "Offices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadTypes",
                table: "LeadTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadStates",
                table: "LeadStates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Leads",
                table: "Leads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadAssignments",
                table: "LeadAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceItems",
                table: "InvoiceItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agents",
                table: "Agents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actions",
                table: "Actions");

            migrationBuilder.RenameTable(
                name: "States",
                newName: "State");

            migrationBuilder.RenameTable(
                name: "StateActions",
                newName: "StateAction");

            migrationBuilder.RenameTable(
                name: "SalesPeople",
                newName: "SalesPerson");

            migrationBuilder.RenameTable(
                name: "Partners",
                newName: "Partner");

            migrationBuilder.RenameTable(
                name: "PartnerBranches",
                newName: "PartnerBranch");

            migrationBuilder.RenameTable(
                name: "Offices",
                newName: "Office");

            migrationBuilder.RenameTable(
                name: "LeadTypes",
                newName: "LeadType");

            migrationBuilder.RenameTable(
                name: "LeadStates",
                newName: "LeadState");

            migrationBuilder.RenameTable(
                name: "Leads",
                newName: "Lead");

            migrationBuilder.RenameTable(
                name: "LeadAssignments",
                newName: "LeadAssignment");

            migrationBuilder.RenameTable(
                name: "Invoices",
                newName: "Invoice");

            migrationBuilder.RenameTable(
                name: "InvoiceItems",
                newName: "InvoiceItem");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameTable(
                name: "Agents",
                newName: "Agent");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameTable(
                name: "Actions",
                newName: "Action");

            migrationBuilder.RenameIndex(
                name: "IX_StateActions_ActionId",
                table: "StateAction",
                newName: "IX_StateAction_ActionId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesPeople_BranchId",
                table: "SalesPerson",
                newName: "IX_SalesPerson_BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_PartnerBranches_PartnerId",
                table: "PartnerBranch",
                newName: "IX_PartnerBranch_PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_PartnerBranches_AddressId",
                table: "PartnerBranch",
                newName: "IX_PartnerBranch_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Offices_CompanyId",
                table: "Office",
                newName: "IX_Office_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Offices_AddressId",
                table: "Office",
                newName: "IX_Office_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_LeadStates_LeadId",
                table: "LeadState",
                newName: "IX_LeadState_LeadId");

            migrationBuilder.RenameIndex(
                name: "IX_Leads_LeadTypeId",
                table: "Lead",
                newName: "IX_Lead_LeadTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Leads_CustomerId",
                table: "Lead",
                newName: "IX_Lead_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_LeadAssignments_PartnerBranchId",
                table: "LeadAssignment",
                newName: "IX_LeadAssignment_PartnerBranchId");

            migrationBuilder.RenameIndex(
                name: "IX_LeadAssignments_LeadId",
                table: "LeadAssignment",
                newName: "IX_LeadAssignment_LeadId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceItems_LeadAssignmentId",
                table: "InvoiceItem",
                newName: "IX_InvoiceItem_LeadAssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_AddressId",
                table: "Customer",
                newName: "IX_Customer_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Agents_OfficeId",
                table: "Agent",
                newName: "IX_Agent_OfficeId");

            migrationBuilder.RenameIndex(
                name: "IX_Actions_NextStateId",
                table: "Action",
                newName: "IX_Action_NextStateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_State",
                table: "State",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StateAction",
                table: "StateAction",
                columns: new[] { "StateId", "ActionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesPerson",
                table: "SalesPerson",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Partner",
                table: "Partner",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartnerBranch",
                table: "PartnerBranch",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Office",
                table: "Office",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadType",
                table: "LeadType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadState",
                table: "LeadState",
                columns: new[] { "StateId", "LeadId", "CreatedTimestamp" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lead",
                table: "Lead",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadAssignment",
                table: "LeadAssignment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice",
                column: "No");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceItem",
                table: "InvoiceItem",
                columns: new[] { "InvoiceNo", "LeadAssignmentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agent",
                table: "Agent",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Action",
                table: "Action",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Action_State_NextStateId",
                table: "Action",
                column: "NextStateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agent_Office_OfficeId",
                table: "Agent",
                column: "OfficeId",
                principalTable: "Office",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Address_AddressId",
                table: "Customer",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItem_Invoice_InvoiceNo",
                table: "InvoiceItem",
                column: "InvoiceNo",
                principalTable: "Invoice",
                principalColumn: "No",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItem_LeadAssignment_LeadAssignmentId",
                table: "InvoiceItem",
                column: "LeadAssignmentId",
                principalTable: "LeadAssignment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lead_Customer_CustomerId",
                table: "Lead",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lead_LeadType_LeadTypeId",
                table: "Lead",
                column: "LeadTypeId",
                principalTable: "LeadType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadAssignment_Lead_LeadId",
                table: "LeadAssignment",
                column: "LeadId",
                principalTable: "Lead",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadAssignment_PartnerBranch_PartnerBranchId",
                table: "LeadAssignment",
                column: "PartnerBranchId",
                principalTable: "PartnerBranch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadAssignmentState_LeadAssignment_LeadAssignmentId",
                table: "LeadAssignmentState",
                column: "LeadAssignmentId",
                principalTable: "LeadAssignment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadAssignmentState_State_StateId",
                table: "LeadAssignmentState",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadState_Lead_LeadId",
                table: "LeadState",
                column: "LeadId",
                principalTable: "Lead",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadState_State_StateId",
                table: "LeadState",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Office_Address_AddressId",
                table: "Office",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Office_Company_CompanyId",
                table: "Office",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerBranch_Address_AddressId",
                table: "PartnerBranch",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartnerBranch_Partner_PartnerId",
                table: "PartnerBranch",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesPerson_PartnerBranch_BranchId",
                table: "SalesPerson",
                column: "BranchId",
                principalTable: "PartnerBranch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StateAction_Action_ActionId",
                table: "StateAction",
                column: "ActionId",
                principalTable: "Action",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StateAction_State_StateId",
                table: "StateAction",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
