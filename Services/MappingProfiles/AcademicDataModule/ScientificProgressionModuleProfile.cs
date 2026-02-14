using Domain.Entities.AcademicDataModule.ScientificProgressionModule;
using Shared.Dtos.AcademicDataModule.ScientificProgressionModule;

namespace Services.MappingProfiles.AcademicDataModule
{
    public class ScientificProgressionModuleProfile : Profile
    {
        public ScientificProgressionModuleProfile()
        {
            CreateMap<AcademicQualifications, AcademicQualificationResponseDto>()
                .ForMember(dest => dest.Qualification, opt => opt.MapFrom(src => src.Qualification))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade))
                .ForMember(dest => dest.DispatchType, opt => opt.MapFrom(src => src.DispatchType));
                

            CreateMap<AcademicQualificationCreateDto, AcademicQualifications>();
            CreateMap<AcademicQualificationsUpdateDto, AcademicQualifications>();

            CreateMap<JobRanks, JobRankResponseDto>()
                .ForMember(dest => dest.JobRank, opt => opt.MapFrom(src => src.JobRank));
            CreateMap<JobRankCreateDto, JobRanks>();
            CreateMap<JobRankUpdateDto, JobRanks>();

            CreateMap<AdministrativePositionDto, AdministrativePositions>();
            CreateMap<AdministrativePositions, AdministrativePositionDto>();

            CreateMap<AdministrativePositionCreateDto, AdministrativePositions>();
        }
    }
}
