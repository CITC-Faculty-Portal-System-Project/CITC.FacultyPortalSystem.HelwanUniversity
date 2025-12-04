using Shared.Dtos;
using System.Text.RegularExpressions;

namespace Services.MappingProfiles
{
    public class LookupItemProfile : Profile
    {
        public LookupItemProfile()
        {
            CreateMap<Lookup, LookupItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ValueAr, opt => opt.MapFrom(src => Regex.Unescape(src.ValueAr)))
                .ForMember(dest => dest.ValueEn, opt => opt.MapFrom(src => Regex.Unescape(src.ValueEn)));

        }
    }
}
