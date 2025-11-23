using Domain.Entities.ProjectsAndCommitteesModule;
using Domain.Entities.ScientificProgressionModule;
using Shared.Dtos.ProjectsAndCommitteesModule;
using Shared.Dtos.ScientificProgressionModule;

namespace Services.MappingProfiles
{
    public class ProjectsAndCommitteesModule : Profile
    {
        public ProjectsAndCommitteesModule()
        {
            CreateMap<CommitteesAndAssociations, CommitteesAndAssociationsResponseDto>()
                .ForMember(dest => dest.TypeOfCommitteeOrAssociation, opt => opt.MapFrom(src => src.TypeOfCommitteeOrAssociation))
                .ForMember(dest => dest.DegreeOfSubscription, opt => opt.MapFrom(src => src.DegreeOfSubscription));

            CreateMap<CommitteesAndAssociationsCreateDto, CommitteesAndAssociations>();
            CreateMap<CommitteesAndAssociationsUpdateDto, CommitteesAndAssociations>();

            CreateMap<ReviewingArticlesDto, ReviewingArticles>();
            CreateMap<ReviewingArticles, ReviewingArticlesDto>();

            CreateMap<ReviewingArticleCreateDto, ReviewingArticles>();
        }
    }
}
