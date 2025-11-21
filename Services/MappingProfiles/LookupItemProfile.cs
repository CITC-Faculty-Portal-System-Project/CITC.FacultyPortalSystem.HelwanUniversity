using Shared.Dtos;

namespace Services.MappingProfiles
{
    public class LookupItemProfile : Profile
    {
        public LookupItemProfile()
        {
            CreateMap<Lookup, LookupItemDto>();
        }
    }
}
