namespace Domain.Entities.FacultyMemberData
{
    public class University : BaseEntity<int>
    {
        public string UniversityName { get; set; } = string.Empty;

        #region Relation With Faculty
        public ICollection<Faculty> Faculties { get; set; } = new HashSet<Faculty>();
        #endregion
    }
}
