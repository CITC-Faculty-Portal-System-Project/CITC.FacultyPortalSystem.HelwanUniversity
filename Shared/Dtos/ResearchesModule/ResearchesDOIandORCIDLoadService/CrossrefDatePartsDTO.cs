using System.Text.Json.Serialization;

namespace Shared.Dtos.ResearchesModule.ResearchesDOIandORCIDLoadService
{
    public record CrossrefDatePartsDTO
    {
        [JsonPropertyName("date-parts")]
        public List<List<int>>? DateParts { get; set; }

    }
}
