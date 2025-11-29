using Domain.Entities.ProjectsAndCommitteesModule;
using Shared.Dtos.ProjectsAndCommitteesModule;

namespace Services.MappingProfiles
{
    public class ProjectsAndCommitteesModule : Profile
    {
        public ProjectsAndCommitteesModule()
        {
            CreateMap<CommitteesAndAssociations, CommitteesAndAssociationsResponseDto>()
                .ForMember(dest => dest.TypeOfCommitteeOrAssociation, opt => opt.MapFrom(src => src.TypeOfCommitteeOrAssociation))
                .ForMember(dest => dest.DegreeOfSubscription, opt => opt.MapFrom(src => src.DegreeOfSubscription));

            CreateMap<CommitteeOrAssociationCreateDto, CommitteesAndAssociations>();
            CreateMap<CommitteeOrAssociationUpdateDto, CommitteesAndAssociations>();

            CreateMap<ReviewingArticlesDto, ReviewingArticles>();
            CreateMap<ReviewingArticles, ReviewingArticlesDto>();

            CreateMap<ReviewingArticleCreateDto, ReviewingArticles>();

            CreateMap<ParticipationInMagazines, ParticipationInMagazinesResponseDto>()
                .ForMember(dest => dest.TypeOfParticipation, opt => opt.MapFrom(src => src.TypeOfParticipation));

            CreateMap<ParticipationInMagazineCreateDto, ParticipationInMagazines>();
            CreateMap<ParticipationInMagazineUpdateDto, ParticipationInMagazines>();

            CreateMap<Projects, ProjectsResponseDto>()
                .ForMember(dest => dest.TypeOfProject, opt => opt.MapFrom(src => src.TypeOfProject))
                .ForMember(dest => dest.ParticipationRole, opt => opt.MapFrom(src => src.ParticipationRole))
                .ForMember(dest => dest.LocalOrInternational, opt => opt.MapFrom(src => src.LocalOrInternational));

            CreateMap<ProjectCreateDto, Projects>();
            CreateMap<ProjectUpdateDto, Projects>();
            CreateMap<ReviewArticleUpdateDto, ReviewingArticles>();
        }
    }
}
