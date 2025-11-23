using Domain.Entities.MissionsModule;
using Shared.Dtos.ConfrencesAndSeminarsModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class ConferncesAndModuleMappingProfile : Profile
    {
        public ConferncesAndModuleMappingProfile()
        {

            #region Add

            CreateMap<ConfrencesAndSeminarsAddDto, ConferencesAndSeminars>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Venue))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.LocalOrInternational, opt => opt.MapFrom(src => src.LocalOrInternational))
                .ForMember(dest => dest.OrganizingAuthority, opt => opt.MapFrom(src => src.OrganizingAuthority))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.RoleOfParticipationId, opt => opt.MapFrom(src => src.RoleOfParticipationId))
                .ForMember(dest => dest.Website, opt => opt.MapFrom(src =>
                                    !string.IsNullOrEmpty(src.Website) ? src.Website : "لا يوجد"))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src =>
                                    !string.IsNullOrEmpty(src.Notes) ? src.Notes : "لا يوجد"));



            #endregion

            #region Get

            CreateMap<ConferencesAndSeminars, ConferncesAndSeminarsResponseDto>()
                 .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                 .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.OrganiserName, opt => opt.MapFrom(src => src.OrganizingAuthority))
                 .ForMember(dest => dest.CountryOrCity, opt => opt.MapFrom(src => src.Venue))
                 .ForMember(dest => dest.ParticipationRole, opt => opt.MapFrom(src => src.RoleOfParticipation.ValueAr))
                 .ForMember(dest => dest.WebSite, opt => opt.MapFrom(src => 
                        !string.IsNullOrEmpty (src.Website) ? src.Website : "لا يوجد"))
                 .ForMember(dest => dest.Notes, opt => opt.MapFrom(src =>
                                    !string.IsNullOrEmpty(src.Notes) ? src.Notes : "لا يوجد"))
                 .ForMember(dest => dest.InternationalOrLocal, opt => opt.MapFrom(src =>
                          src.LocalOrInternational.ToString() == LocalOrInternational.Local.ToString() ? "محلي" : "دولي"))
                 .ForMember(dest => dest.Type, opt => opt.MapFrom(src =>
                        src.Type.ToString() == ConferenceOrSeminar.Seminar.ToString() ? "مؤتمر" : "ندوة"));


            #endregion

            #region Update

            CreateMap<ConfrencesAndSeminarsEditDto, ConferencesAndSeminars>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                    .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                    .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                    .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Venue))
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                    .ForMember(dest => dest.LocalOrInternational, opt => opt.MapFrom(src => src.LocalOrInternational))
                    .ForMember(dest => dest.OrganizingAuthority, opt => opt.MapFrom(src => src.OrganizingAuthority))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.RoleOfParticipationId, opt => opt.MapFrom(src => src.RoleOfParticipationId))
                    .ForMember(dest => dest.Website, opt => opt.MapFrom(src =>src.Website))
                    .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => (src.Notes)));


            #endregion
        }
    }
}
