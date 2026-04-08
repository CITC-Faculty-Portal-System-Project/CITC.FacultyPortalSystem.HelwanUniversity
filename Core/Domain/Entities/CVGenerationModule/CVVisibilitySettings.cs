namespace Domain.Entities.CVGenerationModule
{
    public class CVVisibilitySettings : BaseEntity<Guid>
    {
        public Guid FacultyMemberId { get; set; }
        public string VisibilityJson { get; set; } = "{}";
        public string PublicVisableAttributesJson { get; set; } = "{}";
        public bool isPublic { get; set; }

    }
}
