namespace Domain.Entities.UniversityFacultiesAndDepartments
{
    public class Faculty : BaseEntity<int>
    {
        public string NameAR { get; set; } = string.Empty;
        public string NameEN { get; set; } = string.Empty;


        #region NavigationsAndRelationShips

        public ICollection<PersonalData>? FacultyMembersPersonalData { get; set; }
        public ICollection<Department>? Departments { get; set; } = new List<Department>();

        #endregion
    }
}