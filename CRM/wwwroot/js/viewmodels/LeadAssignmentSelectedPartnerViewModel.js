class LeadAssignmentSelectedPartnerViewModel {
    constructor(leadId) {
        this.LeadId = leadId;
        this.PartnerBranchIds = [];
    }

    addPartner(branchId) {
        this.PartnerBranchIds.push(branchId);
    }
}