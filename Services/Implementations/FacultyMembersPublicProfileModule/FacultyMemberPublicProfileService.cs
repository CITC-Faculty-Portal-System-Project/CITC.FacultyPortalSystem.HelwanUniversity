using Services.Abstraction.Contracts.FacultyMembersPublicProfileModule;
using Services.Global;
using Services.Helpers.PaginationHelpers;
using Services.Specifications.FacultyMembersProfilesModule;
using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.Dtos.FacultyMembersProfilesModule;
using Shared.Dtos.ResearchesModule;
using Shared.SpecificationParameters.FacultyMembersProfilesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.ResearchesModule;

namespace Services.Implementations.FacultyMembersPublicProfileModule
{
    public class FacultyMemberPublicProfileService(
      IUnitOfWork unitOfWork,
      IAuthenticationService authenticationService,
      IMapper mapper) : BaseService<FacultyMember, Guid>(unitOfWork, authenticationService, mapper)
        , IFacultyMemberPublicProfileService
    {
        protected override string EntityName => "Faculty Member";

        public async Task<CursorPaginatedResult<OtherUsersPageResponseDTO, Guid>> GetAllFacultyMembersProfiles(FacultyMembersProfileSpecificationParamters paramters)
        {
            var members = await Repo.GetAllAsync(new OtherUsersPageSpecifications(paramters , paramters.Take , true));

            var count = await Repo.CountAsync(new  OtherUsersPageSpecifications(paramters));

            var (orderedMembers, hasMore, nextCursor) =
               CursorPaginationHelper.ProcessCursorPagination(
                   members.ToList(),
                   paramters.Take,
                   m => m.Id,
                   m => m.CreatedAt
               );

            var mapped = Mapper.Map<IEnumerable<OtherUsersPageResponseDTO>>(members);

            return new CursorPaginatedResult<OtherUsersPageResponseDTO, Guid>
            {
                Items = mapped,
                HasMore = hasMore,
                NextCursor = nextCursor,
                Count = count
            };
        }

        public async Task<FacultyMemberPublicProfileResponseDTO> GetFacultyMemberPublicProfile(Guid facultyMemberId)
        {
            var member = await Repo.GetAsync(new FacultyMemberPublicProfileSpecifications(facultyMemberId))
                ?? throw NotFound();


            return Mapper.Map<FacultyMemberPublicProfileResponseDTO>(member);
            
        }

        public async Task<IEnumerable<OtherUsersPageResponseDTO>> SearchMemberPublicProfile(BaseFacultyMemberProfileSpecificationParamters paramters)
        {
            var members = await Repo.GetAllAsync(new OtherUsersPageSpecifications(paramters));

            return  Mapper.Map<IEnumerable<OtherUsersPageResponseDTO>>(members);

        }
    }
}
