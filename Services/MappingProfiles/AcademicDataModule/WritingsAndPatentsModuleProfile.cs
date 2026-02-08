using Domain.Entities.AcademicDataModule.WritingsAndPatents;
using Shared.Dtos.AcademicDataModule.WritingsAndPatentsModule;

namespace Services.MappingProfiles.AcademicDataModule
{
    public class WritingsAndPatentsModuleProfile : Profile
    {
        public WritingsAndPatentsModuleProfile()
        {
            #region Scientific Writings
            CreateMap<ScientificWritings, ScientificWritingsResponseDTO>()
                .ForMember(dest => dest.AuthorRole, opt => opt.MapFrom(src => src.AuthorRole));
            CreateMap<ScientificWritingsCreateDTO, ScientificWritings>();
            CreateMap<ScientificWritingsUpdateDTO, ScientificWritings>();
            #endregion

            #region Patents
            CreateMap<Patents, PatentsResponseDTO>()
                .ForMember(dest => dest.LocalOrInternational, opt => opt.MapFrom(src => src.LocalOrInternational));
            CreateMap<PatentsCreateDTO, Patents>();
            CreateMap<PatentsUpdateDTO, Patents>();
            #endregion
        }
    }
}
