using Domain.Entities.AcademicDataModule.PrizesModule;
using Shared.Dtos.AcademicDataModule.PrizesModule;

namespace Services.MappingProfiles.AcademicDataModule
{
    public class PrizesModuleProfile : Profile
    {
        public PrizesModuleProfile()
        {
            #region Prizes And Rwards
            CreateMap<PrizesAndRewards, PrizesAndRewardsResponseDTO>()
                .ForMember(dest => dest.Prize, opt => opt.MapFrom(src => src.Prize));
            CreateMap<PrizesAndRewardsCreateDTO, PrizesAndRewards>();
            CreateMap<PrizesAndRewardsUpdateDTO, PrizesAndRewards>();
            #endregion

            #region Manifestations Of Scientific Appreciation
            CreateMap<ManifestationsOfScientificAppreciation, ManifestationsOfScientificAppreciationResponseDTO>();
            CreateMap<ManifestationsOfScientificAppreciationCreateDTO, ManifestationsOfScientificAppreciation>();
            CreateMap<ManifestationsOfScientificAppreciationUpdateDTO, ManifestationsOfScientificAppreciation>();
            #endregion
        }
    }
}
