class LeadAssignmentRatingVM {
    constructor(leadId, leadAssignmentId, comment) {
        this.LeadId = leadId;
        this.LeadAssignmentId = leadAssignmentId;
        this.Comment = comment;
        this.Rate = 0;
        this.CommentedOn = new Date();
        this.CommentedBy = "user";
    }
}