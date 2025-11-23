using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.ConfrencesAndSeminarsModule
{
    public record ConferncesAndSeminarsResponseDto
    {
        public string Name { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string OrganiserName { get; set; } = string.Empty;
        public string CountryOrCity { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string InternationalOrLocal { get; set; } = string.Empty;
        public string ParticipationRole { get; set; } = string.Empty;
        public string WebSite { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}
