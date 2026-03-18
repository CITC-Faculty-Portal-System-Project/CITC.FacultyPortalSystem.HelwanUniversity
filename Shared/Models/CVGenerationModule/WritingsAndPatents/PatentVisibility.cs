namespace Shared.Models.CVGenerationModule.WritingsAndPatents
{
    public class PatentVisibility
    {
        public bool ShowPatents { get; set; } = true;
        public bool ShowNameOfPatent { get; set; } = true;
        public bool ShowAccreditingAuthorityOrCountry { get; set; } = true;
        public bool ShowAccreditationDate { get; set; } = true;
    }
}
