using System.Text.Json.Serialization;

namespace Shared.Dtos.ResearchesModule.ResearchesDOIandORCIDLoadService
{
    public record CrossrefMessageDTO
    {
        public string? DOI { get; set; }
        public string? URL { get; set; }

        public List<string>? Title { get; set; }
        
        [JsonPropertyName("container-title")]
        public List<string>? ContainerTitle { get; set; }  

        public string? Publisher { get; set; }
        public string? Type { get; set; }

        public string? Volume { get; set; }
        public string? Issue { get; set; }
        public string? Page { get; set; }

        public int Year { get; set; }

        public int? ReferenceCount { get; set; }
        public int? IsReferencedByCount { get; set; }

        public string? Abstract { get; set; }


        [JsonPropertyName("reference")]
        public List<RelatedResearchFromDOIDTO>? Reference { get; set; }  

        public RelatedResearchFromDOIDTO? Relation { get; set; }  

        public List<CrossrefAuthorDTO>? Author { get; set; }

        public CrossrefDatePartsDTO? PublishedPrint { get; set; }
        public CrossrefDatePartsDTO? PublishedOnline { get; set; }
        public CrossrefDatePartsDTO? Issued { get; set; }
        public CrossrefDatePartsDTO? Created { get; set; }
    }
}
