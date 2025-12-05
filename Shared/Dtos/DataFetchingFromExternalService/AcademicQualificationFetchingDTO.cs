using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.DataFetchingFromExternalService
{
    public record AcademicQualificationFetchingDTO
    {
        public string CountryCity { get; set; } = string.Empty;
        public string UniversityFaculty { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string DateOfAcquisition { get; set; } = string.Empty;
        public string Qualification { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string NationalNumber { get; set; } = string.Empty;
        public string Dispatch { get; set; } = string.Empty;
    }
}
