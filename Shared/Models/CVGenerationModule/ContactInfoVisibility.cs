namespace Shared.Models.CVGenerationModule
{
    public class ContactInfoVisibility
    {
        public bool ShowContactInfo { get; set; } = true;
        public bool ShowMainPhone { get; set; } = true;
        public bool ShowWorkPhone { get; set; } = true;
        public bool ShowOfficialEmail { get; set; } = true;
        public bool ShowFax { get; set; } = true;
    }
}
