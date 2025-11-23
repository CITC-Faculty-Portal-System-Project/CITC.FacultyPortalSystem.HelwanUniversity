using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.MissionsModule
{
    public record MissionAddResponse
    {
        public string name { get; set; } = string.Empty;
        public string CountryOrCity { get; set; } = string.Empty;
        public string UniversityOrFaculty { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid FacultyMemberId { get; set; }

    }
}
