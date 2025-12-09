using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.DataFetchingFromExternalService
{
    public record AdminstrativePostionsFetchingDTO
    {
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string NationalNumber { get; set; } = string.Empty;
    }
}
