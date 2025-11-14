namespace Domain.Entities.FacultyMemberData
{
    public class Faculty : BaseEntity<int>
    {
        public string FacultyName { get; set; } = string.Empty;

        #region Relation With University 
        [InverseProperty(nameof(University.Faculties))]
        public virtual University? University { get; set; }

        [ForeignKey(nameof(University))]
        public int UniversityId { get; set; }   
        #endregion

        #region Relation With FieldOfStudy
        [InverseProperty(nameof(FieldOfStudy.Faculty))]
        public ICollection<FieldOfStudy> FieldsOfStudy { get; set; } = new HashSet<FieldOfStudy>();
        #endregion
    }
}
