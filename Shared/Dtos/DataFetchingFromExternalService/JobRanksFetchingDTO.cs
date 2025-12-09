using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.DataFetchingFromExternalService
{
    public record JobRanksFetchingDTO
    {
        public string Name { get; set; } = string.Empty;
        public DateOnly PromotionDate { get; set; }
        public string NationalNumber { get; set; } = string.Empty;

    }
}
