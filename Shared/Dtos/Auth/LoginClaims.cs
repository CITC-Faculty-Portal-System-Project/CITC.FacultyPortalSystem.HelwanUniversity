using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Auth
{
    public record LoginClaims
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public IList<string> Roles { get; set; } = new List<string>();
        public string Token { get; set; } = string.Empty;
        public string NationalNumber { get; set; } = string.Empty;

    }
}
