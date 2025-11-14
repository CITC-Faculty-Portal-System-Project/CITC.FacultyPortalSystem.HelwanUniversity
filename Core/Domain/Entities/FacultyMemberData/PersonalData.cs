namespace Domain.Entities.FacultyMemberData
{
    public class PersonalData : BaseEntity<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public string SocialStatus { get; set; } = string.Empty;
        public DateOnly? BirthDate { get; set; }
        public string? BirthPlace { get; set; } 
        public string? NameInComposition { get; set; } 
        public string? CompositionTopics { get; set; }

        #region Relation With FacultyMember
        public Guid FacultyMemberId { get; set; }
        #endregion

        #region Navigation Properties
        public FacultyMember? FacultyMember { get; set; }
        #endregion
    }
}
