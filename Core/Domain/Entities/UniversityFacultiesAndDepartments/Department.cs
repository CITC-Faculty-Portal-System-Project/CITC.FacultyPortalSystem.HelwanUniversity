namespace Domain.Entities.UniversityFacultiesAndDepartments
{
    public class Department : BaseEntity<int>
    {
        public string NameAR { get; set; } = string.Empty;
        public string NameEN { get; set; } = string.Empty;

        #region NavigationsAndRelations

        public int FacultyId { get; set; }
        public Faculty? Faculty { get; set; }

        public ICollection<PersonalData>? FacultyMembers { get; set; } = new List<PersonalData>();

        #endregion
    }
}
