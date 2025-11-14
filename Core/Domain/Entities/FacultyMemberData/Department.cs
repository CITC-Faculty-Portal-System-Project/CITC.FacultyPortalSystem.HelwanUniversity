namespace Domain.Entities.FacultyMemberData
{
    public class Department : BaseEntity<int>
    {
        public string DepartmentName { get; set; } = string.Empty;

        #region Relation With Field
        public virtual FieldOfStudy? FieldOfStudy { get; set; }

        public int FieldOfStudyId { get; set; }
        #endregion

        #region Relation With Specialization
        public ICollection<Specialization> Specializations { get; set; } = new HashSet<Specialization>();
        #endregion
    }
}
