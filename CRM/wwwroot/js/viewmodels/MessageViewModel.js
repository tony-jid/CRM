class MessageViewModel {
    constructor(recipients, subject, message, customerId, leadId, leadAssignmentId) {
        this.Recipients = recipients
        this.Subject = subject;
        this.Message = message;
        this.CustomerId = customerId;
        this.LeadId = leadId;
        this.LeadAssignmentId = leadAssignmentId;
    }
}