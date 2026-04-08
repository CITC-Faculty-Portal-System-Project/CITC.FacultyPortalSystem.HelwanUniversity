namespace Shared.Models.CVGenerationModule.WritingsAndPatents
{
    public class ScientificWritingVisibility
    {
        public bool ShowScientificWritings { get; set; } = true;
        public bool ShowScientificWritingsForPublic { get; set; } = true;
        public bool ShowTitle { get; set; } = true;
        public bool ShowTitleForPublic { get; set; } = true;
        public bool ShowAuthorRole { get; set; } = true;
        public bool ShowAuthorRoleForPublic { get; set; } = true;
        public bool ShowISBN { get; set; } = true;
        public bool ShowISBNForPublic { get; set; } = true;
        public bool ShowPublishingHouse { get; set; } = true;
        public bool ShowPublishingHouseForPublic { get; set; } = true;
        public bool ShowPublishingDate { get; set; } = true;
        public bool ShowPublishingDateForPublic { get; set; } = true;
    }
}
