namespace Domain.Entities.ResearchesModule.Theses_Supervision
{
    public class Theses : BaseEntity<int>
    {
        public string Title { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string? ThesesInEnglishHyperLink { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string StudentMajor { get; set; } = string.Empty;
        public string? StudentNationalNumber { get; set; }
        public DateOnly? EnrollmentDate { get; set; }
        public DateOnly? RegistrationDate { get; set; }
        public DateOnly? InternalGradeDate { get; set; }
        public DateOnly? SupervisionConfirmationDate { get; set; }
        public ThesesType ThesesType { get; set; }

        #region Navigation Properties
        public ICollection<ThesesSupervision> Supervisions { get; set; } = new HashSet<ThesesSupervision>();
        #endregion
    }
}
