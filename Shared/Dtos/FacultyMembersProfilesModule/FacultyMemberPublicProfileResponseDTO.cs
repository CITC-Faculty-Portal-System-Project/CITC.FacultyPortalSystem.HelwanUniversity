using Shared.Dtos.AcademicDataModule.MissionsModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.AttachmentsModule;
using Shared.Dtos.DataFetchingFromExternalService;
using Shared.Dtos.FacultyMemberDataModule;
using Shared.Dtos.ResearchesModule;
using Shared.Enums.IdentityModule.SpecificationEnums;

namespace Shared.Dtos.FacultyMembersProfilesModule
{
    public record FacultyMemberPublicProfileResponseDTO
    {
        public Guid Id { get; set; }
        public string FacultyMemberName { get; set; } = string.Empty;
        public string FacultyMemberEmail { get; set; } = string.Empty;
        public int PersonalDataId { get; set; }
        public IEnumerable<ExternalResearcherInterestsFetchingDTO>? Interests { get; set; }
        public string BioSummary { get; set; } = string.Empty;
        public string RegisterationId { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public AttachmentResponseDTO? ProfilePicture { get; set; }
        public DateTime IssueDate { get; set; }
        public string System { get; set; } = string.Empty;
        public string PreferredCv { get; set; } = string.Empty;
        public IEnumerable<ScientificMissionResponseDto>? ScientificMissions { get; set; }
        public IEnumerable<ResearchResponseDTO>? Researches { get; set; }
        public IEnumerable<ExperiencesSummaryDTO>? Experinces { get; set; }

    }
}
