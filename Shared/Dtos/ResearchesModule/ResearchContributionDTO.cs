using Shared.Enums.ResearchesModule;

namespace Shared.Dtos.ResearchesModule
{
    public record ResearchContributionDTO
    {
        public string MemberAcademicName { get; set; } = string.Empty;
        public ContributorType ContributorType { get; set; }
        public bool IsTheMajorResearcher { get; set; }

    }
}
