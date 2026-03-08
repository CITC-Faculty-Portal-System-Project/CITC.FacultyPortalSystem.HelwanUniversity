using Shared.Enums.ResearchesModule;

namespace Shared.Dtos.ResearchesModule
{
    public record ResearchContributionResponseDTO
    {
        public int Id { get; set; }
        public string MemberAcademicName { get; set; } = string.Empty;
        public ContributorType ContributorType { get; set; }
        public bool IsTheMajorResearcher { get; set; }
        public int ResearchId { get; set; }
        public Guid? ContributorId { get; set; }


    }
}
