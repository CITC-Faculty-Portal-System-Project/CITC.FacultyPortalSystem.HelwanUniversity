namespace Domain.Entities.FacultyMemberData
{
    public class FieldOfStudy : BaseEntity<int>
    {
        public string FieldOfStudyName { get; set; } = string.Empty;

        #region Relation With Faculty
        [InverseProperty(nameof(Faculty.FieldsOfStudy))]
        public virtual Faculty? Faculty { get; set; }

        [ForeignKey(nameof(Faculty))]
        public int FacultyId { get; set; }
        #endregion

        #region Relation With Department
        [InverseProperty(nameof(Department.FieldOfStudy))]
        public ICollection<Department> Departments { get; set; } = new HashSet<Department>();
        #endregion
    }
}
