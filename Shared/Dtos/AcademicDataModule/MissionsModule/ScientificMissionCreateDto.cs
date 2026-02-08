using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.AcademicDataModule.MissionsModule
{
    public record ScientificMissionCreateDto
    {
        [Required(ErrorMessage ="Please Enter Mission Name")]
        public string name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please Enter Country/City")]
        public string CountryOrCity { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please Enter University/Faculty")]
        public string UniversityOrFaculty { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please Enter Start Date")]
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        [Required(ErrorMessage = "Please Enter Description")]
        public string Description { get; set; } = string.Empty;

        public Guid FacultyMemberId { get; set; }
    }
}
