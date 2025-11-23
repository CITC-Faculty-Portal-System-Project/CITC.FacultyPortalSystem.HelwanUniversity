using Shared.Enums.DTORequired.ConfrencesAndSeminarsModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.ConfrencesAndSeminarsModule
{
    public record ConfrencesAndSeminarsEditDto : BaseEditDto
    {
        public ConferenceOrSeminar? Type { get; set; }
        public LocalOrInternational? LocalOrInternational { get; set; }
        public string? Name { get; set; } = string.Empty;
        public Guid? RoleOfParticipationId { get; set; }
        public string? OrganizingAuthority { get; set; } = string.Empty;
        public string? Website { get; set; } = string.Empty;
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Venue { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;

    }
}
