namespace Shared.Models.CVGenerationModule
{
    public class PersonalDataVisibility
    {
        public bool ShowPersonalData { get; set; } = true;
        public bool ShowPersonalDataForPublic { get; set; } = true;
        public bool ShowUniversity { get; set; } = true;
        public bool ShowUniversityForPublic { get; set; } = true;
        public bool ShowAuthority { get; set; } = true;
        public bool ShowAuthorityForPublic { get; set; } = true;
        public bool ShowDepartment { get; set; } = true;
        public bool ShowDepartmentForPublic { get; set; } = true;
        public bool ShowBirthDate { get; set; } = true;
        public bool ShowBirthDateForPublic { get; set; } = true;
        public bool ShowProfilePicture { get; set; } = true;
        public bool ShowProfilePictureForPublic { get; set; } = true;
        public bool ShowSkills { get; set; } = true;    
        public bool ShowSkillsForPublic { get; set; } = true;    
    }
}
