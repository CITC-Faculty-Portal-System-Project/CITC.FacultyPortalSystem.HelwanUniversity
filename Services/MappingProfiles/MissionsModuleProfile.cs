using Domain.Entities.MissionsModule;
using Shared.Dtos.MissionsModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class MissionsModuleProfile : Profile
    {
        public MissionsModuleProfile()
        {
            
            #region Add            
            
            CreateMap<MissionAddDto, ScientificMissions>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.FacultyMemberId, opt => opt.MapFrom(src => src.FacultyMemberId))
                .ForMember(dest => dest.CountryOrCity, opt => opt.MapFrom(src => src.CountryOrCity))
                .ForMember(dest => dest.MissionName, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.UniversityOrFaculty, opt => opt.MapFrom(src => src.UniversityOrFaculty));

            #endregion

            #region Edit

            CreateMap<MissionEditDto , ScientificMissions>()
                    .ForMember(dest => dest.MissionName, opt =>
                    {
                      opt.Condition(src => !string.IsNullOrEmpty(src.name));
                      opt.MapFrom(src => src.name);
                    })
                    .ForMember(dest => dest.CountryOrCity, opt =>
                    {
                        opt.Condition(src => !string.IsNullOrEmpty(src.CountryOrCity));
                        opt.MapFrom(src => src.CountryOrCity);
                    })
                    .ForMember(dest => dest.Notes, opt =>
                    {
                        opt.Condition(src => !string.IsNullOrEmpty(src.Description));
                        opt.MapFrom(src => src.Description);
                    })
                    .ForMember(dest => dest.UniversityOrFaculty, opt =>
                    {
                        opt.Condition(src => !string.IsNullOrEmpty(src.UniversityOrFaculty));
                        opt.MapFrom(src => src.UniversityOrFaculty);
                    })
                    .ForMember(dest => dest.StartDate, opt =>
                    {
                        opt.Condition(src => src.StartDate != null);
                        opt.MapFrom(src => src.StartDate);
                    })
                    .ForMember(dest => dest.EndDate, opt =>
                    {
                        opt.Condition(src => src.EndDate != null);
                        opt.MapFrom(src => src.EndDate);
                    })
                    .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));


            CreateMap<MissionEditDto, MissionEditResponseDto>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.UniversityOrFaculty, opt => opt.MapFrom(src => src.UniversityOrFaculty))
                .ForMember(dest => dest.CountryOrCity, opt => opt.MapFrom(src => src.CountryOrCity))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            #endregion

            #region Get

            CreateMap<ScientificMissions, MissionResponseDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.MissionName))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.CountryOrCity, opt => opt.MapFrom(src => src.CountryOrCity))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.UniversityOrFaculty, opt => opt.MapFrom(src => src.UniversityOrFaculty));


            #endregion

        }
    }
}
