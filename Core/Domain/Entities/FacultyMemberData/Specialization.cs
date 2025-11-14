namespace Domain.Entities.FacultyMemberData
{
    public class Specialization : BaseEntity<int>
    {
        public string? GeneralSpecialization { get; set; } 
        public string? AccurateSpecialization { get; set; }

        #region Relation With Department
        public virtual Department? Department { get; set; }
        public int DepartmentId { get; set; }
        #endregion

        #region Relation With FacultyMember
        public ICollection<FacultyMember> FacultyMembers { get; set; } = new HashSet<FacultyMember>();
        #endregion
    }
}
