namespace Shared.Models.CVGenerationModule
{
    public class ContactInfoVisibility
    {
        public bool ShowContactInfo { get; set; } = true;
        public bool ShowContactInfoForPublic { get; set; } = true;
        public bool ShowMainPhone { get; set; } = true;
        public bool ShowMainPhoneForPublic { get; set; } = true;
        public bool ShowWorkPhone { get; set; } = true;
        public bool ShowWorkPhoneForPublic { get; set; } = true;
        public bool ShowOfficialEmail { get; set; } = true;
        public bool ShowOfficialEmailForPublic { get; set; } = true;
        public bool ShowFax { get; set; } = true;
        public bool ShowFaxForPublic { get; set; } = true;
    }
}
