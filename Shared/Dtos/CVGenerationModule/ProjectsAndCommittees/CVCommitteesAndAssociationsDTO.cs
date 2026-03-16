namespace Shared.Dtos.CVGenerationModule.ProjectsAndCommittees
{
    public record CVCommitteesAndAssociationsDTO
    {
        public int Id { get; set; }
        public string? NameOfCommitteeOrAssociation { get; set; } 
        public LookupItemDto? TypeOfCommitteeOrAssociation { get; set; } 
        public LookupItemDto? DegreeOfSubscription { get; set; } 
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
