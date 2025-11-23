using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.MissionsModule
{
    public record MissionAddDto : BaseAddDto
    {
        [Required(ErrorMessage ="Please Enter Mission Name")]
        public string name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please Enter Country/City")]
        public string CountryOrCity { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please Enter University/Faculty")]
        public string UniversityOrFaculty { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please Enter Start Date")]
        public DateOnly StartDate { get; set; }
        [Required(ErrorMessage = "Plase Enter End Date")]
        public DateOnly EndDate { get; set; }
        [Required(ErrorMessage = "Please Enter Description")]
        public string Description { get; set; } = string.Empty;

        public Guid FacultyMemberId { get; set; }
    }
}
