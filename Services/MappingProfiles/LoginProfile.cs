using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Auth;

namespace Services.MappingProfiles
{
    public class LoginProfile : Profile
    {
        public LoginProfile() {
            CreateMap<LoginClaims, LoginClaimsResponseDto>();
        }
    }
}
