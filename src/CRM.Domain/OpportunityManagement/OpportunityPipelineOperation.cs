namespace CRM.Domain.OpportunityManagement;

public enum OpportunityPipelineOperation
{
    Create = 0,
    Update = 1,
    Progress = 2,
    Win = 3,
    Lose = 4,
    Cancel = 5
}
