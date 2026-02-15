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
                 .ForMember(d => d.Value, opt => opt.MapFrom((src, _, __, ctx) =>
                        (bool)ctx.Items["isAr"] ? src.ValueAr : src.ValueEn
                    ));
        }
    }
}
