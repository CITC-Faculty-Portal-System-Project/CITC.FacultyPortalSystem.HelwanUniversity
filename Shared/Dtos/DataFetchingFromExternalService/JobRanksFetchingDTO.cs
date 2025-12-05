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
        public string PromotionDate { get; set; } = string.Empty;
        public string NationalNumber { get; set; } = string.Empty;

    }
}
