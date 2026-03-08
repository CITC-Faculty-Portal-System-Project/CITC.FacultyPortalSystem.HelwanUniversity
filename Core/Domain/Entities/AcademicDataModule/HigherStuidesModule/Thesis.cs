using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.EntitesAttachments;

namespace Domain.Entities.AcademicDataModule.HigherStuidesModule
{
    public class Thesis : BaseEntity<int>
    {
        public ThesisType Type { get; set; }
        public string? Link { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public Guid GradeId { get; set; }
        public Lookup? Grade { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public DateOnly? InternalGradeDate { get; set; }
        public DateOnly? SupervisionConfirmationDate { get; set; }
        public DateOnly? DiscussionDate { get; set; }
        public string? UniversityOrFaculty { get; set; }
        public Guid FacultyMemberId { get; set; }
        public FacultyMember? FacultyMember { get; set; }
        public ICollection<ThesisComittee>? ComitteeMembers = new List<ThesisComittee>();
        public ICollection<Research>? Researches { get; set; } = new List<Research>();
        public ICollection<ThesesAttachment>? Attachments { get; set; } = new List<ThesesAttachment>();
        public ICollection<Supervising>? Supervisings { get; set; } = new List<Supervising>();
    }
}
