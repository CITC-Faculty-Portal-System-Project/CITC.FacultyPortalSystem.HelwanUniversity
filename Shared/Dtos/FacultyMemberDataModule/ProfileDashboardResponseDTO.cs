namespace Shared.Dtos.FacultyMemberDataModule
{
    public record ProfileDashboardResponseDTO
    {
        //Data from PersonalData
        public Guid? ProfilePictureId { get; set; }
        public int PersonalDataId { get; set; }
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public LookupItemDto Title { get; set; } = null!;
        public LookupItemDto University { get; set; } = null!;
        public LookupItemDto Department { get; set; } = null!;
        public string? BioSummary { get; set; }
        public List <string>? Skills { get; set; }


        //Data from SocialMediaLinks
        public string? LinkedIn { get; set; }
        public string? Instagram { get; set; }
        public string? PersonalWebsite { get; set; }
        public string? GoogleScholar { get; set; }
        public string? Scopus { get; set; }
        public string? Facebook { get; set; }
        public string? X { get; set; }
        public string? YouTube { get; set; }

        //Counts
        public int ResearchCount { get; set; }
        public int PrizesAndRewardsCount { get; set; }
        public int ScientificWritingsCount { get; set; }
        public int ProjectsCount { get; set; }
        public int ContributionsCount { get; set; }

        public List<ExperiencesSummaryDTO> TopExperiences { get; set; } = new();
        public List<AcademicQualificationsSummaryDTO> TopAcademicQualifications { get; set; } = new();

    }
}
