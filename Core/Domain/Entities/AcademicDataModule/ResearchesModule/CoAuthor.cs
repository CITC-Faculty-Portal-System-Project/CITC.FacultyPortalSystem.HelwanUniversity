using System.Reflection.PortableExecutable;

namespace Domain.Entities.AcademicDataModule.ResearchesModule
{
    public class CoAuthor : BaseEntity<int>
    {
        public string ScholarProfileLink { get; set; } = string.Empty;
        public string AcademicName { get; set; } = string.Empty;
        public string ScholarProfileImageURL { get; set; } = string.Empty;
        public string OrganisationalDomain { get; set; } = string.Empty;

        #region Navigations

        public ICollection<ResearcherCoAuthor>? Researchers { get; set; } = new List<ResearcherCoAuthor>();

        #endregion

    }
}
