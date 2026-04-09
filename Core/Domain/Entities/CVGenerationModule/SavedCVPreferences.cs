namespace Domain.Entities.CVGenerationModule
{
    public class SavedCVPreferences : BaseEntity<int>
    {
        public Guid FacultyMemberId { get; set; }
        public string TemplateName { get; set; } = string.Empty;    
    }
}
