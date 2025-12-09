using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.DataFetchingFromExternalService
{
    public record AdminstrativePostionsFetchingDTO
    {
        public string StartDate { get; set; } = string.Empty;
        public string? EndDate { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string NationalNumber { get; set; } = string.Empty;
    }
}
